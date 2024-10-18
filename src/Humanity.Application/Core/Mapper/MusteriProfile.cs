using AutoMapper;
using Humanity.Application.Models.DTOs.firma;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Core.Mapper
{
    public class MusteriProfile : Profile
    {
        public MusteriProfile()
        {
            CreateMap<Musteri, MusteriDTO>().ReverseMap();
            CreateMap<Musteri, CreateMusteriReq>().ReverseMap();
            CreateMap<Musteri, CreateMusteriRes>().ReverseMap();
            CreateMap<MusteriIletisim, MusteriIletisimDTO>().ReverseMap();
        }
    }
}
