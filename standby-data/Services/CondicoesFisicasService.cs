using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using standby_data.Repositories;
using standby_data.Models;
namespace standby_data.Services
{
  public class CondicoesFisicasService
  {
    public RepositoryCondicoesFisicas repositoryCondicoesFisicas { get; set; }

    public CondicoesFisicasService()
    {
      repositoryCondicoesFisicas = new RepositoryCondicoesFisicas();
    }
  }
}