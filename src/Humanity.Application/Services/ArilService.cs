﻿using AutoMapper;
using Humanity.Application.Core.Mapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Humanity.Application.Services
{
    public class ArilService : IArilService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string token = string.Empty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirmaService _firmaService;
        private readonly IEndeksService _endeksService;
        private readonly IMapper mapper;

        public ArilService(IUnitOfWork unitOfWork, IFirmaService firmaService, IEndeksService endeksService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _firmaService = firmaService;
            _endeksService = endeksService;
            this.mapper = mapper;
            // _ = GetToken();
        }

        public async Task GetToken(MusteriEntegrasyon musteriEntegrasyon)
        {
            //dağıtım firmaya göre dinamik gelecek.
            string tokenUrl = "aril-portalserver/customer-rest-api/generate-token";

            if (!musteriEntegrasyon.ServisAdres.EndsWith(".com.tr/"))
            {
                throw new Exception("Entegrasyon Bilgileri Hatalı");
            }
            string firmaArilAdres = musteriEntegrasyon.ServisAdres;


            tokenUrl = firmaArilAdres + "aril-portalserver/customer-rest-api/generate-token";
            var credentials = new
            {
                UserCode = musteriEntegrasyon.KullaniciAdi,
                Password = musteriEntegrasyon.Sifre
            };

            var content = new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(tokenUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<string>(json);
                token = tokenResponse;
                //Console.WriteLine($"Token alındı: {token}");
            }
            else
            {
                token = "";
                throw new Exception("Entegrasyon Bilgileri Hatalı");
            }
        }

        public async Task<CustomerSubscriptionResponse> GetCustomerPortalSubscriptions(int musteriid)
        {
            // müşteriye ait token al.
            var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriid);
            spec.AddInclude(a => a.Musteri);
            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);
            var retVal = new CustomerSubscriptionResponse() { ResultList = new List<CustomerSubscription>() };
            if (entegre != null && entegre.Count > 0)
            {
                foreach (var entegrasyon in entegre)
                {
                    await GetToken(entegrasyon);

                    var postData = new
                    {
                        PageNumber = 1,
                        PageSize = 100
                    };

                    var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("aril-service-token", token);

                    if (!entegrasyon.ServisAdres.EndsWith(".com.tr/"))
                    {
                        throw new Exception("Entegrasyon Bilgileri Hatalı");
                    }
                    string firmaArilAdres = entegrasyon.ServisAdres;

                    string url = "aril-portalserver/customer-rest-api/proxy-aril/GetCustomerPortalSubscriptions";
                    url = firmaArilAdres + url;

                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var customers = JsonSerializer.Deserialize<CustomerSubscriptionResponse>(json);
                        foreach (var r in customers.ResultList)
                        {
                            r.DagitimFİrmaId = entegrasyon.DagitimFirmaId;
                        }
                        retVal.ResultList.AddRange(customers.ResultList);
                    }
                }

                return retVal;
            }
            else
            {
                throw new Exception("Entegrasyon Bilgileri Bulunamadı");
            }
            return null;
        }

        // müşteriye bağlı tüm abonelerin saatlik datasını alan servis.
        public async Task<GetOwnerConsumptionsResponse> GetOwnerConsumptions(int musteriId, DateTime startDate, DateTime endDate)
        {
            var currentDate = startDate;
            var customers = await GetCustomerPortalSubscriptions(musteriId);

            if (customers != null)
            {
                while (currentDate <= endDate)
                {
                    // Her ay için tarih aralığını ayarlıyoruz
                    //var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                    //var endOfMonth = startOfMonth.AddMonths(1).AddMinutes(-1); // Ayın son günü
                    foreach (var result in customers.ResultList)
                    {
                        string donem = currentDate.Year.ToString() + "/" + currentDate.Month.ToString().PadLeft(2, '0');
                        var abone = await _unitOfWork.Repository<Abone>().GetBy(new BaseSpecification<Abone>(a => a.SeriNo == result.SubscriptionSerno));
                        if (abone == null)
                            continue;

                        var consumpiton = await GetOwnerConsumption(result.SubscriptionSerno, -1, startDate, endDate);
                        // Bir sonraki aya geçiyoruz
                        currentDate = currentDate.AddMonths(1);
                    }

                }
                await _unitOfWork.SaveChangesAsync();
            }

            return new GetOwnerConsumptionsResponse() { };
        }

        //müşteri saatlik data
        public async Task<GetOwnerConsumptionsResponse> GetOwnerConsumption(int seriNo, int aboneId, DateTime startDate, DateTime endDate)
        {
            // Post isteği için body verisi
            Abone abone = null;
            if (aboneId != -1)
                abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneId);
            else if (seriNo != -1)
                abone = await _unitOfWork.Repository<Abone>().GetBy(new BaseSpecification<Abone>(a => a.SeriNo == seriNo));
            if (abone == null)
                throw new Exception("Abone Bulunamadı");

            string donem = startDate.Year.ToString() + "/" + startDate.Month.ToString().PadLeft(2, '0');


            var startProfileDate = Convert.ToInt64(startDate.Year.ToString() + startDate.Month.ToString() + startDate.Day.ToString() + startDate.Hour.ToString() + startDate.Minute.ToString() + "00");
            var endProfileDate = Convert.ToInt64(endDate.Year.ToString() + endDate.Month.ToString() + endDate.Day.ToString() + endDate.Hour.ToString() + endDate.Minute.ToString() + "00");
            // bu donem vars silip tekrar ceksin.

            var endekler = await _unitOfWork.Repository<AboneSaatlikEndeks>().ListAsync(new BaseSpecification<AboneSaatlikEndeks>(
                x => x.AboneId == abone.Id && x.Donem == donem
                && x.ProfilDate >= startProfileDate && x.ProfilDate <= endProfileDate
                ));

            foreach (var item in endekler)
            {
                _unitOfWork.Repository<AboneSaatlikEndeks>().Delete(item);
            }

            await _unitOfWork.SaveChangesAsync();

            var postData = new
            {
                OwnerSerno = abone.SeriNo,  // Dinamik olarak serno geliyor
                StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                PageSize = 1000,            // Sayfa boyutu (sabit)
                PageNumber = 1,             // Sayfa numarası (sabit)
                IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                IncludeLoadProfiles = false, // Yük profilleri dahil değil
                WithoutMultiplier = false,  // Çarpansız veri
                MergeResult = true         // Sonuçları birleştirme
            };

            var entegrasyonSpec = new BaseSpecification<MusteriEntegrasyon>(a => a.DagitimFirmaId == abone.DagitimFirmaId && a.MusteriId == abone.MusteriId);
            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().GetBy(entegrasyonSpec);
            if (!entegre.ServisAdres.EndsWith(".com.tr/"))
            {
                throw new Exception("Entegrasyon Bilgileri Hatalı");
            }

            await GetToken(entegre);
            var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("aril-service-token", token);

            string firmaArilAdres = entegre.ServisAdres;

            string url = "aril-portalserver/customer-rest-api/proxy-aril/GetOwnerConsumptions";
            url = firmaArilAdres + url;

            //string url = "https://osos.dedas.com.tr/aril-portalserver/customer-rest-api/proxy-aril/GetOwnerConsumptions";
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var saatlikDatalar = JsonSerializer.Deserialize<ArilSaatlikResponse>(json);
                var saatlikEndkeksler = mapper.Map<List<AboneSaatlikEndeks>>(saatlikDatalar.MergedConsumptions);

                //var saatlikEndkekslerUretim = mapper.Map<List<AboneSaatlikEndeks>>(saatlikDatalar.OutConsumption);
                if (saatlikEndkeksler.Count > 0)
                {
                    foreach (var item in saatlikEndkeksler)
                    {
                        item.AboneId = abone.Id;
                        item.CreatedOn = DateTime.Now;
                        item.LastModifiedOn = DateTime.Now;
                        item.CreatedBy = new Guid();
                        item.LastModifiedBy = new Guid();
                    }

                    var saatlikEndeks = await _unitOfWork.Repository<AboneSaatlikEndeks>().AddRandeAsync(saatlikEndkeksler);
                    await _unitOfWork.SaveChangesAsync();
                }

                return new GetOwnerConsumptionsResponse() { };
            }
            else
            {
                //Console.WriteLine($"Owner Consumptions API çağrısı başarısız. Status Code: {response.StatusCode}");
                return null;
            }
        }

        //müşteri aylık data
        public async Task<GetEndOfMonthEndexesResponse> GetEndOfMonthEndexes(int aboneid, string donem, string donemSon, bool kaydet = false, bool uretimEndeksAl = false)
        {
            // Post isteği için body verisi

            var startDate = Convert.ToDateTime(donem + "/01");
            var endDate = Convert.ToDateTime(donemSon + "/01").AddMonths(1);

            // eğer güncel ayda ise diğer metoda gidelim. buradan sonuç dönmez

            if (startDate.Month == DateTime.Now.Month && startDate.Year == DateTime.Now.Year)
            {
                var result = await GetCurrentEndexes(aboneid, kaydet);
                return result;
            }


            var abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneid);

            var postData = new
            {
                OwnerSerno = abone.SeriNo,  // Dinamik olarak serno geliyor
                StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                PageSize = 1000,            // Sayfa boyutu (sabit)
                PageNumber = 1,             // Sayfa numarası (sabit)
                IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                IncludeLoadProfiles = false, // Yük profilleri dahil değil
                WithoutMultiplier = false,  // Çarpansız veri
                MergeResult = false,        // Sonuçları birleştirme
                EndexDirection = 0 // 1 üretim endeksi 0 tüketim endeksi
            };

            var entegreList = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(new BaseSpecification<MusteriEntegrasyon>(a => a.MusteriId == abone.MusteriId));
            var entegre = entegreList.FirstOrDefault(a => a.DagitimFirmaId == abone.DagitimFirmaId);

            await GetToken(entegre);

            // Post verisini JSON formatına çevirip istek gönderiyoruz
            var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("aril-service-token", token);

            // aril-service-token header'ı eklenmiş durumda

            if (!entegre.ServisAdres.EndsWith(".com.tr/"))
            {
                throw new Exception("Entegrasyon Bilgileri Hatalı");
            }
            string firmaArilAdres = entegre.ServisAdres;

            string url = "aril-portalserver/customer-rest-api/proxy-aril/GetEndOfMonthEndexes";
            url = firmaArilAdres + url;

            //string url = "https://osos.dedas.com.tr/aril-portalserver/customer-rest-api/proxy-aril/GetEndOfMonthEndexes";
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<GetEndOfMonthEndexesResponse>(json);

                if (kaydet)
                {
                    await _endeksService.AylikEndeksKaydet(aboneid, res);

                    if (uretimEndeksAl)
                    {
                        postData = new
                        {
                            OwnerSerno = abone.SeriNo,  // Dinamik olarak serno geliyor
                            StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                            EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                            PageSize = 1000,            // Sayfa boyutu (sabit)
                            PageNumber = 1,             // Sayfa numarası (sabit)
                            IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                            IncludeLoadProfiles = false, // Yük profilleri dahil değil
                            WithoutMultiplier = false,  // Çarpansız veri
                            MergeResult = false,        // Sonuçları birleştirme
                            EndexDirection = 1 // 1 üretim endeksi 0 tüketim endeksi
                        };


                        content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
                        var responseUretim = await client.PostAsync(url, content);

                        var jsonUretim = await responseUretim.Content.ReadAsStringAsync();
                        var resUretim = JsonSerializer.Deserialize<GetEndOfMonthEndexesResponse>(jsonUretim);
                        await _endeksService.AylikEndeksKaydet(aboneid, resUretim);

                    }
                }
                return res;  // Gelen cevabı deserialize et ve döndür
            }
            else
            {
                //Console.WriteLine($"Owner Consumptions API çağrısı başarısız. Status Code: {response.StatusCode}");
                return null;
            }
        }

        //müşteri aylık data
        public async Task<GetEndOfMonthEndexesResponse> GetCurrentEndexes(int aboneid, bool kaydet = false)
        {
            // Post isteği için body verisi

            var startDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/01");
            var endDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/01").AddMonths(1);
            var abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneid);


            var postData = new
            {
                OwnerSerno = abone.SeriNo,  // Dinamik olarak serno geliyor
                StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                PageSize = 1000,            // Sayfa boyutu (sabit)
                PageNumber = 1,             // Sayfa numarası (sabit)
                IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                IncludeLoadProfiles = false, // Yük profilleri dahil değil
                WithoutMultiplier = false,  // Çarpansız veri
                MergeResult = false,// Sonuçları birleştirme
                EndexDirection = 0 // 1 üretim endeksi 0 tüketim endeksi
            };

            var entegreList = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(new BaseSpecification<MusteriEntegrasyon>(a => a.DagitimFirmaId == abone.DagitimFirmaId && a.MusteriId == abone.MusteriId));
            var entegre = entegreList.FirstOrDefault();

            await GetToken(entegre);

            // Post verisini JSON formatına çevirip istek gönderiyoruz
            var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("aril-service-token", token);

            // aril-service-token header'ı eklenmiş durumda

            if (!entegre.ServisAdres.EndsWith(".com.tr/"))
            {
                throw new Exception("Entegrasyon Bilgileri Hatalı");
            }
            string firmaArilAdres = entegre.ServisAdres;

            string url = "aril-portalserver/customer-rest-api/proxy-aril/GetCurrentEndexes";
            url = firmaArilAdres + url;

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<GetEndOfMonthEndexesResponse>(json);

                if (kaydet)
                {
                    await _endeksService.AylikEndeksKaydet(aboneid, res);
                }


                if (abone.SahisTip == Domain.Enums.Enums.SahisTip.Uretici)
                {
                    postData = new
                    {
                        OwnerSerno = abone.SeriNo,  // Dinamik olarak serno geliyor
                        StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                        EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                        PageSize = 1000,            // Sayfa boyutu (sabit)
                        PageNumber = 1,             // Sayfa numarası (sabit)
                        IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                        IncludeLoadProfiles = false, // Yük profilleri dahil değil
                        WithoutMultiplier = false,  // Çarpansız veri
                        MergeResult = false,// Sonuçları birleştirme
                        EndexDirection = 1 // 1 üretim endeksi 0 tüketim endeksi
                    };

                    content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

                    response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        json = await response.Content.ReadAsStringAsync();
                        res = JsonSerializer.Deserialize<GetEndOfMonthEndexesResponse>(json);

                        if (kaydet)
                        {
                            await _endeksService.AylikEndeksKaydet(aboneid, res);
                        }
                    }
                }
                return res;  // Gelen cevabı deserialize et ve döndür
            }
            else
            {
                //Console.WriteLine($"Owner Consumptions API çağrısı başarısız. Status Code: {response.StatusCode}");
                return null;
            }
        }

        // tüm abonelerin aktif aya ait endekslerini
        public async Task<object> GetCurrentEndexesAll(int musteriId)
        {
            var customers = await GetCustomerPortalSubscriptions(musteriId);

            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddMinutes(-1); // Ayın son günü
            var currentDate = DateTime.Now;

            var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriId);
            spec.AddInclude(a => a.Musteri);
            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);

            List<GetEndOfMonthEndexesResponse> retVal = new List<GetEndOfMonthEndexesResponse>();
            List<EndexData> endexDatas = new List<EndexData>();

            string donem = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');
            if (customers != null)
            {
                foreach (var result in customers.ResultList)
                {
                    var abone = await _unitOfWork.Repository<Abone>().GetBy(new BaseSpecification<Abone>(x => x.SeriNo == result.SubscriptionSerno));
                    if (abone == null)
                        continue;
                    var currentEndex = await GetCurrentEndexes(abone.Id);
                    endexDatas.AddRange(currentEndex.ResultList);
                    retVal.Add(currentEndex);
                }
            }

            var groupedData = endexDatas
          .GroupBy(r => new
          {
              r.SensorSerno,
              YearMonth = r.EndexDate.ToString().Substring(0, 6)
          })
          .Select(g => new
          {
              SensorSerno = g.Key.SensorSerno,
              EndexDate = g.Key.YearMonth,
              T1Endex = g.Sum(x => x.T1Endex),
              T2Endex = g.Sum(x => x.T2Endex),
              T3Endex = g.Sum(x => x.T3Endex),
          })
          .ToList();

            return groupedData;
        }


        public async Task<DateTime?> GetVeriDurumuAsync()
        {
            //var endeksVeriDurumu = await _unitOfWork.Repository<EndeksVeriDurumu>().GetBy(new BaseSpecification<EndeksVeriDurumu>(x => x.Tarih.Date == tarih.Date));
            //if (endeksVeriDurumu != null)
            //{
            //    return true;
            //}
            //return false;


            var query = new BaseSpecification<EndeksVeriDurumu>(x=>x.VeriCekildi==true);
            query.ApplyOrderByDescending(a => a.Tarih);

            var lastData = await _unitOfWork.Repository<EndeksVeriDurumu>().GetBy(query);
      

            return lastData?.Tarih;
        }

        public async Task<bool> FetchAndSaveDataAsync(DateTime tarih)
        {
            // Servisten verileri çek ve kaydet
            var musteriler = await _unitOfWork.Repository<Musteri>().ListAsync(new BaseSpecification<Musteri>(a => a.Durum == Domain.Enums.Enums.Status.Aktif));

            foreach (var item in musteriler)
            {
                await GetOwnerConsumptions(item.Id, tarih.AddDays(-1), tarih);
            }

            // Veri durumu güncelle
            var durum = new EndeksVeriDurumu() { Tarih = tarih, VeriCekildi = true };
            await _unitOfWork.Repository<EndeksVeriDurumu>().AddAsync(durum);
            var inserted =
             await _unitOfWork.SaveChangesAsync();
            return inserted > 0;
        }
    }
}
