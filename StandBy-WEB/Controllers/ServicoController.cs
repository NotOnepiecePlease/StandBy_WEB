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

    [Route("Servico/Editar/{id?}")]
    public IActionResult EditarServico(int? id, ServicoCompletoModel _servicoCompleto, IFormCollection form)
    {
      try
      {
        var imageData = form["ImageData"].ToString();

        // Converte a string base64 em um array de bytes
        byte[] imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);

        // Armazena a imagem na coluna sv_senha_pattern
        var senhaPattern = imageBytes;

        tb_servicos servico = servicoService.repositoryServico.BuscarPorID(33943);
        servico.sv_senha_pattern = senhaPattern;
        servicoService.repositoryServico.Atualizar(servico);
        servicoService.repositoryServico.SalvarModificacoes();
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }



      if (id.HasValue)
      {
        _servicoCompleto = servicoService.repositoryServico.BuscarServicoCompleto(id.Value);

        byte[] imageBytes = (byte[])_servicoCompleto.servico.sv_senha_pattern;
        string base64String = Convert.ToBase64String(imageBytes);
        string imageSrc = String.Format("data:image/jpg;base64,{0}", base64String);

        ViewData["imageSrc"] = imageSrc;

        return View(_servicoCompleto);
      }

      if (!ModelState.IsValid)
      {
        ModelState.AddModelError(string.Empty, GetModelStateErrorString());
        return View("EditarServico", _servicoCompleto);
      }

      try
      {
        //Futuramente vai quer te atualizar Cond e CH
        servicoService.repositoryServico.Atualizar(_servicoCompleto.servico);
        servicoService.repositoryServico.SalvarModificacoes();
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, ex.Message);
        System.Console.WriteLine(ex.Message);
        return View("EditarServico", _servicoCompleto);
      }

      return RedirectToAction("Index");
    }

    // [Route("Servico/Editar/{id?}")]
    // public IActionResult EditarServico(int? id, tb_servicos servico = null)
    // {
    //   if (!id.HasValue && servico == null)
    //   {
    //     return RedirectToAction("Index");
    //   }

    //   servico = ObterServicoPorId(id.Value);

    //   if (!ModelState.IsValid)
    //   {
    //     ModelState.AddModelError(string.Empty, GetModelStateErrorString());
    //     return View("EditarServico", servico);
    //   }

    //   try
    //   {
    //     servico.sv_ordem_serv = 000;
    //     servico.sv_cl_idcliente = 22;
    //     servico.sv_aparelho = "Ae boy";
    //     servico.sv_ativo = 1;
    //     servicoService.repositoryServico.Atualizar(servico);
    //     servicoService.repositoryServico.SalvarModificacoes();
    //   }
    //   catch (Exception ex)
    //   {
    //     ModelState.AddModelError(string.Empty, ex.Message);
    //     return View("EditarServico", servico);
    //   }

    //   return RedirectToAction("Index");
    // }
    public IActionResult Index()
    {
      var servicos = servicoService.repositoryServico.BuscarTodos();
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