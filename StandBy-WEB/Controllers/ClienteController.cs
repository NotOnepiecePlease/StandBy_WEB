using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using standby_data.Enums;
using standby_data.Models;
using standby_data.Models.DTOs;
using standby_data.Models.UtilModels;
using standby_data.Services;

namespace StandBy_WEB.Controllers
{
  [Authorize]
  public class ClienteController : Controller
  {
    private ClienteService clienteService = new ClienteService();
    private readonly IMapper _mapper;
    public ClienteController(IMapper mapper)
    {
      _mapper = mapper;
    }
    public IActionResult Index()
    {
      return View("Index");
    }

    #region Adicionar cliente VIEW
    public IActionResult AddCliente()
    {
      return View("AddCliente");
    }
    #endregion

    [Route("Cliente/Editar/{id?}")]
    public IActionResult EditarCliente1(int? id, ClienteDTO? _cliente = null)
    {
      #region Essa parte é chamada quando o usuário clica no botão de editar na tabela
      System.Console.WriteLine("ID: " + id);
      System.Console.WriteLine("Cliente: " + _cliente.cl_id);
      if (id.HasValue && _cliente.cl_id == 0)
      {
        var clienteEdit = clienteService.repositoryCliente.BuscarPorID(id.Value);
        var clienteDTO = _mapper.Map<ClienteDTO>(clienteEdit); //Transformo tb_clientes em ClienteDTO
        System.Console.WriteLine("Editando cliente: " + _cliente.cl_nome);
        System.Console.WriteLine("-----------------------------");
        return View("EditClienteView", clienteDTO);
      }
      #endregion

      #region Essa parte é chamada quando o usuário clica no botão de salvar dentro da tela de edição e retorna erros
      if (!ModelState.IsValid)
      {
        foreach (var modelState in ViewData.ModelState.Values)
        {
          foreach (var error in modelState.Errors)
          {
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
          }
        }

        System.Console.WriteLine("Segundo IF, deu erro");
        System.Console.WriteLine("-----------------------------");
        return View("EditClienteView", _cliente);
      }
      #endregion

      #region Essa parte é chamada quando o usuário clica no botão de salvar dentro da tela de edição e retorna sucesso
      var cliente = _mapper.Map<tb_clientes>(_cliente);//Transformo ClienteDTO em tb_clientes
      System.Console.WriteLine("Salvando cliente: " + cliente.cl_nome);
      System.Console.WriteLine("-----------------------------");
      clienteService.repositoryCliente.Atualizar(cliente);
      clienteService.repositoryCliente.SalvarModificacoes();
      return RedirectToAction("Index");
      #endregion
    }

    #region Lista de clientes usado para popular o grid mas usando ajax
    public JsonResult ListaClientes()
    {
      var clientes = GetClientes();
      return Json(clientes);
    }
    #endregion

    #region Pegar a lista de clientes do banco
    private List<tb_clientes> GetClientes()
    {
      List<tb_clientes> listCliente = clienteService.repositoryCliente.BuscarTodos();
      return listCliente;
    }
    #endregion

    #region Editar cliente com id #DESCARTADO
    // public IActionResult Edit(int id)
    // {
    //   tb_clientes cliente = clienteService.repositoryCliente.BuscarPorID(id);
    //   return PartialView("EditClienteView", cliente);
    //   //return PartialView("_EditClientePartialView", cliente);
    // }
    #endregion

    #region Deletar registro especifico
    public async Task<IActionResult> Delete(int _id)
    {
      try
      {
        var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
        return Ok($"Tem certeza que deseja deletar {_cliente.cl_nome}?");
      }
      catch (Exception e)
      {
        var erroTraduzido = await TraduzirTexto(e.Message);
        return BadRequest(new BadRequestErrorModel
        {
          CustomMessage = $"Algo inesperado aconteceu",
          ErrorMessage = $"{erroTraduzido}"
        });
      }
    }

