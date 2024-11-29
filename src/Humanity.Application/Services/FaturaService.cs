using AutoMapper;
using Humanity.Application.Core.Models;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Application.Scripts;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Humanity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

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
        public async Task<List<FaturaDTO>> AboneAylikFaturaHesapla(int aboneid, string donem)
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
            var retListDto = new List<FaturaDTO>();
            string oncekiAyWhereClause = @$" AND ""EndexYear"" = {oncekiYil} AND ""EndexMonth"" = {oncekiAy} ";
            string donemWhereClause = @$" AND ""EndexYear"" = {year} AND ""EndexMonth"" = {month} ";

            var vergiler = await _firebaseService.GetVergiler();
            if (aboneid > 0)
            {
                oncekiAyWhereClause += $@" AND ""AboneId""={aboneid}";
                donemWhereClause += $@" AND ""AboneId""={aboneid}";
            }

            var queryStr = String.Format(SqlQueryProvider.GetQuery(Domain.Enums.Enums.SqlQuery.AboneAktifAylikTuketimUretim), oncekiAyWhereClause, donemWhereClause);
            var aboneDonemEndeks = await _unitOfWork.Repository<AboneAylikTuketim>().RawSql(queryStr);

            // abone tarifeyi bulalım.

            var tarifeFiyatlar = await _firebaseService.GetTarifeFiyat();

            foreach (var aboneendex in aboneDonemEndeks)
            {
                Abone abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneendex.AboneId);
                AboneEndeks endex = await _unitOfWork.Repository<AboneEndeks>().GetBy(new BaseSpecification<AboneEndeks>(x => x.AboneId == abone.Id && x.EndexYear == year && x.EndexMonth == month));
                List<FaturaDetayDTO> detaySatirlar = new List<FaturaDetayDTO>();
                var aboneTarifeler = tarifeFiyatlar.Where(a => a.AgOg.GetHashCode() == abone.Agog.GetHashCode() && a.Terim.GetHashCode() == abone.Terim && a.TanimId == abone.Tarife);
                var dagBedBrFiyat = aboneTarifeler.FirstOrDefault().DagitimBedeli;
                decimal gunduzBrFiyat = 0, puantBrFiyat = 0, geceBrFiyat = 0, reaktifBrFiyat;

                if (abone.SayacZaman == Enums.EmumSayacZaman.CiftZaman)
                {
                    var gunduzTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Gunduz);
                    var puantTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Puant);
                    var geceTarife = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == Domain.Enums.Enums.EnumEnerjiCinsi.Gece);

                    gunduzBrFiyat = gunduzTarife.NormalFiyat;
                    puantBrFiyat = puantTarife.NormalFiyat;
                    geceBrFiyat = geceTarife.NormalFiyat;
                    reaktifBrFiyat = gunduzTarife.ReaktifBedel;
                }
                else
                {
                    gunduzBrFiyat = puantBrFiyat = geceBrFiyat = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == EnumEnerjiCinsi.Tum).NormalFiyat;
                    reaktifBrFiyat = aboneTarifeler.FirstOrDefault(a => a.EnerjiCinsi == EnumEnerjiCinsi.Tum).ReaktifBedel;
                }


                #region Gunduz
                var t1Tl = (decimal)aboneendex.T1Usage * gunduzBrFiyat;
                var faturaDetayGunduzDto = new FaturaDetayDTO()
                {
                    KalemKod = EnumThkKod.Gunduz,
                    KalemAdi = EnumThkKod.Gunduz.ToString(),
                    Kwh = (decimal)aboneendex.T1Usage,
                    Tutar = t1Tl,
                    KdvOran = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger,
                    KdvTuar = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger * t1Tl,
                    BirimFiyat = gunduzBrFiyat
                };
                detaySatirlar.Add(faturaDetayGunduzDto);
                #endregion

                #region Puant
                var t2Tl = (decimal)aboneendex.T2Usage * puantBrFiyat;

                var faturaDetayPuantDto = new FaturaDetayDTO()
                {
                    KalemKod = EnumThkKod.Puant,
                    Kwh = (decimal)aboneendex.T2Usage,
                    KalemAdi = EnumThkKod.Puant.ToString(),
                    Tutar = t2Tl,
                    KdvOran = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger,
                    KdvTuar = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger * t2Tl,
                    BirimFiyat = puantBrFiyat
                };
                detaySatirlar.Add(faturaDetayPuantDto);
                #endregion

                #region Gece
                var t3Tl = (decimal)aboneendex.T3Usage * geceBrFiyat;
                var faturaDetayGeceDto = new FaturaDetayDTO()
                {
                    KalemKod = EnumThkKod.Gece,
                    KalemAdi = EnumThkKod.Gece.ToString(),
                    Kwh = (decimal)aboneendex.T3Usage,
                    Tutar = t3Tl,
                    KdvOran = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger,
                    KdvTuar = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger * t3Tl,
                    BirimFiyat = geceBrFiyat
                };
                detaySatirlar.Add(faturaDetayGeceDto);
                #endregion

                #region Dagitim
                var dagitimBedeli = (decimal)(aboneendex.T1Usage + aboneendex.T2Usage + aboneendex.T3Usage)
                     * dagBedBrFiyat;

                var faturaDetayDagiimDto = new FaturaDetayDTO()
                {
                    KalemKod = EnumThkKod.DagitimBaglantiBedeli,
                    KalemAdi = EnumThkKod.DagitimBaglantiBedeli.ToString(),
                    Kwh = (decimal)aboneendex.T1Usage + (decimal)aboneendex.T2Usage + (decimal)aboneendex.T3Usage,
                    Tutar = dagitimBedeli,
                    KdvOran = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger,
                    KdvTuar = vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger * dagitimBedeli,
                    BirimFiyat = dagBedBrFiyat
                };

                detaySatirlar.Add(faturaDetayDagiimDto);

                #endregion

                #region GucBedeli
                // güç bedel hesabı
                decimal gucBedeliTutar = 0;

                decimal gucAsimBedeliTutar = 0;
                if (abone.KuruluGuc >= 15.01) // reaktif hesap
                {
                    FaturaDetayDTO reaktifDetay = ReaktifCezaHesapla(abone, aboneendex, reaktifBrFiyat, vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger);
                    if (reaktifDetay.Tutar > 0)
                        detaySatirlar.Add(reaktifDetay);

                }
                if (abone.Agog == Enums.EnumAgOg.Og.GetHashCode())// güç bedeli
                {

                    // k.G = 400
                    // max demand 0.072
                    // carpan 1750

                    if (abone.Terim == EnumTerim.CiftTerim.GetHashCode() || abone.Terim == EnumTerim.CiftTerimPuant.GetHashCode())
                    {
                        var gucbedeli = vergiler.FirstOrDefault(a => a.Adi == "GUC").Deger;
                        var gucAsimbedeli = vergiler.FirstOrDefault(a => a.Adi == "GUCASIM").Deger;

                        var dAlinacakGucAsim = 0.0;
                        var dAlinacakGuc = 0.0;
                        var dDemandGucKwh = endex.MaxDemand * (double)abone.Carpan;

                        if (dDemandGucKwh > abone.SozlesmeGucu)
                            dAlinacakGucAsim = dDemandGucKwh - abone.SozlesmeGucu;
                        dAlinacakGuc = abone.SozlesmeGucu;
                        gucBedeliTutar = Convert.ToDecimal(dAlinacakGuc * (double)gucbedeli);
                        var faturaDetayDto = new FaturaDetayDTO() { KalemAdi = EnumThkKod.GucBedeli.ToString(), KalemKod = EnumThkKod.GucBedeli, Kwh = (decimal)dAlinacakGuc, Tutar = gucBedeliTutar, KdvOran = 1, KdvTuar = 1 };
                        detaySatirlar.Add(faturaDetayDto);
                        gucAsimBedeliTutar = Convert.ToDecimal(dAlinacakGucAsim * (double)gucAsimbedeli);
                        if (gucAsimBedeliTutar > 0)
                        {
                            faturaDetayDto = new FaturaDetayDTO() { KalemAdi = EnumThkKod.GucAsimBedeli.ToString(), KalemKod = EnumThkKod.GucAsimBedeli, Kwh = (decimal)dAlinacakGucAsim, Tutar = gucAsimBedeliTutar, KdvOran = 1, KdvTuar = 1 };
                            detaySatirlar.Add(faturaDetayDto);
                        }
                    }
                }

                #endregion

                var retDto = new FaturaDTO() { AboneId = abone.Id, Donem = donem };

                retDto.SeriNo = abone.SeriNo.GetValueOrDefault(0);
                retDto.Fonlar = (t1Tl + t2Tl + t3Tl) * vergiler.FirstOrDefault(a => a.Adi == "BTV").Deger;
                retDto.KDV = (t1Tl + t2Tl + t3Tl) * vergiler.FirstOrDefault(a => a.Adi == "KDV").Deger;
                retDto.FaturaDetaylar = detaySatirlar;

                retListDto.Add(retDto);
            }

            return retListDto;
        }

        private FaturaDetayDTO ReaktifCezaHesapla(Abone abone, AboneAylikTuketim endex, decimal reaktifBrFiyat,decimal kdvOran)
        {
            var enduktifOran = endex.InduktifUsage / endex.TSum;
            var fdInd = new FaturaDetayDTO() { Tutar = 0, KdvTuar = 0 };
            var fdKap = new FaturaDetayDTO() { Tutar = 0, KdvTuar = 0 };

            var cezaOranlari = CezaOranlari(abone.KuruluGuc.Value);

            //enduktif tüketim aktif tüketimin limit oranından büyükse, doğrudan kendisi alınacak
            if ((abone.KuruluGuc > 15.01 && abone.KuruluGuc < 50 && enduktifOran > 0.33) || (abone.KuruluGuc > 50.01 && enduktifOran > 0.2))
            {
                var enduktifTutar = 0.0;
                if (endex.InduktifUsage > (endex.TSum * cezaOranlari.dEnduktifCezaOrani))
                {
                    enduktifTutar = (endex.InduktifUsage) * (double)reaktifBrFiyat;
                }

                decimal kdv = (decimal)enduktifTutar * kdvOran;

                fdInd = new FaturaDetayDTO()
                {
                    KalemAdi = EnumThkKod.ReaktifIndudkif.ToString(),
                    BirimFiyat = reaktifBrFiyat,
                    KalemKod = EnumThkKod.ReaktifIndudkif,
                    KdvOran = kdvOran,
                    KdvTuar = kdv,
                    Kwh = (decimal)endex.InduktifUsage,
                    Tutar = (decimal)enduktifTutar
                };
            }

            var kapasiifOran = endex.KapasitifUsage / endex.TSum;
            if ((abone.KuruluGuc > 15.01 && abone.KuruluGuc < 50 && kapasiifOran > 0.33) || (abone.KuruluGuc > 50.01 && kapasiifOran > 0.2))
            {
                var kapasitifTutar = 0.0;
                if (endex.KapasitifUsage > (endex.TSum * cezaOranlari.dKapasitifCezaOrani))
                {
                    kapasitifTutar = (endex.KapasitifUsage) * (double)reaktifBrFiyat;
                }
                decimal kdv = (decimal)kapasitifTutar * kdvOran;

                fdKap = new FaturaDetayDTO()
                {
                    KalemAdi = EnumThkKod.ReaktifKapasitif.ToString(),
                    BirimFiyat = reaktifBrFiyat,
                    KalemKod = EnumThkKod.ReaktifKapasitif,
                    KdvOran = kdvOran,
                    KdvTuar = kdv,
                    Kwh = (decimal)endex.KapasitifUsage,
                    Tutar = (decimal)kapasitifTutar
                };
            }

            if (fdKap.Tutar > fdInd.Tutar)
                return fdKap;
            else return fdInd;
        }

        //Ustteki kurallarin karışığı
        public CezaOranDTO CezaOranlari(double aboneKuruluGuc)
        {
            var dKapasitifAltLimit = 15;
            var dEnduktifCezaOrani = 0.0;
            var dKapasitifCezaOrani = 0.0;
            var dSekonderOran = 0.035;
            var dEndKapDegLmtOrani = 50;


            var dEndCezaLimitOrani = 0.0;
            var dKapCezaLimitOrani = 0.0;

            if (aboneKuruluGuc >= dEndKapDegLmtOrani)
            {
                dEndCezaLimitOrani = 0.2;
                dKapCezaLimitOrani = 0.15;
            }
            else
            {
                dEndCezaLimitOrani = 0.33;
                dKapCezaLimitOrani = 0.2;
            }
            return new CezaOranDTO()
            {
                dEndKapDegLmtOrani = dEndKapDegLmtOrani,
                dEnduktifCezaOrani = dEnduktifCezaOrani,
                dKapasitifAltLimit = dKapasitifAltLimit,
                dSekonderOran = dSekonderOran,
                dKapasitifCezaOrani = dKapasitifCezaOrani

            };
        }
    }
}
