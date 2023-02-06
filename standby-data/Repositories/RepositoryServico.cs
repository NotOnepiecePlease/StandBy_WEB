using standby_data.Interfaces;
using standby_data.Models;

namespace standby_data.Repositories
{
    public class RepositoryServico : RepositoryBase<tb_servicos>, IRepositoryServico
    {
        public RepositoryServico(bool _saveChanges = true) : base(_saveChanges)
        {
        }
    }
}