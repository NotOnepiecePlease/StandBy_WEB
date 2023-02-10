using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StandBy_WEB.Controllers
{
  public class UsuarioController : Controller
  {
    public IActionResult Configuracoes()
    {

      return View();
    }
  }
}