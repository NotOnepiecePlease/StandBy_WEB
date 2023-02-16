using standby_data.Interfaces;
using standby_data.Models;

namespace standby_data.Repositories
{
  public class RepositoryCliente : RepositoryBase<tb_clientes>, IRepositoryCliente
  {
    public RepositoryCliente(bool _saveChanges = true) : base(_saveChanges)
    {
    }

    public bool SeExisteServicoVinculado(int _idCliente)
    {
      bool isExisteServico = context.tb_servicos.Any(x => x.sv_cl_idcliente == _idCliente);
      return isExisteServico;
    }

    public List<tb_clientes> BuscarPorNome(string _nome)
    {
      return context.tb_clientes.Where(x => x.cl_nome.Contains(_nome)).OrderBy(y => y.cl_nome).ToList();
    }
  }
}