using AutoMapper;
using standby_data.Models;
using standby_data.Models.DTOs;

namespace standby_data.AutoMapper
{
  public class MappingModels : Profile
  {
    public MappingModels()
    {
      CreateMap<tb_clientes, ClienteDTO>().ReverseMap();
      CreateMap<tb_condicoes_fisicas, CondicoesFisicasDTO>().ReverseMap();
      CreateMap<tb_servicos, ServicoDTO>().ReverseMap();
      CreateMap<tb_checklist, ChecklistDTO>().ReverseMap();
    }
  }
}