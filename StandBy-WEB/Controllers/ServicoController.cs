using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using standby_data.Context;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Services;
using static standby_data.Repositories.RepositoryServico;

namespace StandBy_WEB.Controllers
{
  [Authorize]
  // [Authorize(Roles = "Administrador, Gerente, Operador")]
  public class ServicoController : Controller
  {
    private ServicoService servicoService = new ServicoService();
    private ClienteService clienteService = new ClienteService();
    private ItemService itemService = new ItemService();
    private readonly ILogger<ServicoController> _logger;



    public ServicoController(ILogger<ServicoController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      var servicos = servicoService.repositoryServico.BuscarTodos();
      return View("Index", servicos);
    }

    // [Route("Servico/Adicionar")]
    public IActionResult Adicionar()
    {
      var modelTeste = CarregarListagemItems();
      Console.WriteLine(modelTeste.ListasItems.InCorItemsList.Count);
      // modelTeste.ListasItems.InMarcaItemsList.ForEach(x => Console.WriteLine(x));
      modelTeste.ListasItems.InCorItemsList.ForEach(x => Console.WriteLine(x));
      return View("Adicionar", modelTeste);
    }


    public AdicionarServicoModel CarregarListagemItems()
    {
      var model = new AdicionarServicoModel();
      model.ListasItems.InMarcaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "INFOS_APARELHO_ITEM", "Marca");
      model.ListasItems.InCorItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "INFOS_APARELHO_ITEM", "Cor");



      return model;
    }


    [Route("Servico/Editar/{id?}")]
    public IActionResult EditarServico(int? id, ServicoCompletoModel _servicoCompleto, IFormCollection form)
    {
      #region Essa é a parte que o usuario clicou em editar no grid e vai chamar a view para edição
      if (id.HasValue) //
      {
        ServicoCompletoModel servicoEditar = new ServicoCompletoModel();
        servicoEditar = servicoService.repositoryServico.BuscarServicoCompleto(id.Value);

        if (servicoEditar.servico.sv_senha_pattern != null)
        {
          byte[] imageBytes = (byte[])servicoEditar.servico.sv_senha_pattern;
          string base64String = Convert.ToBase64String(imageBytes);
          string imageSrc = String.Format("data:image/jpg;base64,{0}", base64String);
          ViewData["imageSrc"] = imageSrc;
        }

        Console.WriteLine("Prev de entrega do serv: " + servicoEditar.servico.sv_previsao_entrega);
        return View(servicoEditar);
      }
      #endregion

      #region Essa parte já é a final, o usuario ja editou as informações e deu submit no formulario

      //Removendo esses itens da verificação porque alterando o serviço, pouco importa se esses campos estão vazios.
      ModelState.Remove("cliente.cl_telefone");
      ModelState.Remove("cliente.cl_telefone_recado");
      ModelState.Remove("servico.sv_previsao_entrega");

      if (!ModelState.IsValid)
      {
        ModelState.AddModelError(string.Empty, GetModelStateErrorString());
        System.Console.WriteLine("Erro no ModelState");
        return View("EditarServico", _servicoCompleto);
      }

      try
      {
        //Futuramente vai quer te atualizar Cond e Checklist
        var imageData = form["ImageData"].ToString();
        if (imageData != "" && imageData != null)
        {
          System.Console.WriteLine("Inserindo nova senha, usuario trocou.");
          // Converte a string base64 em um array de bytes
          byte[] imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);
          // Armazena a imagem na coluna sv_senha_pattern
          var senhaPattern = imageBytes;
          _servicoCompleto.servico.sv_senha_pattern = senhaPattern;
        }
        else
        {
          var senhaPatternAtualDoCliente = form["senhaPatternCliente"].ToString();
          if (senhaPatternAtualDoCliente != "" && senhaPatternAtualDoCliente != null)
          {
            System.Console.WriteLine("Cliente ja possui uma senha, usuario nao tentou trocar.");
            byte[] imageBytes = Convert.FromBase64String(senhaPatternAtualDoCliente.Split(',')[1]);
            var senhaPattern = imageBytes;
            _servicoCompleto.servico.sv_senha_pattern = senhaPattern;
          }
        }


        servicoService.repositoryServico.Atualizar(_servicoCompleto.servico);
        servicoService.repositoryServico.SalvarModificacoes();
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, ex.Message);
        System.Console.WriteLine("Erro ao salvar: " + ex.Message);
        return View("EditarServico", _servicoCompleto);
      }
      #endregion

      return RedirectToAction("Index");
    }

    [HttpGet]
    public JsonResult ListaServicos()
    {
      List<ServicoComNomeClienteStruct> listServicos = ListaServico();
      return Json(listServicos);
    }


    public List<ServicoComNomeClienteStruct> ListaServico()
    {
      var result = servicoService.repositoryServico.BuscarServicosComCliente();
      return result;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }

    private tb_servicos ObterServicoPorId(int id)
    {
      return servicoService.repositoryServico.BuscarPorID(id);
    }

    private string GetModelStateErrorString()
    {
      return string.Join(Environment.NewLine, ModelState.Values
          .SelectMany(x => x.Errors)
          .Select(x => x.ErrorMessage));
    }
  }
}