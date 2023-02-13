using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using standby_data.Models.UtilModels;
using standby_data.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using standby_data.Models;

namespace StandBy_WEB.Controllers
{
  // [Route("[controller]")]
  public class LoginController : Controller
  {
    // private readonly SignInManager<IdentityUser> _signInManager;
    private LoginService loginService = new LoginService();

    public IActionResult Index()
    {
      ClaimsPrincipal claimUser = HttpContext.User;
      if (claimUser.Identity.IsAuthenticated)
      {
        Console.WriteLine("USUARIO AUTENTICADO");
        Console.WriteLine("Redirecionando para o dashboard");
        return RedirectToAction("Index", "DashBoard");
      }

      System.Console.WriteLine("Acessando a INDEX, Faça o login");
      string usuario = HttpContext.Session.GetString("Usuario");
      string senha = HttpContext.Session.GetString("Senha");
      string lembrar = HttpContext.Session.GetString("Lembrar");

      Console.WriteLine($"Usuario: {usuario}" + $"\nSenha: {senha} " + $" \nLembrar: {lembrar}");

      ViewData["Usuario"] = usuario;
      ViewData["Senha"] = senha;
      ViewData["Lembrar"] = Convert.ToBoolean(lembrar);
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(InformacoesLoginModel model)
    {
      if (ValidarLogin(model))
      {
        if (model.lembrarInfos)
        {
          HttpContext.Session.SetString("Usuario", model.lg_usuario);
          HttpContext.Session.SetString("Senha", model.lg_senha);
          HttpContext.Session.SetString("Lembrar", model.lembrarInfos.ToString());
          Console.WriteLine("Informacoes gravadas na session");
        }

        System.Console.WriteLine("Model Validada");
        List<Claim> claims = new List<Claim>(){
          new Claim(ClaimTypes.NameIdentifier, model.lg_usuario),
          new Claim("OtherProperties","Example Role")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        AuthenticationProperties properties = new AuthenticationProperties()
        {
          AllowRefresh = true,
          IsPersistent = model.lembrarInfos,
        };

        System.Console.WriteLine("Chamando SigninAsync");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        return RedirectToAction("Index", "Dashboard", new { _usuarioLogado = model.lg_usuario });
      }


      ViewData["LoginIncorreto"] = "d-none";
      return View("Index");
    }

    public async Task<IActionResult> LogOut()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return RedirectToAction("Index", "Login");
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