using Humanity.Application.Core.Models;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class MusteriDTO
    {
        private readonly IMusteriService _musteriService;

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        public string? Unvan { get; set; }

        public int CariKartId { get; set; }
        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public Status Durum { get; set; }
        public GercekTuzel GercekTuzel { get; set; }
        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }

        public AboneDTO Abone { get; set; }

        public async Task<AboneDTO> AboneBilgiGetir(int id)
        {
           return await  _musteriService.GetAboneById(id);
        }

        public async Task<AboneDTO> AboneBilgiGetirMusteriId(int id)
        {
            return await _musteriService.GetMusteriUreticiAbone(id);
        }

        public UreticiDTO Uretici { get; set; }

        public UreticiDTO GetUretici(Humanity.Domain.Entities.AboneUretici uretici)
        {
            return new UreticiDTO(uretici);
        }

        public MusteriIletisimDTO MusteriIletisim { get; set; }

        public MusteriIletisimDTO GetMusteriIletisim(int musteriId)
        {
            var musteriIletisim = _musteriService.GetMusteriIletisim(musteriId);
            return musteriIletisim.Result;
        }

        public MusteriDTO(IMusteriService musteriService, Humanity.Domain.Entities.AboneUretici aboneuretici)
        {
            _musteriService = musteriService;
            Id = aboneuretici.Abone.Musteri.Id;
            Adi = aboneuretici.Abone.Musteri.Adi;
            Soyadi = aboneuretici.Abone.Musteri.Soyadi;
            Unvan = aboneuretici.Abone.Musteri.Unvan;
            Tckn = aboneuretici.Abone.Musteri.Tckn;
            CariKartId = aboneuretici.Abone.Musteri.CariKartId;
            Vkn = aboneuretici.Abone.Musteri.Vkn;
            GercekTuzel = aboneuretici.Abone.Musteri.GercekTuzel;
            OzelkodId1 = aboneuretici.Abone.Musteri.OzelkodId1;
            OzelkodId2 = aboneuretici.Abone.Musteri.OzelkodId2;
            OzelkodId3 = aboneuretici.Abone.Musteri.OzelkodId3;
            Abone = AboneBilgiGetir(aboneuretici.Abone.Musteri.Id).Result;
            MusteriIletisim = GetMusteriIletisim(aboneuretici.Abone.Musteri.Id);
            Uretici = GetUretici(aboneuretici);
        }


        public MusteriDTO(IMusteriService musteriService, Humanity.Domain.Entities.Musteri musteri)
        {
            _musteriService = musteriService;
            Id = musteri.Id;
            Adi = musteri.Adi;
            Soyadi = musteri.Soyadi;
            Unvan = musteri.Unvan;
            Tckn = musteri.Tckn;
            CariKartId= musteri.CariKartId; 
            Vkn = musteri.Vkn;
            GercekTuzel = musteri.GercekTuzel;
            OzelkodId1= musteri.OzelkodId1;
            OzelkodId2 = musteri.OzelkodId2;
            OzelkodId3 = musteri.OzelkodId3;
            Abone = AboneBilgiGetir(musteri.Id).Result;
            MusteriIletisim = GetMusteriIletisim(musteri.Id);
          //  Uretici = GetUretici(musteri);
        }
    
    }

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
        public long RefNo{ get; set; }

        public GercekTuzel GercekTuzel { get; set; }
        public MusteriIletisimDTO CariIletisim { get; set; }


        public MusteriIletisimDTO GetIletisim(int cariKartId)
        {
            var musteriIletisim = _cariKartService.GetIletisim(cariKartId);
            return musteriIletisim.Result;
        }

        public CariKartDTO(Humanity.Domain.Entities.CariKart cari, ICariKartService cariKartService)
        {
            _cariKartService = cariKartService;
            Id = cari.Id;
            Adi = cari.Adi;
            Soyadi = cari.Soyadi;
            Unvan = cari.Unvan;
            Durum = cari.Durum.GetHashCode();
            Tckn = cari.Tckn;
            Vkn = cari.Vkn;
            GercekTuzel = cari.GercekTuzel;

            CariIletisim = GetIletisim(cari.Id);
    }
}
}
