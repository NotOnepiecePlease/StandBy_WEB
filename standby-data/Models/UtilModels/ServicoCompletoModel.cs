using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace standby_data.Models.UtilModels
{
  public class ServicoCompletoModel
  {
    public tb_servicos servico { get; set; } = null!;
    public tb_clientes cliente { get; set; } = null!;
    public tb_condicoes_fisicas condicaoFisica { get; set; } = new tb_condicoes_fisicas();
    public tb_checklist checkList { get; set; } = new tb_checklist();
  }
}