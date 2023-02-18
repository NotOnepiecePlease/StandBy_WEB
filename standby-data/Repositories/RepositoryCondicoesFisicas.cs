using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Interfaces;

namespace standby_data.Repositories
{
  public class RepositoryCondicoesFisicas : RepositoryBase<tb_condicoes_fisicas>, IRepositoryCondicoesFisicas
  {
    public RepositoryCondicoesFisicas(bool _saveChanges = true) : base(_saveChanges)
    {
    }
  }
}