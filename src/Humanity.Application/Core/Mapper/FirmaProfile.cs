using AutoMapper;
using Humanity.Application.Models.DTOs.firma;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Core.Mapper
{
    public class FirmaProfile : Profile
    {
        public FirmaProfile()
        {
            CreateMap<Firma, FirmaDTO>().ReverseMap();
            CreateMap<MusteriEntegrasyon, MusteriEntegrasyonDTO>().ReverseMap();
        }
    }

}
