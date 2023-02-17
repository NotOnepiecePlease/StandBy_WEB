using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
  }
}