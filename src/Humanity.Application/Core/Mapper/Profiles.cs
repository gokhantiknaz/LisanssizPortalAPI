using AutoMapper;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.ListDTOS;
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
            CreateMap<Abone, CustomerSubscription>()
                 .ForMember(dest => dest.GroupInfo, opt => opt.MapFrom(src => src.Adi))
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Unvan))
                 .ForMember(dest => dest.MinInductiveRate, opt => opt.MapFrom(src => src.SozlesmeGucu))
                 .ForMember(dest => dest.IdentifierValue, opt => opt.MapFrom(src => src.EtsoKodu))
                 .ForMember(dest => dest.InstalledPower, opt => opt.MapFrom(src => src.KuruluGuc))
                 .ForMember(dest => dest.AccordPower, opt => opt.MapFrom(src => src.BaglantiGucu))
                 .ForMember(dest => dest.Multiplier, opt => opt.MapFrom(src => src.Carpan))
                 .ForMember(dest => dest.SubscriptionSerno, opt => opt.MapFrom(src => src.SeriNo)).ReverseMap();

            CreateMap<TuketiciTableDTO, AboneTuketici>().ReverseMap();

        }
    }

}
