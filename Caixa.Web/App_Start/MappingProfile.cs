using AutoMapper;
using Caixa.Web.DTOs;
using Caixa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caixa.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Acerto, AcertoDTO>();
            Mapper.CreateMap<AcertoDTO, Acerto>();
            Mapper.CreateMap<Estabelecimentos, EstabelecimentoDTO>();
            Mapper.CreateMap<EstabelecimentoDTO, Estabelecimentos>();
        }
    }
}