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
                COALESCE(c.""T1Endex"" - p.PreviousT1Endex, 0) AS ""T1Usage"",
                COALESCE(c.""T2Endex"" - p.PreviousT2Endex, 0) AS ""T2Usage"",
                COALESCE(c.""T3Endex"" - p.PreviousT3Endex, 0) AS ""T3Usage""
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
                var otjhers = result.Skip(4).Aggregate(new YillikUretimTuketim { Unvan = "Diğer", TotalEndex = 0,EndexType=1 }, (acc, x) =>
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

                var all = yearlyUtuketimData.GroupBy(e => new  { e.EndexMonth,e.EndexYear }).Select(g => new AylikUretimTuketim
                {
                    Uretim = g.Where(e => e.EndexType == 1).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    Tuketim = g.Where(e => e.EndexType == 0).Sum(e => e.T1Endex + e.T2Endex + e.T3Endex),
                    EndexMonth=g.Key.EndexMonth
                });

                //ilk 4 tüketici ve 1 üretici

                return all;
            }
            catch (Exception er)
            {
                throw er;
            }
        }
    }
}
