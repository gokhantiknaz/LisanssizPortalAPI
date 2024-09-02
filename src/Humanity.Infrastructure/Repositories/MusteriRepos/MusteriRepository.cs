using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Repositories;
using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Repositories;

using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Humanity.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Infrastructure.Repositories.MusteriRepos
{
    public  class MusteriRepository: BaseRepositoryAsync<Musteri>, IMusteriRepository
    {
        protected readonly LisanssizContext _dbContext;

        public MusteriRepository(LisanssizContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public MusteriDTO get(int id)   
        {
            var result = _dbContext.Musteri
    .Where(m => m.Id == 4) // Musteri tablosunda filtreleme
    .Join(_dbContext.MusteriIletisim, // INNER JOIN MusteriIletisim
          musteri => musteri.Id,
          musteriIletisim => musteriIletisim.MusteriId,
          (musteri, musteriIletisim) => new { musteri, musteriIletisim })
    .Join(_dbContext.Iletisim, // INNER JOIN Iletisim
          temp => temp.musteriIletisim.IletisimId,
          iletisim => iletisim.Id,
          (temp, iletisim) => new { temp.musteri, temp.musteriIletisim, iletisim })
    .Join(_dbContext.Abone, // INNER JOIN Abone
          temp => temp.musteri.Id,
          abone => abone.MusteriId,
          (temp, abone) => new { temp.musteri, temp.musteriIletisim, temp.iletisim, abone })
    .GroupJoin(_dbContext.AboneUretici, // LEFT JOIN AboneUretici
               temp => temp.abone.Id,
               aboneUretici => aboneUretici.AboneId,
               (temp, aboneUreticiler) => new { temp.musteri, temp.musteriIletisim, temp.iletisim, temp.abone, aboneUreticiler })
    .SelectMany(temp => temp.aboneUreticiler.DefaultIfEmpty(), // LEFT JOIN için DefaultIfEmpty kullanımı
                (temp, aboneUretici) => new MusteriDTO(temp.musteri)
                {
                    Id = temp.musteri.Id,
                    Adi = temp.musteri.Adi,
                    Soyadi = temp.musteri.Soyadi,
                    Unvan = temp.musteri.Unvan,
                    CariKartId = temp.musteri.CariKartId,
                    Tckn = temp.musteri.Tckn,
                    Vkn = temp.musteri.Vkn,
                    Durum = temp.musteri.Durum,
                    GercekTuzel = temp.musteri.GercekTuzel,
                    OzelkodId1 = temp.musteri.OzelkodId1,
                    OzelkodId2 = temp.musteri.OzelkodId2,
                    OzelkodId3 = temp.musteri.OzelkodId3,
                    Abone = new AboneDTO(temp.abone),
                    //{
                    //    Tarife = temp.abone.Tarife,
                    //    EtsoKodu = temp.abone.EtsoKodu,
                    //    DagitimFirmaId = temp.abone.DagitimFirmaId,
                    //    SeriNo = temp.abone.SeriNo,
                    //    SozlesmeGucu = temp.abone.SozlesmeGucu,
                    //    BaglantiGucu = temp.abone.BaglantiGucu,
                    //    KuruluGuc = temp.abone.KuruluGuc,
                    //    SahisTip = temp.abone.SahisTip,
                    //    Terim = temp.abone.Terim,
                    //    Agog = temp.abone.Agog,
                    
                    //},
                    Uretici = aboneUretici != null ? new UreticiDTO(aboneUretici) : null,
                    MusteriIletisim = new MusteriIletisimDTO(temp.musteriIletisim)
                  
                })
    .FirstOrDefault();

            return result;

        }

        public List<MusteriDTO> GetBagimsizTuketiciler(int cariId)
        {
            var secilebilecekMusteriler = _dbContext.Abone.Where(a => a.Musteri.CariKartId==cariId && a.SahisTip != Domain.Enums.Enums.SahisTip.Uretici
            && _dbContext.AboneTuketici.Any(y => y.Id != a.Id)).Select(x => new MusteriDTO(x.Musteri)).ToList();

            return secilebilecekMusteriler;
        }
    }
}
