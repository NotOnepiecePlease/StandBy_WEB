using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Repositories;
using standby_data.Models;
namespace standby_data.Services
{
  public class LoginService
  {
    public RepositoryLogin repositoryLogin { get; set; }

    public LoginService()
    {
      repositoryLogin = new RepositoryLogin();
    }

    
  }
}