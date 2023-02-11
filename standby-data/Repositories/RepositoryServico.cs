using standby_data.Interfaces;
using standby_data.Models;

namespace standby_data.Repositories
{
  public class RepositoryServico : RepositoryBase<tb_servicos>, IRepositoryServico
  {


    public struct ServicoComNomeClienteStruct
    {
      public tb_servicos Servicos { get; set; }
      public string cl_nome { get; set; }
      public int? diferenca
      {
        get
        {
          if (Servicos.sv_previsao_entrega == null)
          {
            return null;
          }
          return (Convert.ToDateTime(Servicos.sv_previsao_entrega) - Servicos.sv_data).Days;
        }
      }
    }

    public struct ServicosColunasSelecionadas
    {
      public int sv_id { get; set; }
      public DateTime sv_data { get; set; }
      public int sv_ordem_serv { get; set; }
      public string sv_defeito { get; set; }
      public string sv_aparelho { get; set; }
      public int? sv_status { get; set; }
      public DateTime? sv_previsao_entrega { get; set; }

    }

    public RepositoryServico(bool _saveChanges = true) : base(_saveChanges)
    {
    }
    public List<ServicoComNomeClienteStruct> BuscarServicosComCliente()
    {
      // var result = context.tb_servicos.Join(context.tb_clientes,
      //                                       servico => servico.sv_cl_idcliente,
      //                                       cliente => cliente.cl_id,
      //                                       (servico, cliente) => new ServicoComNomeClienteStruct
      //                                       {
      //                                         Servicos = servico,
      //                                         cl_nome = cliente.cl_nome
      //                                       }).Where(x => x.Servicos.sv_status == 1).ToList();
      // return result;
      var result = context.tb_servicos
                                .Join(context.tb_clientes,
                                    servico => servico.sv_cl_idcliente,
                                    cliente => cliente.cl_id,
                                    (servico, cliente) => new ServicoComNomeClienteStruct
                                    {
                                      Servicos = servico,
                                      cl_nome = cliente.cl_nome
                                    })
                                .Where(x => x.Servicos.sv_status == 1)
                                .AsEnumerable()
                                .OrderByDescending(ti => (Convert.ToDateTime(ti.Servicos.sv_previsao_entrega) - ti.Servicos.sv_data).Days)
                                .ToList();

      return result;
    }
    public List<ServicosColunasSelecionadas> BuscarServicosSelecionados()
    {
      var result = context.tb_servicos.Join(context.tb_clientes,
                                            servico => servico.sv_cl_idcliente,
                                            cliente => cliente.cl_id,
                                            (servico, cliente) => new ServicosColunasSelecionadas
                                            {
                                              sv_id = servico.sv_id,
                                              sv_data = servico.sv_data,
                                              sv_ordem_serv = servico.sv_ordem_serv,
                                              sv_defeito = servico.sv_defeito,
                                              sv_aparelho = servico.sv_aparelho,
                                              sv_status = servico.sv_status,
                                              sv_previsao_entrega = servico.sv_previsao_entrega

                                            }).Where(x => x.sv_status == 1).ToList();
      return result;
    }

  }
}