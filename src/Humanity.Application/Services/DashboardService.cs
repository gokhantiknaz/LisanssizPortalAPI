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
    public class DashboardService:IDashboardService
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

    }
}
