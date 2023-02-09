using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Interfaces;

namespace standby_data.Repositories
{
  public class RepositoryLogin : RepositoryBase<tb_login>, IRepositoryLogin
  {
    public RepositoryLogin(bool _saveChanges = true) : base(_saveChanges)
    {

    }

    public bool ValidarLogin(InformacoesLoginModel model)
    {
      var usuarioExistente = context.tb_login
      .Where(x => x.lg_usuario == model.lg_usuario && x.lg_senha == model.lg_senha)
      .Any();
      return usuarioExistente;
    }
  }
}