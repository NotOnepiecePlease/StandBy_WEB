using Microsoft.AspNetCore.Mvc;
using standby_data.Context;
using standby_data.Enums;
using standby_data.Models;
using standby_data.Services;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using standby_data.Constantes;
using standby_data.Models.ProcedureModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace StandBy_WEB.Controllers
{
  [Authorize]
  public class DashboardController : Controller
  {
    readonly standby_orgContext context = new standby_orgContext();

    public async Task<IActionResult> Index(string _usuarioLogado)
    {
      System.Console.WriteLine("Exibindo DashBoard\n------------------");
      var valorLucroSemana = await BuscarValorLucrosSemana();
      ViewData["Lucro"] = valorLucroSemana.First().LucroTotalSemana.ToString(CultureInfo.InvariantCulture);

      var valorServicoSemana = await BuscarValorServicoSemana();
      ViewData["ServicoValor"] =
          valorServicoSemana.First().ServicoTotalSemana.ToString(CultureInfo.InvariantCulture);

      var valorPrejuizoSemana = await BuscarValorPrejuizoSemana();
      ViewData["PrejuizoValor"] =
          valorPrejuizoSemana.First().PrejuizoTotalSemana.ToString(CultureInfo.InvariantCulture);

      var valorPecasSemana = await BuscarValorPecasSemana();
      ViewData["PecasValor"] =
          valorPecasSemana.First().GastosPecasTotalSemana.ToString(CultureInfo.InvariantCulture);

      //Calculo da porcentagem
      decimal receita = valorServicoSemana.First().ServicoTotalSemana;
      decimal despesas = valorPecasSemana.First().GastosPecasTotalSemana;

      var lucroPorcentagem = 0.0m;

      if (receita != 0 && despesas != 0)
      {
        lucroPorcentagem = ((receita - despesas) / receita) * 100;
      }

      ViewData["LucroPorcentagem"] = lucroPorcentagem.ToString("N2");

      var aniversariantesMes = BuscarAniversariantesDoMes();

      //var aniversariantesMes = new List<tb_clientes>();

      ViewData["Usuario"] = _usuarioLogado;

      //_logger.LogInformation("Carregando a DashBoard - Index...");
      return View("Index", aniversariantesMes);
    }

    public async Task<IActionResult> PreencherGraficoSemanal()
    {
      //List<tb_servicos> dados = new List<tb_servicos>();
      try
      {
        standby_orgContext context = new standby_orgContext();

        //standby_orgContextProcedures _context = new standby_orgContextProcedures(context);
        //var dados = await _context.ServicosUltimos7DiasV2Async();
        var dados = await context.ServicosUltimaSemana.FromSqlRaw(Querys.ServicoUltimos7Dias).ToListAsync();

        return Json(dados);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      return BadRequest();
    }

    //Buscar  Dados
    private async Task<List<BuscarLucroValorUltimaSemana>> BuscarValorLucrosSemana()
    {
      try
      {
        return await context.BuscarLucroValorUltimaSemana.FromSqlRaw(Querys.ValorLucroUltimos7Dias).ToListAsync();
      }
      catch (System.Exception)
      {
        var result = new List<BuscarLucroValorUltimaSemana>();
        result.Add(new BuscarLucroValorUltimaSemana()
        {
          LucroTotalSemana = 0
        });
        return result;
      }
    }

    private async Task<List<BuscarServicoValorUltimaSemana>> BuscarValorServicoSemana()
    {

      try
      {
        return await context.BuscarServicoValorUltimaSemana.FromSqlRaw(Querys.ValorServicoUltimos7Dias)
            .ToListAsync();
      }
      catch (System.Exception)
      {

        var result = new List<BuscarServicoValorUltimaSemana>();
        result.Add(new BuscarServicoValorUltimaSemana()
        {
          ServicoTotalSemana = 0
        });
        return result;
      }
    }

    private async Task<List<BuscarPrejuizoValorUltimaSemana>> BuscarValorPrejuizoSemana()
    {

      try
      {
        var result = await context.BuscarPrejuizoValorUltimaSemana.FromSqlRaw(Querys.ValorPrejuizoUltimos7Dias)
           .ToListAsync();
        return result;
      }
      catch (System.Exception)
      {
        var result = new List<BuscarPrejuizoValorUltimaSemana>();
        result.Add(new BuscarPrejuizoValorUltimaSemana()
        {
          PrejuizoTotalSemana = 0
        });
        return result;
      }

    }

    private async Task<List<BuscarPecasValorUltimaSemana>> BuscarValorPecasSemana()
    {
      try
      {
        return await context.BuscarPecasValorUltimaSemana.FromSqlRaw(Querys.ValorPecasUltimos7Dias).ToListAsync();
      }
      catch (System.Exception)
      {
        var result = new List<BuscarPecasValorUltimaSemana>();
        result.Add(new BuscarPecasValorUltimaSemana()
        {
          GastosPecasTotalSemana = 0
        });
        return result;
      }
    }

    private List<tb_clientes> BuscarAniversariantesDoMes()
    {
      try
      {
        var clientes1 = context.tb_clientes.ToList();
        var clientes2 = context.tb_servicos.ToList();
        var hoje = DateTime.Today;
        var clientes = context.tb_clientes
            .Where(c => c.cl_data_nascimento.HasValue && c.cl_data_nascimento.Value.Month == hoje.Month)
            .OrderBy(c => Math.Abs((c.cl_data_nascimento.Value.Month - hoje.Month) +
                                   (c.cl_data_nascimento.Value.Day - hoje.Day)))
            .Take(5)
            .ToList();


        //var hoje = DateTime.Today;
        //var clientes = context.tb_clientes
        //    .Where(c => c.cl_data_nascimento != null)
        //    .OrderBy(c => Math.Abs((c.cl_data_nascimento.Value.Month - hoje.Month) +
        //                           (c.cl_data_nascimento.Value.Day - hoje.Day)))
        //    .Take(5)
        //    .ToList();

        return clientes;
      }
      catch (Exception e)
      {
        Console.WriteLine("Erro:\n----------------------" + e.Message);
        return new List<tb_clientes>();
      }
    }
  }
}