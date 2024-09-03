using Humanity.Application.Interfaces;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class CariKartDTO
    {
        private readonly ICariKartService _cariKartService;

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Durum { get; set; }

        public string Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }


        public GercekTuzel GercekTuzel { get; set; }
        public CariIletisimDTO CariIletisim { get; set; }

        public CariKartDTO(Humanity.Domain.Entities.CariKart cari)
        {
            Id = cari.Id;
            Adi = cari.Adi;
            Soyadi = cari.Soyadi;
            Unvan = cari.Unvan;
            Durum = cari.Durum.GetHashCode();
            Tckn = cari.Tckn;
            Vkn = cari.Vkn;
            GercekTuzel = cari.GercekTuzel;
            CariIletisim = new CariIletisimDTO();
        }
    }


    public class CariIletisimDTO
    {
        private CariIletisim cariIletisim;


        public CariIletisimDTO()
        {

        }

        public CariIletisimDTO(CariIletisim cariIletisim)
        {
            this.cariIletisim = cariIletisim;
            IletisimId = cariIletisim.IletisimId;
            MusteriId = cariIletisim.CariKartId;
            CepTel = cariIletisim.Iletisim.CepTel;
            Email = cariIletisim.Iletisim.Email;
            Ilid = cariIletisim.Iletisim.Ilid;
            Ilceid = cariIletisim.Iletisim.Ilceid;
            Adres = cariIletisim.Iletisim.Adres;
        }

        public int IletisimId { get; set; }

        public int MusteriId { get; set; }
        public string? CepTel { get; set; }
        public string? Email { get; set; }
        public int? Ilid { get; set; }
        public int? Ilceid { get; set; }
        public string? Adres { get; set; }
    }
}
