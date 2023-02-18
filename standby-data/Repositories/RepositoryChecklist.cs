using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Interfaces;

namespace standby_data.Repositories
{
  public class RepositoryChecklist : RepositoryBase<tb_checklist>, IRepositoryCheckList
  {
    public RepositoryChecklist(bool _saveChanges = true) : base(_saveChanges)
    {
    }

  }
}