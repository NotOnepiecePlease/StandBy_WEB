using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using standby_data.Models.DTOs;

namespace standby_data.Models.UtilModels
{
  //[Bind(include: "cl_id, cl_nome, cl_telefone, cl_telefone_recado")]
  public class ServicoCompletoModel
  {
    public tb_servicos servico { get; set; } = null!;
    public tb_clientes cliente { get; set; } = null!;
    public tb_condicoes_fisicas condicaoFisica { get; set; } = new tb_condicoes_fisicas();
    public ChecklistDTO checkList { get; set; } = new ChecklistDTO();
  }
}