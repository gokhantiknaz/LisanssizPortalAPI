using AutoMapper;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Core.Mapper
{

    public class SaatlikEndeksProfile : Profile
    {
        public SaatlikEndeksProfile()
        {
            var gmtPlus3 = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            CreateMap<SaatlikEndeksRequest, AboneSaatlikEndeks>()
           .ForMember(dest => dest.CekisTuketim, opt => opt.MapFrom(src => src.TuketimCekis))
           .ForMember(dest => dest.CekisReaktifInduktif, opt => opt.MapFrom(src => src.ReakIndCekis))
           .ForMember(dest => dest.CekisReaktifKapasitif, opt => opt.MapFrom(src => src.ReakKapCekis))
           .ForMember(dest => dest.VerisReaktifInduktif, opt => opt.MapFrom(src => src.ReakIndVeris))
           .ForMember(dest => dest.VerisReaktifKapasitif, opt => opt.MapFrom(src => src.ReakKapVeris))
           .ForMember(dest => dest.Uretim, opt => opt.MapFrom(src => src.UretimVeris))
           .ForMember(dest => dest.Donem, opt => opt.MapFrom(src => src.Donem))
           .ForMember(dest => dest.ProfilDate, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(src.ProfilTarihi), gmtPlus3)))
           .ForMember(dest => dest.AboneId, opt => opt.MapFrom(src => src.AboneId))
           .ForMember(dest => dest.Carpan, opt => opt.MapFrom(src => src.Carpan)).ReverseMap();

            CreateMap<AboneSaatlikEndeks, SaatlikEndeksRes>()
       .ForMember(dest => dest.TuketimCekis, opt => opt.MapFrom(src => src.CekisTuketim))
       .ForMember(dest => dest.ReakIndCekis, opt => opt.MapFrom(src => src.CekisReaktifInduktif))
       .ForMember(dest => dest.ReakKapCekis, opt => opt.MapFrom(src => src.CekisReaktifKapasitif))
       .ForMember(dest => dest.ReakIndVeris, opt => opt.MapFrom(src => src.VerisReaktifInduktif))
       .ForMember(dest => dest.ReakKapVeris, opt => opt.MapFrom(src => src.VerisReaktifKapasitif))
       .ForMember(dest => dest.UretimVeris, opt => opt.MapFrom(src => src.Uretim))
       .ForMember(dest => dest.Donem, opt => opt.MapFrom(src => src.Donem))
       .ForMember(dest => dest.ProfilTarihi, opt => opt.MapFrom(src => src.ProfilDate.ToLocalTime().ToString()))
       .ForMember(dest => dest.AboneId, opt => opt.MapFrom(src => src.AboneId))
       .ForMember(dest => dest.Carpan, opt => opt.MapFrom(src => src.Carpan)).ReverseMap();
        }
    }
    public class AylikEndeksProfile : Profile
    {
        public AylikEndeksProfile()
        {
            CreateMap<AboneEndeks, AylikEndeksRes>().
                ForMember(dest => dest.Donem, opt => opt.MapFrom(src => src.EndexYear.ToString() + "/" + src.EndexMonth.ToString().PadLeft(2, '0'))).ReverseMap();

            CreateMap<AboneEndeks, EndexData>().ReverseMap();
        }
    }
}
