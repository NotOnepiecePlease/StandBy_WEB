using standby_data.Repositories;

namespace standby_data.Services
{
    public class ClienteService
    {
        public RepositoryCliente repositoryCliente { get; set; }

        public ClienteService()
        {
            repositoryCliente = new RepositoryCliente();
        }
    }
}