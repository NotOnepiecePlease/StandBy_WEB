using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using standby_data.Context;
using standby_data.Models;
using standby_data.Models.DTOs;
using standby_data.Models.UtilModels;
using standby_data.Services;
using static standby_data.Repositories.RepositoryServico;

namespace StandBy_WEB.Controllers
{
  [Authorize]
  // [Authorize(Roles = "Administrador, Gerente, Operador")]
  public class ServicoController : Controller
  {
    standby_orgContext context = new standby_orgContext();
    private ServicoService servicoService = new ServicoService();
    private ClienteService clienteService = new ClienteService();
    CondicoesFisicasService condicoesFisicasService = new CondicoesFisicasService();
    ChecklistService checkListService = new ChecklistService();
    private ItemService itemService = new ItemService();
    private readonly ILogger<ServicoController> _logger;

    private readonly IMapper _mapper;

    public ServicoController(ILogger<ServicoController> logger, IMapper mapper)
    {
      _logger = logger;
      _mapper = mapper;
    }

    public IActionResult Index()
    {
      var servicos = servicoService.repositoryServico.BuscarTodos();
      return View("Index", servicos);
    }

    // [Route("Servico/Adicionar")]
    [HttpGet]
    public IActionResult Adicionar()
    {
      AdicionarServicoModel model = new AdicionarServicoModel();
      model.ListasItems = CarregarListagemItems();
      var OrdemServicoNova = context.tb_log.FirstOrDefault().log_ultima_ordem_servico + 1;
      ViewData["OrdemServicoNova"] = OrdemServicoNova;
      //ViewData["ClienteSelecionado"] = null;
      return View("Adicionar", model);
    }

    [HttpPost]
    public IActionResult Adicionar(AdicionarServicoModel _clienteCompleto, IFormCollection form)
    {
      Console.WriteLine("Cliente: " + _clienteCompleto.servico.sv_cl_idcliente);
      var nomeCliente = form["clienteNOME"].ToString();
      var OrdemServicoNova = context.tb_log.FirstOrDefault().log_ultima_ordem_servico + 1;
      ModelState.Remove("cliente.cl_cpf");
      if (!ModelState.IsValid)
      {
        foreach (var modelState in ViewData.ModelState.Values)
        {
          foreach (var error in modelState.Errors)
          {
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
          }
        }

        AdicionarServicoModel modelTeste = new AdicionarServicoModel();
        modelTeste.ListasItems = CarregarListagemItems();

        ViewData["OrdemServicoNova"] = OrdemServicoNova;
        ViewData["ClienteSelecionado"] = _clienteCompleto.servico.sv_cl_idcliente;
        ViewData["clienteNOME"] = nomeCliente;

        return View("Adicionar", modelTeste);
      }

      try
      {

        var imageData = form["ImageData"].ToString();
        if (imageData != "" && imageData != null)
        {
          System.Console.WriteLine("Cadastrando senha pattern no servi??o.");
          // Converte a string base64 em um array de bytes
          byte[] imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);
          // Armazena a imagem na coluna sv_senha_pattern
          var senhaPattern = imageBytes;
          _clienteCompleto.servico.sv_senha_pattern = senhaPattern;
        }

        string checkBoxSelecionadaByUser = form["PrevisaoEntrega"].ToString();
        var previsaoEntrega = PegarPrevisaoEntrega(checkBoxSelecionadaByUser);

        var dataHoje = DateTime.Now;
        var servico = _clienteCompleto.servico;
        servico.sv_ordem_serv = OrdemServicoNova;
        servico.sv_previsao_entrega = previsaoEntrega;


        var servicoMapper = _mapper.Map<tb_servicos>(servico);

        var condicaoFisica = _clienteCompleto.condicaoFisica;
        var condicaoFisicaMapper = _mapper.Map<tb_condicoes_fisicas>(condicaoFisica);

        var checkList = _clienteCompleto.checkList;
        var checkListMapper = _mapper.Map<tb_checklist>(checkList);

        servicoMapper.sv_data = dataHoje;
        servicoService.repositoryServico.Adicionar(servicoMapper);

        condicaoFisicaMapper.cf_sv_idservico = servicoMapper.sv_id;
        condicaoFisicaMapper.cf_data = dataHoje;
        condicoesFisicasService.repositoryCondicoesFisicas.Adicionar(condicaoFisicaMapper);

        checkListMapper.ch_sv_idservico = servicoMapper.sv_id;
        checkListMapper.ch_data = dataHoje;
        checkListService.repositoryChecklist.Adicionar(checkListMapper);

        servicoService.repositoryServico.SalvarModificacoes();
        condicoesFisicasService.repositoryCondicoesFisicas.SalvarModificacoes();
        checkListService.repositoryChecklist.SalvarModificacoes();
      }
      catch (System.Exception e)
      {
        Console.WriteLine("Erro ao adicionar servico: " + e);
        return BadRequest("Erro ao adicionar servico: " + e);
      }
      System.Console.WriteLine("Servico adicionado com sucesso");
      TempData["AlertMessage"] = "Servi??o adicionado!";
      return RedirectToAction("Index");
    }

