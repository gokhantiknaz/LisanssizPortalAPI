using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Humanity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Humanity.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logService;
        private readonly IFirebaseService _fireService;
        private readonly IMapper mapper;


        const string sqlGunlukUretimTuketimMiktar = @"select ""Donem"",
       EXTRACT(day FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) as Gun,
       sum(""Uretim"")                                                              as ToplamUretim,
       sum(""CekisTuketim"")                                                        as ToplamTuketim
from ""AboneSaatlikEndeks""
where EXTRACT(YEAR FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(YEAR FROM CURRENT_DATE)
  and EXTRACT(MONTH FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(MONTH FROM CURRENT_DATE)
group by ""Donem"", EXTRACT(day FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD'));";

        const string sqlAyliEnDusukEnYUksekKullanimMiktarveGunleri = @"
WITH DailyConsumption AS (
    SELECT
        TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD') AS ""Day"",
        ""Donem"",
        SUM(""CekisTuketim"") AS ""TotalDailyConsumption""
    FROM
        ""AboneSaatlikEndeks""
     where    EXTRACT(YEAR FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(YEAR FROM CURRENT_DATE)
    GROUP BY
        TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD'), ""Donem""
),
MonthlyHighLow AS (
    SELECT
        ""Donem"",
        MAX(""TotalDailyConsumption"") AS ""HighConsumption"",
        MIN(""TotalDailyConsumption"") AS ""LowConsumption""
    FROM
        DailyConsumption
    GROUP BY
        ""Donem""
)
SELECT
    mh.""Donem"",
    mh.""HighConsumption"",
    (SELECT ""Day""
     FROM DailyConsumption
     WHERE ""Donem"" = mh.""Donem"" AND ""TotalDailyConsumption"" = mh.""HighConsumption""
     LIMIT 1) AS ""HighDay"",
    mh.""LowConsumption"",
    (SELECT ""Day""
     FROM DailyConsumption
     WHERE ""Donem"" = mh.""Donem"" AND ""TotalDailyConsumption"" = mh.""LowConsumption""
     LIMIT 1) AS ""LowDay""
FROM
    MonthlyHighLow mh;
";


        const string sqlAboneAktifAylikTuketimUretim = @"
            WITH PreviousMonth AS (
                SELECT 
                    ""AboneId"",
                    ""EndexMonth"",
                    ""EndexYear"",
                    ""EndexType"",
                    ""T1Endex"" AS PreviousT1Endex,
                    ""T2Endex"" AS PreviousT2Endex,
                    ""T3Endex"" AS PreviousT3Endex
                FROM 
                    ""AboneEndeks""
                WHERE
                  ""EndexType""=0 and 
                     (""EndexMonth"" = EXTRACT(MONTH FROM CURRENT_DATE) - 1 
                     AND ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE))
                    OR
                    (""EndexMonth"" = 12 AND EXTRACT(MONTH FROM CURRENT_DATE) = 1 AND ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE) - 1)
            ),
            CurrentMonth AS (
                SELECT 
                    ""AboneId"",
                    ""EndexMonth"",
                    ""EndexYear"",
                    ""EndexType"",
                    ""T1Endex"",
                    ""T2Endex"",
                    ""T3Endex""
                FROM 
                    ""AboneEndeks""
                WHERE 
                  ""EndexType""=0 and 
                    ""EndexMonth"" = EXTRACT(MONTH FROM CURRENT_DATE)
                    AND ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE)
            )
            SELECT 
                a.""Adi"" as ""Unvan"",
                c.""EndexMonth"",
                c.""EndexYear"",
                a.""SeriNo"",
                c.""EndexType"",
                COALESCE(c.""T1Endex"" - p.PreviousT1Endex, 0) * a.""Carpan"" AS ""T1Usage"",
                COALESCE(c.""T2Endex"" - p.PreviousT2Endex, 0) * a.""Carpan"" AS ""T2Usage"",
                COALESCE(c.""T3Endex"" - p.PreviousT3Endex, 0) * a.""Carpan"" AS ""T3Usage""
            FROM 
                CurrentMonth c inner join ""Abone"" a on a.""Id""= c.""AboneId""
            LEFT JOIN 
                PreviousMonth p ON c.""AboneId"" = p.""AboneId"";
        ";

        const string sqlYillikTuketimUretim = @"
         WITH EndeksFark AS (
    SELECT
        ""AboneId"",
        ""EndexMonth"",
        ""EndexYear"",
        ""EndexType"",
        LAG(""T1Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT1,
        LAG(""T2Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT2,
        LAG(""T3Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT3,
        ""T1Endex"",
        ""T2Endex"",
        ""T3Endex""
    FROM ""AboneEndeks""
    WHERE  ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE) OR (""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE) - 1 AND ""EndexMonth"" = 12)
)
SELECT
    ""EndexMonth"",
    ""EndexYear"",
    SUM(CASE
            WHEN ""EndexType"" = 1 THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS Uretim,
    SUM(CASE
            WHEN ""EndexType"" = 0 AND ""Abone"".""MahsubaDahil"" = TRUE THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS Tuketim,
    SUM(CASE
            WHEN ""EndexType"" = 0 AND ""Abone"".""MahsubaDahil"" = FALSE THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS TuketimMahsubaDahilDegil,
       SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T1Tuketim,
         SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T2Endex"" - COALESCE(PrevT2, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T2Tuketim,
         SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T3Tuketim

FROM EndeksFark
JOIN ""Abone"" ON EndeksFark.""AboneId"" = ""Abone"".""Id""
WHERE ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE)
GROUP BY ""EndexMonth"",""EndexYear""
ORDER BY ""EndexMonth"";

        ";

        const string sqlYillikUretimTuketimToplam = @"
WITH RankedData AS (
    SELECT 
        ""AboneEndeks"".""EndexType"",  
        (MAX(""TSum"") - MIN(""TSum"")) * ""Carpan"" as Tuketim , 
        A.""Unvan"",
        ROW_NUMBER() OVER (PARTITION BY ""EndexType"" ORDER BY (MAX(""TSum"") - MIN(""TSum"")) * ""Carpan"" DESC) AS rn
    FROM ""AboneEndeks""
    INNER JOIN public.""Abone"" A ON ""AboneEndeks"".""AboneId"" = A.""Id""
    GROUP BY ""EndexType"", A.""Id"", A.""Unvan""
)
SELECT 
    ""EndexType"",
    CASE 
        WHEN rn <= 4 THEN ""Unvan"" 
        ELSE 'Diğer' 
    END AS ""Unvan"",
    SUM(Tuketim) AS TotalEndex
FROM RankedData
GROUP BY ""EndexType"", 
         CASE WHEN rn <= 4 THEN ""Unvan"" ELSE 'Diğer' END
ORDER BY ""EndexType"" DESC, ""Unvan"";";

        public DashboardService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper, IFirebaseService fireService)
        {
            _unitOfWork = unitOfWork;
            _logService = loggerService;
            _fireService = fireService;
            this.mapper = mapper;
        }

        public async Task<List<AboneAylikTuketim>> AboneAktifAylikTuketimGetir()
        {
            try
            {
                var endeksler = await _unitOfWork.Repository<AboneAylikTuketim>().RawSql(sqlAboneAktifAylikTuketimUretim);
                if (endeksler == null)
                    throw new Exception("Endeks Bulunamadı");

                return mapper.Map<List<AboneAylikTuketim>>(endeksler.Where(a => a.EndexType == Enums.EnumEndeksDirection.Tuketim.GetHashCode()).ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<DailyProductionConsumption>> GunlukUretimTuketimGetir()
        {
            try
            {
                var endeksler = await _unitOfWork.Repository<DailyProductionConsumption>().RawSql(sqlGunlukUretimTuketimMiktar);
                if (endeksler == null)
                    throw new Exception("Endeks Bulunamadı");

                return endeksler.OrderBy(a => a.Gun).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<AylikEnYuksekEnDusukTuketimGunveMiktar>> AylikEnYuksekEnDusukTuketimGunveMiktar()
        {
            try
            {
                var endeksler = await _unitOfWork.Repository<AylikEnYuksekEnDusukTuketimGunveMiktar>().RawSql(sqlAyliEnDusukEnYUksekKullanimMiktarveGunleri);
                if (endeksler == null)
                    throw new Exception("Endeks Bulunamadı");

                return endeksler.OrderByDescending(a => a.Donem).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<YillikUretimTuketim>> AboneUretimTuketimKarsilastirma()
        {
            try
            {



                //            var endeksSpect = new BaseSpecification<AboneEndeks>(a => a.EndexYear == DateTime.Now.Year);
                //            endeksSpect.AddInclude(a => a.Abone);
                //            var yearlyUretimData = await _unitOfWork.Repository<AboneEndeks>().ListAsync(endeksSpect);

                //            var endeksresult = yearlyUretimData.GroupBy(a => new { a.Abone.Unvan, a.AboneId, a.EndexType, a.Abone.Carpan })
                //.Select(g => new
                //{
                //    AboneId = g.Key,
                //    Carpan = g.Key.Carpan,
                //    Unvan = g.Key.Unvan,
                //    EndexType = g.Key.EndexType,
                //    // En düşük yıl ve aya ait T1, T2, T3 endeks değerleri
                //    MinEndeks = g
                //        .OrderBy(a => a.EndexYear)
                //        .ThenBy(a => a.EndexMonth)
                //        .Select(a => new
                //        {
                //            a.EndexYear,
                //            a.EndexMonth,
                //            a.T1Endex,
                //            a.T2Endex,
                //            a.T3Endex,
                //            a.TSum
                //        })
                //        .FirstOrDefault(),

                //    // En yüksek yıl ve aya ait T1, T2, T3 endeks değerleri
                //    MaxEndeks = g
                //        .OrderByDescending(a => a.EndexYear)
                //        .ThenByDescending(a => a.EndexMonth)
                //        .Select(a => new
                //        {
                //            a.EndexYear,
                //            a.EndexMonth,
                //            a.T1Endex,
                //            a.T2Endex,
                //            a.T3Endex,
                //            a.TSum
                //        })
                //        .FirstOrDefault()
                //}).ToList();
                //son endeks - ilk endeks tüketimi verecek


                //var all = endeksresult.GroupBy(e => new { e.Unvan, e.EndexType, e.AboneId, e.Carpan }).Select(g => new
                //{
                //    AboneId = g.Key.AboneId,
                //    Unvan = g.Key.Unvan,
                //    Uretim = g.Where(e => e.EndexType == 1).Sum(e => e.MaxEndeks.TSum - e.MinEndeks.TSum) * (double)g.Key.Carpan,
                //    Tuketim = g.Where(e => e.EndexType == 0).Sum(e => e.MaxEndeks.TSum - e.MinEndeks.TSum) * (double)g.Key.Carpan,
                //    EndexType = g.Key.EndexType
                //}).OrderByDescending(a => a.EndexType);

                //var result = all.GroupBy(e => new { e.AboneId, e.Unvan, e.EndexType }).Select(g => new YillikUretimTuketim
                //{
                //    EndexType = g.Key.EndexType,
                //    Unvan = g.Key.Unvan,
                //    TotalEndex = g.Key.EndexType == 0 ? g.Sum(e => e.Tuketim) : g.Sum(e => e.Uretim)
                //}).OrderByDescending(x => x.EndexType).ToList();

                ////ilk 4 tüketici ve 1 üretici

                //var topFourTuketici = result.Take(4).ToList();
                //var otjhers = result.Skip(4).Aggregate(new YillikUretimTuketim { Unvan = "Diğer", TotalEndex = 0, EndexType = 0 }, (acc, x) =>
                //{
                //    acc.TotalEndex += x.TotalEndex;
                //    return acc;
                //});

                //topFourTuketici.Add(otjhers);

                var endeksler = await _unitOfWork.Repository<YillikUretimTuketim>().RawSql(sqlYillikUretimTuketimToplam);

                return endeksler.ToList();

            }
            catch (Exception er)
            {
                throw er;
            }
        }

        public async Task<IEnumerable<AylikUretimTuketim>> AylikUretimTuketimKarsilastirma()
        {
            try
            {
                var endeksler = await _unitOfWork.Repository<AylikUretimTuketim>().RawSql(sqlYillikTuketimUretim);

                return endeksler.ToList();
            }
            catch (Exception er)
            {
                throw er;
            }
        }

        public async Task<IEnumerable<AylikBazdaTumAbonelerTuketimSummary>> YillikToplamTuketimGetir()
        {


            //        string rawSql= @"SELECT
            //    ""DagitimFirmaId"" as Firma,
            //    a.""SeriNo""  ,
            //    a.""MahsubaDahil"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 1 THEN ""TSum"" ELSE 0 END) AS ""Ocak"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 2 THEN ""TSum"" ELSE 0 END) AS ""Subat"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 3 THEN ""TSum"" ELSE 0 END) AS ""Mart"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 4 THEN ""TSum"" ELSE 0 END) AS ""Nisan"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 5 THEN ""TSum"" ELSE 0 END) AS ""Mayis"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 6 THEN ""TSum"" ELSE 0 END) AS ""Haziran"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 7 THEN ""TSum"" ELSE 0 END) AS ""Temmuz"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 8 THEN ""TSum"" ELSE 0 END) AS ""Agustos"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 9 THEN ""TSum"" ELSE 0 END) AS ""Eylul"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 10 THEN ""TSum"" ELSE 0 END) AS ""Ekim"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 11 THEN ""TSum"" ELSE 0 END) AS ""Kasim"",
            //    SUM(CASE WHEN e.""EndexMonth"" = 12 THEN ""TSum"" ELSE 0 END) AS ""Aralik""
            //FROM ""AboneEndeks"" e
            //INNER JOIN ""Abone"" a ON e.""AboneId"" = a.""Id""
            //WHERE e.""EndexType"" = 0 and ""EndexYear"" = 2024
            //GROUP BY ""DagitimFirmaId"", a.""SeriNo"", a.""MahsubaDahil"" ";

            //            var endeksler = await _unitOfWork.Repository<AylikUTuketimSummaryretimTuketim>().RawSql(rawSql);

            var firmalar = await _fireService.GetDagitimFirmalar();

            var result = (from e in _unitOfWork.Repository<AboneEndeks>().ListAllAsync().Result
                          join a in _unitOfWork.Repository<Abone>().ListAllAsync().Result on e.AboneId equals a.Id
                          where e.EndexType == 0 && (e.EndexYear == DateTime.Now.Year || (e.EndexYear == DateTime.Now.Year - 1 && e.EndexMonth == 12))
                          group e by new { a.DagitimFirmaId, a.SeriNo, a.MahsubaDahil, a.Carpan } into g
                          select new AylikBazdaTumAbonelerTuketimSummary
                          {
                              Firma = firmalar.FirstOrDefault(a => a.Id == g.Key.DagitimFirmaId.Value).Adi,
                              SeriNo = g.Key.SeriNo.Value,
                              MahsubaDahil = g.Key.MahsubaDahil,
                              Carpan = g.Key.Carpan,
                              AralikOncekiYil = g.Where(x => x.EndexMonth == 12 && x.EndexYear == DateTime.Now.Year - 1).Sum(x => x.TSum),
                              Ocak = g.Where(x => x.EndexMonth == 1).Sum(x => x.TSum),
                              Subat = g.Where(x => x.EndexMonth == 2).Sum(x => x.TSum),
                              Mart = g.Where(x => x.EndexMonth == 3).Sum(x => x.TSum),
                              Nisan = g.Where(x => x.EndexMonth == 4).Sum(x => x.TSum),
                              Mayis = g.Where(x => x.EndexMonth == 5).Sum(x => x.TSum),
                              Haziran = g.Where(x => x.EndexMonth == 6).Sum(x => x.TSum),
                              Temmuz = g.Where(x => x.EndexMonth == 7).Sum(x => x.TSum),
                              Agustos = g.Where(x => x.EndexMonth == 8).Sum(x => x.TSum),
                              Eylul = g.Where(x => x.EndexMonth == 9).Sum(x => x.TSum),
                              Ekim = g.Where(x => x.EndexMonth == 10).Sum(x => x.TSum),
                              Kasim = g.Where(x => x.EndexMonth == 11).Sum(x => x.TSum),
                              Aralik = g.Where(x => x.EndexMonth == 12).Sum(x => x.TSum)
                          }).ToList();


            var aylikBazdaTuketimSummary = result.Select(x => new AylikBazdaTumAbonelerTuketimSummary
            {
                Firma = x.Firma,
                SeriNo = x.SeriNo,
                MahsubaDahil = x.MahsubaDahil,
                Ocak = (x.Ocak - x.AralikOncekiYil) * (double)x.Carpan,  // Ocak tüketimi = Ocak endeksi - Bir önceki yıl Aralık endeksi
                Subat = (x.Subat - x.Ocak) * (double)x.Carpan,         // Şubat = Şubat Endeksi - Ocak Endeksi
                Mart = (x.Mart - x.Subat) * (double)x.Carpan,          // Mart = Mart Endeksi - Şubat Endeksi
                Nisan = (x.Nisan - x.Mart) * (double)x.Carpan,         // Nisan = Nisan Endeksi - Mart Endeksi
                Mayis = (x.Mayis - x.Nisan) * (double)x.Carpan,
                Haziran = (x.Haziran - x.Mayis) * (double)x.Carpan,
                Temmuz = (x.Temmuz - x.Haziran) * (double)x.Carpan,
                Agustos = (x.Agustos - x.Temmuz) * (double)x.Carpan,
                Eylul = (x.Eylul - x.Agustos) * (double)x.Carpan,
                Ekim = (x.Ekim - x.Eylul) * (double)x.Carpan,
                Kasim = (x.Kasim - x.Ekim) * (double)x.Carpan,
                Aralik = (x.Aralik - x.Kasim) * (double)x.Carpan
            }).ToList();

            return aylikBazdaTuketimSummary;
        }

        public async Task<IEnumerable<AylikBazdaTumAbonelerTuketimSummary>> YillikToplamUretimmGetir()
        {
            var firmalar = await _fireService.GetDagitimFirmalar();

            var result = (from e in _unitOfWork.Repository<AboneEndeks>().ListAllAsync().Result
                          join a in _unitOfWork.Repository<Abone>().ListAllAsync().Result on e.AboneId equals a.Id
                          where a.SahisTip == Domain.Enums.Enums.SahisTip.Uretici && e.EndexType == 1 && (e.EndexYear == DateTime.Now.Year || (e.EndexYear == DateTime.Now.Year - 1 && e.EndexMonth == 12))
                          group e by new { a.DagitimFirmaId, a.SeriNo, a.MahsubaDahil, a.Carpan } into g
                          select new AylikBazdaTumAbonelerTuketimSummary
                          {
                              Firma = firmalar.FirstOrDefault(a => a.Id == g.Key.DagitimFirmaId.Value).Adi,
                              SeriNo = g.Key.SeriNo.Value,
                              MahsubaDahil = g.Key.MahsubaDahil,
                              Carpan = g.Key.Carpan,
                              AralikOncekiYil = g.Where(x => x.EndexMonth == 12 && x.EndexYear == DateTime.Now.Year - 1).Sum(x => x.TSum),
                              Ocak = g.Where(x => x.EndexMonth == 1).Sum(x => x.TSum),
                              Subat = g.Where(x => x.EndexMonth == 2).Sum(x => x.TSum),
                              Mart = g.Where(x => x.EndexMonth == 3).Sum(x => x.TSum),
                              Nisan = g.Where(x => x.EndexMonth == 4).Sum(x => x.TSum),
                              Mayis = g.Where(x => x.EndexMonth == 5).Sum(x => x.TSum),
                              Haziran = g.Where(x => x.EndexMonth == 6).Sum(x => x.TSum),
                              Temmuz = g.Where(x => x.EndexMonth == 7).Sum(x => x.TSum),
                              Agustos = g.Where(x => x.EndexMonth == 8).Sum(x => x.TSum),
                              Eylul = g.Where(x => x.EndexMonth == 9).Sum(x => x.TSum),
                              Ekim = g.Where(x => x.EndexMonth == 10).Sum(x => x.TSum),
                              Kasim = g.Where(x => x.EndexMonth == 11).Sum(x => x.TSum),
                              Aralik = g.Where(x => x.EndexMonth == 12).Sum(x => x.TSum)
                          }).ToList();


            var aylikBazdaTuketimSummary = result.Select(x => new AylikBazdaTumAbonelerTuketimSummary
            {
                Firma = x.Firma,
                SeriNo = x.SeriNo,
                MahsubaDahil = x.MahsubaDahil,
                Ocak = (x.Ocak - x.AralikOncekiYil) * (double)x.Carpan,  // Ocak tüketimi = Ocak endeksi - Bir önceki yıl Aralık endeksi
                Subat = (x.Subat - x.Ocak) * (double)x.Carpan,         // Şubat = Şubat Endeksi - Ocak Endeksi
                Mart = (x.Mart - x.Subat) * (double)x.Carpan,          // Mart = Mart Endeksi - Şubat Endeksi
                Nisan = (x.Nisan - x.Mart) * (double)x.Carpan,         // Nisan = Nisan Endeksi - Mart Endeksi
                Mayis = (x.Mayis - x.Nisan) * (double)x.Carpan,
                Haziran = (x.Haziran - x.Mayis) * (double)x.Carpan,
                Temmuz = (x.Temmuz - x.Haziran) * (double)x.Carpan,
                Agustos = (x.Agustos - x.Temmuz) * (double)x.Carpan,
                Eylul = (x.Eylul - x.Agustos) * (double)x.Carpan,
                Ekim = (x.Ekim - x.Eylul) * (double)x.Carpan,
                Kasim = (x.Kasim - x.Ekim) * (double)x.Carpan,
                Aralik = (x.Aralik - x.Kasim) * (double)x.Carpan
            }).ToList();

            return aylikBazdaTuketimSummary;
        }
    }
}
