using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Services;
using static standby_data.Repositories.RepositoryServico;

namespace StandBy_WEB.Controllers
{
  // [Route("[controller]/[action]")]
  // [Route("[controller]/[action]")]
  [Authorize]
  public class ServicoController : Controller
  {
    private ServicoService servicoService = new ServicoService();
    private ClienteService clienteService = new ClienteService();
    private readonly ILogger<ServicoController> _logger;

    public ServicoController(ILogger<ServicoController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      var servicos = servicoService.repositoryServico.BuscarTodos();
      ThemeDetail themas = new ThemeDetail();
      var teste = servicoService.repositoryServico.BuscarTodos();
      ViewBag.Data = teste;
      ViewData["temas"] = teste;
      // foreach (var item in teste)
      // {
      //   System.Console.WriteLine(item.sv_defeito);
      // }
      return View("Index", servicos);
    }


    [HttpGet]
    public JsonResult ListaServicos()
    {
      System.Console.WriteLine("ListaServicos");
      List<ServicoComNomeClienteStruct> listServicos = ListaServico();
      return Json(listServicos);
    }

    [HttpGet]
    public JsonResult ListaTb_Servicos()
    {
      List<ServicosColunasSelecionadas> listServicos = new List<ServicosColunasSelecionadas>();
      try
      {
        System.Console.WriteLine("ListaServicos");
        listServicos = servicoService.repositoryServico.BuscarServicosSelecionados();

      }
      catch (Exception e)
      {
        _logger.LogError(e.Message);
      }
      return Json(listServicos);
    }

    [HttpGet]
    public JsonResult ListaServicosTeste()
    {
      // System.Console.WriteLine("ListaServicos");
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
  }


}