    private DateTime? PegarPrevisaoEntrega(string _checkBoxSelecionada)
    {
      switch (_checkBoxSelecionada)
      {
        case "chkSemPrevia":
          return null;

        case "chk24":
          return DateTime.Now.AddDays(1);

        case "chk2":
          return DateTime.Now.AddDays(2);

        case "chk3":
          return DateTime.Now.AddDays(3);

        case "chk4":
          return DateTime.Now.AddDays(4);

        case "chk5":
          return DateTime.Now.AddDays(5);

        case "chk7":
          return DateTime.Now.AddDays(7);

        default:
          return null;
      }
    }


    [Route("Servico/BuscarClientes/{nome?}")]
    public JsonResult BuscarClientes(string? nome)
    {
      try
      {
        var clientes = clienteService.repositoryCliente.BuscarPorNome(nome);
        Console.WriteLine("Clientes: " + clientes.Count());
        return Json(clientes);
      }
      catch (System.Exception)
      {
        System.Console.WriteLine("Erro ao buscar clientes");
      }
      return Json("Nao encontrou clientes");
    }



    [Route("Servico/Editar/{id?}")]
    public IActionResult EditarServico(int? id, ServicoCompletoModel _servicoCompleto, IFormCollection form)
    {
      #region Essa ?? a parte que o usuario clicou em editar no grid e vai chamar a view para edi????o
      if (id.HasValue) //
      {
        ServicoCompletoModel servicoEditar = new ServicoCompletoModel();
        servicoEditar = servicoService.repositoryServico.BuscarServicoCompleto(id.Value);

        System.Console.WriteLine("Checklist ID: " + servicoEditar.checkList.ch_id);

        if (servicoEditar.servico.sv_senha_pattern != null)
        {
          byte[] imageBytes = (byte[])servicoEditar.servico.sv_senha_pattern;
          string base64String = Convert.ToBase64String(imageBytes);
          string imageSrc = String.Format("data:image/jpg;base64,{0}", base64String);
          ViewData["imageSrc"] = imageSrc;
        }

        ViewData["ListasItems"] = CarregarListagemItems(); //Lista com as opcoes que o usuario vai escolher no form de checklist

        return View(servicoEditar);
      }
      #endregion

      #region Essa parte j?? ?? a final, o usuario ja editou as informa????es e deu submit no formulario

      //Removendo esses itens da verifica????o porque alterando o servi??o, pouco importa se esses campos est??o vazios.
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

    [HttpPost]
    public IActionResult InserirAtualizarChecklist(ChecklistDTO checklist, IFormCollection form)
    {
      System.Console.WriteLine("Checklist FOREIGN: " + checklist.ch_sv_idservico);
      try
      {
        System.Console.WriteLine("Try");
        if (checklist.ch_id != 0)
        {
          System.Console.WriteLine("Atualizando");
          var tbChecklist = _mapper.Map<tb_checklist>(checklist);
          checkListService.repositoryChecklist.Atualizar(tbChecklist);
          checkListService.repositoryChecklist.SalvarModificacoes();
          return Ok();
        }
        else
        {
          System.Console.WriteLine("Adicionando");
          var tbChecklist = _mapper.Map<tb_checklist>(checklist);
          tbChecklist.ch_data = DateTime.Now;
          checkListService.repositoryChecklist.Adicionar(tbChecklist);
          checkListService.repositoryChecklist.SalvarModificacoes();
          return Ok();
        }
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine("Erro ao inserir ou atualizar checklist: " + ex);
        return BadRequest();
      }
    }

    [HttpGet]
    public JsonResult ListaServicos()
    {
      List<ServicoComNomeClienteStruct> listServicos = servicoService.repositoryServico.BuscarServicosComCliente();
      return Json(listServicos);
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

    public ListasItems CarregarListagemItems()
    {
      var model = new ListasItems();
      model.InMarcaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "INFOS_APARELHO_ITEM", "Marca");
      model.InCorItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "INFOS_APARELHO_ITEM", "Cor");
      model.CfPeliculaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "Pelicula");
      model.CfTelaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "Tela");
      model.CfTampaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "Tampa");
      model.CfAroItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "Aro");
      model.CfBotoesItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "Botoes");
      model.CfLenteCamItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CONDICOES_FISICAS_ITEM", "LenteCamera");

      //Checklist
      model.ChBiometriaFaceItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Biometria");
      model.ChMicrofoneItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Microfone");
      model.ChTelaItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Tela");
      model.ChChipItemsList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Chip");
      model.ChBotoesList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Botoes");
      model.ChSensorList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Sensor");
      model.ChCamerasList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Cameras");
      model.ChAuricularList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Auricular");
      model.ChWifiList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Wifi");
      model.ChAltoFalanteList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Altofalante");
      model.ChBluetoothList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Bluetooth");
      model.ChCarregamentoList = itemService.repositoryItem.BuscarItems("ORDEM_SERVICO", "CHECKLIST_ITEM", "Carregamento");



      return model;
    }
  }
}