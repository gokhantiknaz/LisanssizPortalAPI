using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.ListDTOS
{
    public class MusteriTableDTO
    {

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string? Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }

        public AboneDTO Abone { get; set; }

        private List<TuketiciTableDTO> GetTuketiciler()
        {
            return new List<TuketiciTableDTO>();
        }

        public MusteriTableDTO() { }

        public MusteriTableDTO(Humanity.Domain.Entities.Musteri musteri)
        {
            Id = musteri.Id;
            Adi = musteri.Adi;
            Soyadi = musteri.Soyadi;
            Unvan = musteri.Unvan;
            Tckn = musteri.Tckn;
            Vkn = musteri.Vkn;
        }
    }
}
