using AutoMapper;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Core.Mapper
{
    public class AboneProfile : Profile
    {
        public AboneProfile()
        {
            CreateMap<Abone, AboneDTO>().ReverseMap();
            CreateMap<AboneIletisim, AboneIletisimDTO>().ReverseMap();
        }
    }
 
}
