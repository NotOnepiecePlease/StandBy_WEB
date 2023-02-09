using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Repositories;
namespace standby_data.Services
{
  public class ServicoService
  {
    public RepositoryServico repositoryServico { get; set; }

    public ServicoService()
    {
      repositoryServico = new RepositoryServico();
    }
  }
}