    public static async Task<string> TraduzirTexto(string text)
    {
      using (var client = new HttpClient())
      {
        // var response = await client.GetAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=pt-br&dt=t&q={Uri.EscapeUriString(text)}");

        var response =
            await client.GetAsync(
                $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=pt-br&dt=t&q={Uri.EscapeDataString(text)}");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        var startIndex = result.IndexOf("\"") + 1;
        var endIndex = result.IndexOf("\"", startIndex);
        return result.Substring(startIndex, endIndex - startIndex);
      }
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int _id)
    {
      List<tb_clientes> listClienteNaoPodeSerDeletado = new List<tb_clientes>();
      bool isExisteServicoVinculado = clienteService.repositoryCliente.SeExisteServicoVinculado(_id);
      string clienteDeletado = "";
      if (isExisteServicoVinculado == false)
      {
        var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
        clienteDeletado = _cliente.cl_nome;
        clienteService.repositoryCliente.Deletar(_cliente);
        clienteService.repositoryCliente.SalvarModificacoes();
      }
      else
      {
        var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
        listClienteNaoPodeSerDeletado.Add(_cliente);

        //return PartialView("_ClienteNaoPodeSerDeletadoModalPartialView", listClienteNaoPodeSerDeletado);
        return BadRequest($"{_cliente.cl_nome} possui serviços vinculados =( !");
      }


      return Ok($"{clienteDeletado} foi deletado com sucesso =D !");

      //return PartialView("_DeletadoSucessoModalPartialView");
    }
    #endregion

    #region Deletar registros selecionados
    public IActionResult DeleteSelected(int[] selectedIds)
    {
      try
      {
        if (selectedIds == null || !selectedIds.Any())
        {
          return BadRequest();
        }

        List<tb_clientes> listClienteNaoPodeSerDeletado = new List<tb_clientes>();
        List<tb_clientes> listClienteForamDeletados = new List<tb_clientes>();
        foreach (var id in selectedIds)
        {
          bool isExisteServicoVinculado = clienteService.repositoryCliente.SeExisteServicoVinculado(id);
          if (isExisteServicoVinculado == false)
          {
            var cliente = clienteService.repositoryCliente.BuscarPorID(id);
            listClienteForamDeletados.Add(cliente);
            clienteService.repositoryCliente.Deletar(cliente);
            clienteService.repositoryCliente.SalvarModificacoes();
          }
          else
          {
            listClienteNaoPodeSerDeletado.Add(clienteService.repositoryCliente.BuscarPorID(id));
          }
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("Clientes deletados: ");
        sb.AppendLine("<br>");
        if (listClienteForamDeletados.Count > 0)
        {
          foreach (var item in listClienteForamDeletados)
          {
            sb.AppendLine($"<a class=\"text-success\">{item.cl_nome}</a>");
          }
        }
        else
        {
          sb.AppendLine($"<a class=\"text-success\">Nenhum.</a>");
        }

        sb.AppendLine("<br>");
        sb.AppendLine("<br>");

        //So vai acontecer se achar algum cliente q nao pode ser deletado

        #region Cliente nao pode ser deletado
        if (listClienteNaoPodeSerDeletado.Count > 0)
        {
          //return StatusCode(500, "Ocorreu um erro interno no servidor.");
          sb.Append("Existem serviços vinculados, impossivel deletar:");
          sb.AppendLine("<br>");
          foreach (var item in listClienteNaoPodeSerDeletado)
          {
            sb.Append($"<a class=\"text-warning\">{item.cl_nome}</a><br>");
          }

          return Ok($"{sb}");

          //return PartialView("_ClienteNaoPodeSerDeletadoModalPartialView", listClienteNaoPodeSerDeletado);
        }
        #endregion

        List<tb_clientes> listCliente = clienteService.repositoryCliente.BuscarTodos();
        return PartialView("Index", listCliente);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return StatusCode(500, "Ocorreu um erro interno no servidor.");
      }
    }
    #endregion
  }
}