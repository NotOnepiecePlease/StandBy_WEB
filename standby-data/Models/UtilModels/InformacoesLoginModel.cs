using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace standby_data.Models.UtilModels
{
  public class InformacoesLoginModel
  {
    public string lg_usuario { get; set; }
    public string lg_senha { get; set; }
    public bool lembrarInfos { get; set; }
  }
}