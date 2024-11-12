using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
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

        private readonly IMapper mapper;

        public DashboardService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logService = loggerService;
            this.mapper = mapper;
        }

        public async Task<List<AboneAylikTuketim>> AboneAylikTuketimGetir()
        {
            string sql = @"
            WITH PreviousMonth AS (
                SELECT 
                    ""AboneId"",
                    ""EndexMonth"",
                    ""EndexYear"",
                    ""T1Endex"" AS PreviousT1Endex,
                    ""T2Endex"" AS PreviousT2Endex,
                    ""T3Endex"" AS PreviousT3Endex
                FROM 
                    ""AboneEndeks""
                WHERE 
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
                    ""T1Endex"",
                    ""T2Endex"",
                    ""T3Endex""
                FROM 
                    ""AboneEndeks""
                WHERE 
                    ""EndexMonth"" = EXTRACT(MONTH FROM CURRENT_DATE)
                    AND ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE)
            )
            SELECT 
                a.""Adi"" as ""Unvan"",
                c.""EndexMonth"",
                c.""EndexYear"",
                a.""SeriNo"",
                COALESCE(c.""T1Endex"" - p.PreviousT1Endex, 0) * a.""Carpan"" AS ""T1Usage"",
                COALESCE(c.""T2Endex"" - p.PreviousT2Endex, 0) * a.""Carpan"" AS ""T2Usage"",
                COALESCE(c.""T3Endex"" - p.PreviousT3Endex, 0) * a.""Carpan"" AS ""T3Usage""
            FROM 
                CurrentMonth c inner join ""Abone"" a on a.""Id""= c.""AboneId""
            LEFT JOIN 
                PreviousMonth p ON c.""AboneId"" = p.""AboneId"";
        ";
            try
            {
                var endeksler = await _unitOfWork.Repository<AboneAylikTuketim>().RawSql(sql);

                if (endeksler == null)
                    throw new Exception("Endeks Bulunamadı");

                return mapper.Map<List<AboneAylikTuketim>>(endeksler.ToList());
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
                //var uretimEndeksSpect = new BaseSpecification<AboneEndeks>(a => a.EndexType == 1);
                //var yearlyUretimData = await _unitOfWork.Repository<AboneEndeks>().ListAsync(uretimEndeksSpect);


                var tuketimEndeksSpect = new BaseSpecification<AboneEndeks>(a => a.EndexYear == 2024);
                tuketimEndeksSpect.AddInclude(a => a.Abone);
                tuketimEndeksSpect.ApplyOrderByDescending(a => a.TSum);
                var yearlyUtuketimData = await _unitOfWork.Repository<AboneEndeks>().ListAsync(tuketimEndeksSpect);

                var all = yearlyUtuketimData.GroupBy(e => new { e.EndexType, e.Abone.Unvan, e.AboneId }).Select(g => new
                {
                    AboneId = g.Key.AboneId,
                    Unvan = g.Key.Unvan,
                    Uretim = g.Where(e => e.EndexType == 1).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    Tuketim = g.Where(e => e.EndexType == 0).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    EndexType = g.Key.EndexType
                }).OrderByDescending(a => a.EndexType);


                var result = all.GroupBy(e => new { e.AboneId, e.Unvan, e.EndexType }).Select(g => new YillikUretimTuketim
                {
                    EndexType = g.Key.EndexType,
                    Unvan = g.Key.Unvan,
                    TotalEndex = g.Sum(e => e.Uretim + e.Tuketim)
                }).OrderByDescending(x => x.EndexType).ToList();

                //ilk 4 tüketici ve 1 üretici
                var topFourTuketici = result.Take(4).ToList();
                var otjhers = result.Skip(4).Aggregate(new YillikUretimTuketim { Unvan = "Diğer", TotalEndex = 0, EndexType = 1 }, (acc, x) =>
                {
                    acc.TotalEndex += x.TotalEndex;
                    return acc;
                });

                topFourTuketici.Add(otjhers);

                return topFourTuketici;
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
                var tuketimEndeksSpect = new BaseSpecification<AboneEndeks>(a => a.EndexYear == 2024);
                tuketimEndeksSpect.AddInclude(a => a.Abone);
                tuketimEndeksSpect.ApplyOrderByDescending(a => a.TSum);
                var yearlyUtuketimData = await _unitOfWork.Repository<AboneEndeks>().ListAsync(tuketimEndeksSpect);

                var all = yearlyUtuketimData.GroupBy(e => new { e.EndexMonth, e.EndexYear }).Select(g => new AylikUretimTuketim
                {
                    Uretim = g.Where(e => e.EndexType == 1).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    Tuketim = g.Where(e => e.EndexType == 0).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    EndexMonth = g.Key.EndexMonth
                });

                //ilk 4 tüketici ve 1 üretici

                return all;
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


            var result = (from e in _unitOfWork.Repository<AboneEndeks>().ListAllAsync().Result
                          join a in _unitOfWork.Repository<Abone>().ListAllAsync().Result on e.AboneId equals a.Id
                          where e.EndexType == 0 && (e.EndexYear== DateTime.Now.Year || (e.EndexYear==DateTime.Now.Year-1 && e.EndexMonth==12))
                          group e by new { a.DagitimFirmaId, a.SeriNo, a.MahsubaDahil,a.Carpan } into g
                          select new AylikBazdaTumAbonelerTuketimSummary
                          {
                              Firma = g.Key.DagitimFirmaId.Value,
                              SeriNo = g.Key.SeriNo.Value,
                              MahsubaDahil = g.Key.MahsubaDahil,
                              Carpan= g.Key.Carpan,
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
                Subat = (x.Subat - x.Ocak)*(double)x.Carpan,         // Şubat = Şubat Endeksi - Ocak Endeksi
                Mart = (x.Mart - x.Subat) * (double)x.Carpan,          // Mart = Mart Endeksi - Şubat Endeksi
                Nisan = (x.Nisan - x.Mart) * (double)x.Carpan,         // Nisan = Nisan Endeksi - Mart Endeksi
                Mayis = (x.Mayis - x.Nisan) * (double)x.Carpan,
                Haziran = (x.Haziran - x.Mayis) * (double)x.Carpan,
                Temmuz = (x.Temmuz - x.Haziran) * (double)x.Carpan,
                Agustos = (x.Agustos - x.Temmuz) * (double)x.Carpan,
                Eylul = (x.Eylul - x.Agustos) * (double)x.Carpan,
                Ekim = (x.Ekim - x.Eylul) * (double)x.Carpan,
                Kasim =     (x.Kasim - x.Ekim) * (double)x.Carpan,
                Aralik = (x.Aralik - x.Kasim) * (double)x.Carpan
            }).ToList();

            return aylikBazdaTuketimSummary;
        }

        public async Task<IEnumerable<AylikBazdaTumAbonelerTuketimSummary>> YillikToplamUretimmGetir()
        {
            var result = (from e in _unitOfWork.Repository<AboneEndeks>().ListAllAsync().Result
                          join a in _unitOfWork.Repository<Abone>().ListAllAsync().Result on e.AboneId equals a.Id
                          where a.SahisTip==Domain.Enums.Enums.SahisTip.Uretici &&  e.EndexType == 1 && (e.EndexYear == DateTime.Now.Year || (e.EndexYear == DateTime.Now.Year - 1 && e.EndexMonth == 12))
                          group e by new { a.DagitimFirmaId, a.SeriNo, a.MahsubaDahil, a.Carpan } into g
                          select new AylikBazdaTumAbonelerTuketimSummary
                          {
                              Firma = g.Key.DagitimFirmaId.Value,
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
