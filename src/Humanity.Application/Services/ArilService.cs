using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public ArilService(IUnitOfWork unitOfWork, IFirmaService firmaService)
        {
            _unitOfWork = unitOfWork;
            _firmaService = firmaService;
            // _ = GetToken();
        }

        public async Task GetToken(int firmaId, MusteriEntegrasyon musteriEntegrasyon)
        {
            //dağıtım firmaya göre dinamik gelecek.
            string tokenUrl = "aril-portalserver/customer-rest-api/generate-token";

            string firmaArilAdres = "https://osos.dedas.com.tr";
            firmaArilAdres = "https://ososout.oedas.com.tr/";

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
                Console.WriteLine($"Token alındı: {token}");
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


            if (entegre != null && entegre.Count > 0)
            {
                await GetToken(entegre.First().Musteri.DagitimFirmaId, entegre.First());
            }
            else
            {
                throw new Exception("Entegrasyon Bilgileri Bulunamadı");
            }


            var postData = new
            {
                PageNumber = 1,
                PageSize = 100
            };



            var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");


            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("aril-service-token", token);


            string firmaArilAdres = "https://osos.dedas.com.tr/";
            firmaArilAdres = "https://ososout.oedas.com.tr/";

            string url = "aril-portalserver/customer-rest-api/proxy-aril/GetCustomerPortalSubscriptions";
            url = firmaArilAdres + url;
            
                var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CustomerSubscriptionResponse>(json);
            }
            return null;
        }
        public async Task<GetOwnerConsumptionsResponse> GetOwnerConsumptions(int musteriId, DateTime startDate, DateTime endDate)
        {
            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                // Her ay için tarih aralığını ayarlıyoruz
                var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddMinutes(-1); // Ayın son günü

                string donem = currentDate.Year.ToString() + "/" + currentDate.Month.ToString().PadLeft(2, '0');
                var customers = await GetCustomerPortalSubscriptions(musteriId);

                if (customers != null)
                {
                    foreach (var result in customers.ResultList)
                    {

                        var tumData = await _unitOfWork.Repository<OwnerConsumpiton>().ListAsync(new BaseSpecification<OwnerConsumpiton>(x => x.SerNo == result.SubscriptionSerno));
                        if (tumData.Count(a => a.Donem == donem) > 0)
                            continue;

                        var consumpiton = await GetOwnerConsumption(result.SubscriptionSerno, startOfMonth, endOfMonth);

                        var jsonStr = JsonSerializer.Serialize(consumpiton, new JsonSerializerOptions { WriteIndented = true });
                        await _unitOfWork.Repository<OwnerConsumpiton>().AddAsync(new OwnerConsumpiton() { Donem = donem, Firma = "dedas", Json = jsonStr, SerNo = result.SubscriptionSerno });
                        // Bir sonraki aya geçiyoruz
                        currentDate = currentDate.AddMonths(1);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return new GetOwnerConsumptionsResponse() { };
        }

        static async Task<object> GetOwnerConsumption(int serno, DateTime startDate, DateTime endDate)
        {
            // Post isteği için body verisi
            var postData = new
            {
                OwnerSerno = serno,  // Dinamik olarak serno geliyor
                StartDate = startDate.ToString("yyyyMMddHHmmss"), // Başlangıç tarihi (sabit)
                EndDate = endDate.ToString("yyyyMMddHHmmss"),   // Bitiş tarihi (sabit)
                PageSize = 1000,            // Sayfa boyutu (sabit)
                PageNumber = 1,             // Sayfa numarası (sabit)
                IsOnlySuccess = true,       // Sadece başarılı kayıtlar (sabit)
                IncludeLoadProfiles = false, // Yük profilleri dahil değil
                WithoutMultiplier = false,  // Çarpansız veri
                MergeResult = false         // Sonuçları birleştirme
            };

            // Post verisini JSON formatına çevirip istek gönderiyoruz
            var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

            // aril-service-token header'ı eklenmiş durumda
            string url = "https://osos.dedas.com.tr/aril-portalserver/customer-rest-api/proxy-aril/GetOwnerConsumptions";
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<object>(json);  // Gelen cevabı deserialize et ve döndür
            }
            else
            {
                Console.WriteLine($"Owner Consumptions API çağrısı başarısız. Status Code: {response.StatusCode}");
                return null;
            }
        }



    }
}
