using standby_data.Interfaces;
using standby_data.Models;
using standby_data.Models.DTOs;
using standby_data.Models.UtilModels;

namespace standby_data.Repositories
{
  public class RepositoryServico : RepositoryBase<tb_servicos>, IRepositoryServico
  {
    public struct ServicoComNomeClienteStruct
    {
      public tb_servicos Servicos { get; set; }
      public string cl_nome { get; set; }

      // public int? diferenca
      // {
      //     get
      //     {
      //         if (Servicos.sv_previsao_entrega == null)
      //         {
      //             return null;
      //         }

      //         return (Convert.ToDateTime(Servicos.sv_previsao_entrega) - Servicos.sv_data).Days;
      //     }
      // }

      public double? diferenca
      {
        get
        {
          if (Servicos.sv_previsao_entrega == null)
          {
            return null;
          }

          return Convert.ToDateTime(Servicos.sv_previsao_entrega).Subtract(DateTime.Now).TotalSeconds;
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
          .OrderByDescending(ti =>
              (Convert.ToDateTime(ti.Servicos.sv_previsao_entrega) - ti.Servicos.sv_data).Days)
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
            sv_ordem_serv = servico.sv_ordem_serv
              ,
            sv_defeito = servico.sv_defeito,
            sv_aparelho = servico.sv_aparelho,
            sv_status = servico.sv_status
              ,
            sv_previsao_entrega = servico.sv_previsao_entrega
          }).Where(x => x.sv_status == 1).ToList();
      return result;
    }

    public ServicoCompletoModel BuscarServicoCompleto(int _id)
    {
      var result = context.tb_servicos.Join(context.tb_clientes,
          servico => servico.sv_cl_idcliente,
          cliente => cliente.cl_id,
          (servico, cliente) => new ServModel
          {
            servico = servico,
            cliente = cliente,
            condicaoFisica = context.tb_condicoes_fisicas.Where(x => x.cf_sv_idservico == servico.sv_id).FirstOrDefault(),
            checkList = context.tb_checklist.Where(x => x.ch_sv_idservico == servico.sv_id && x.ch_tipo == "ENTRADA").FirstOrDefault()
          }).Where(x => x.servico.sv_id == _id).FirstOrDefault();

      var servicoCompleto = new ServicoCompletoModel();
      servicoCompleto.servico = result.servico;
      servicoCompleto.cliente = result.cliente;
      servicoCompleto.condicaoFisica = result.condicaoFisica;
      servicoCompleto.checkList = ConvertToDTO(result.checkList);
      return servicoCompleto;
    }

    public struct ServModel
    {
      public ServModel()
      {
      }
      public tb_servicos servico { get; set; } = null!;
      public tb_clientes cliente { get; set; } = null!;
      public tb_condicoes_fisicas condicaoFisica { get; set; } = new tb_condicoes_fisicas();
      public tb_checklist checkList { get; set; } = new tb_checklist();
    }

    public ChecklistDTO ConvertToDTO(tb_checklist _checklist)
    {
      var checklistDTO = new ChecklistDTO();
      if (_checklist != null)
      {
        checklistDTO.ch_id = _checklist.ch_id;
        checklistDTO.ch_ordem_serv = _checklist.ch_ordem_serv;
        checklistDTO.ch_data = _checklist.ch_data;
        checklistDTO.ch_sv_idservico = _checklist.ch_sv_idservico;
        checklistDTO.ch_tipo = _checklist.ch_tipo;
        checklistDTO.ch_biometria_faceid = _checklist.ch_biometria_faceid;
        checklistDTO.ch_microfone = _checklist.ch_microfone;
        checklistDTO.ch_tela = _checklist.ch_tela;
        checklistDTO.ch_chip = _checklist.ch_chip;
        checklistDTO.ch_botoes = _checklist.ch_botoes;
        checklistDTO.ch_sensor = _checklist.ch_sensor;
        checklistDTO.ch_cameras = _checklist.ch_cameras;
        checklistDTO.ch_auricular = _checklist.ch_auricular;
        checklistDTO.ch_wifi = _checklist.ch_wifi;
        checklistDTO.ch_altofalante = _checklist.ch_altofalante;
        checklistDTO.ch_bluetooth = _checklist.ch_bluetooth;
        checklistDTO.ch_carregamento = _checklist.ch_carregamento;
        checklistDTO.ch_observacoes = _checklist.ch_observacoes;
        checklistDTO.ch_ausente = _checklist.ch_ausente;
        checklistDTO.ch_motivo_ausencia = _checklist.ch_motivo_ausencia;
      }
      return checklistDTO;
    }
  }
}