using AutoMapper;
using Humanity.Application.Core.Models;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Application.Scripts;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logService;
        private readonly IFirebaseService _firebaseService;


        private readonly IMapper mapper;
        public FaturaService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper, IFirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _logService = loggerService;

            this.mapper = mapper;
            _firebaseService = firebaseService; 
        }
        public async Task<FaturaDTO> AboneAylikFaturaHesapla(int aboneid, string donem)
        {
            // fiili tüketim

            int year = Convert.ToInt32(donem.Split('/')[0]);
            int month = Convert.ToInt32(donem.Split('/')[1]);
            int oncekiAy = month - 1;
            int oncekiYil = year;

            if (month == 1)
            {
                oncekiYil -= -1;
                oncekiAy = 12;
            }

            string oncekiAyWhereClause = @$" AND ""EndexYear"" = {oncekiYil} AND ""EndexMonth"" = {oncekiAy} AND ""AboneId""={aboneid}";
            string donemWhereClause = @$" AND ""EndexYear"" = {year} AND ""EndexMonth"" = {month} AND ""AboneId""={aboneid}";

            var queryStr = String.Format(SqlQueryProvider.GetQuery(Domain.Enums.Enums.SqlQuery.AboneAktifAylikTuketimUretim), oncekiAyWhereClause, donemWhereClause);
            var aboneDonemEndeks = await _unitOfWork.Repository<AboneAylikTuketim>().RawSql(queryStr);

            // abone tarifeyi bulalım.
            Abone abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneid);
            var tarifeFiyatlar =await _firebaseService.GetTarifeFiyat();
            var aboneTarifeler = tarifeFiyatlar.Where(a => a.AgOg.GetHashCode() == abone.Agog.GetHashCode() && a.Terim.GetHashCode() == abone.Terim);
            var gunduzTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Gunduz);
            var puantTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Puant);
            var geceTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Gece);

            return new FaturaDTO() { AboneId = aboneid, Donem = donem };
        }
    }
}
