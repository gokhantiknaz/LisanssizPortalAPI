using Humanity.Application.Models.DTOs.firma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class FirmaDTO
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public string FirmaUnvan { get; set; }
        public int Durum { get; set; }

  

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }


        public GercekTuzel GercekTuzel { get; set; }
        public FirmaIletisimDTO FirmaIletisim { get; set; }

        public FirmaDTO(Humanity.Domain.Entities.Firma firma)
        {
            Id = firma.Id;
            FirmaAdi = firma.FirmaAdi;
            FirmaUnvan = firma.FirmaUnvan;
            Durum = firma.Durum.GetHashCode();
            Tckn = firma.Tckn;
            Vkn = firma.Vkn;
            GercekTuzel = firma.GercekTuzel;
            FirmaIletisim = new FirmaIletisimDTO();
        }
    }
}
