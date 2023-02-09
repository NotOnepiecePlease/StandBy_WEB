using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using standby_data.Models.UtilModels;
using standby_data.Services;

namespace StandBy_WEB.Controllers
{
  // [Route("[controller]")]
  public class LoginController : Controller
  {
    // private readonly ILogger<LoginController> _logger;

    // public LoginController(ILogger<LoginController> logger)
    // {
    //   _logger = logger;

    // }


    LoginService loginService = new LoginService();

    public IActionResult FazerLogin(InformacoesLoginModel model)
    {
      if (ValidarLogin(model))
      {
        if (model.lembrarInfos)
        {
          HttpContext.Session.SetString("Usuario", model.lg_usuario);
          HttpContext.Session.SetString("Senha", model.lg_senha);
          Console.WriteLine("Informacoes gravadas na session");
        }
        return RedirectToAction("Index", "Dashboard", new { _usuarioLogado = model.lg_usuario });
      }
      ViewData["LoginIncorreto"] = "d-none";
      return View("Index");
    }

    public IActionResult Index()
    {
      Console.WriteLine("Pegando informacoes");
      string usuario = HttpContext.Session.GetString("Usuario");
      string senha = HttpContext.Session.GetString("Senha");

      Console.WriteLine(
        $"Usuario: {usuario}" +
        $"Senha: {senha}");
        
      ViewData["Usuario"] = usuario;
      ViewData["Senha"] = senha;

      return View();
    }

    public bool ValidarLogin(InformacoesLoginModel model)
    {
      var login = loginService.repositoryLogin.ValidarLogin(model);
      if (login)
      {
        return true;
      }
      return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}