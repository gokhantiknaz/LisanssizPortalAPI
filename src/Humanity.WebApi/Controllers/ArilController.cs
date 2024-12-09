using Azure.Core;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Application.Services;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ArilController : Controller
    {
        private readonly IArilService _arilService;

        public ArilController(IArilService arilService)
        {
            _arilService = arilService;
        }


        // müşteriye ait tüm  abonelerde veri ceker
        [HttpPost("GetOwnerConsumptions")]
        public async Task<ActionResult<GetOwnerConsumptionsResponse>> GetOwnerConsumptions([FromQuery] int aboneId, [FromQuery] string donem, [FromQuery] string donemSon)
        {

            int donemYear = Convert.ToInt32(donem.Split('/')[0]);
            int donemMonth = Convert.ToInt32(donem.Split('/')[1]);


            int donemSonYear = Convert.ToInt32(donemSon.Split('/')[0]);
            int donemSonMonth = Convert.ToInt32(donemSon.Split('/')[1]);

            DateTime basTarih = new DateTime(donemYear, donemMonth, DateTime.DaysInMonth(donemYear, donemMonth));
            DateTime sonTarih = new DateTime(donemSonYear, donemSonMonth, DateTime.DaysInMonth(donemSonYear, donemSonMonth));

            var result = await _arilService.GetOwnerConsumptions(aboneId, basTarih, sonTarih);
            return Ok(result);
        }

        // abone saatlik veri ceker
        [HttpGet("GetOwnerConsumption")]
        public async Task<ActionResult<GetOwnerConsumptionsResponse>> GetOwnerConsumption([FromQuery] int aboneId, [FromQuery] string donem, [FromQuery] string donemSon)
        {

            int donemYear = Convert.ToInt32(donem.Split('/')[0]);
            int donemMonth = Convert.ToInt32(donem.Split('/')[1]);


            int donemSonYear = Convert.ToInt32(donemSon.Split('/')[0]);
            int donemSonMonth = Convert.ToInt32(donemSon.Split('/')[1]);

            DateTime basTarih = new DateTime(donemYear, donemMonth, 1);
            DateTime sonTarih = new DateTime(donemSonYear, donemSonMonth, DateTime.DaysInMonth(donemSonYear, donemSonMonth)).AddDays(1);

            var result = await _arilService.GetOwnerConsumption(-1, aboneId, basTarih, sonTarih);
            return Ok(result);
        }

        [HttpGet("GetEndOfMonthEndexes")]
        public async Task<ActionResult<GetEndOfMonthEndexesResponse>> GetEndOfMonthEndexes([FromQuery] int aboneId, [FromQuery] string donem, [FromQuery] string donemSon)
        {

            var result = await _arilService.GetEndOfMonthEndexes(aboneId, donem, donemSon, true);
            return Ok(result);
        }

        [HttpGet("GetCurrentEndexes")]
        public async Task<ActionResult<GetEndOfMonthEndexesResponse>> GetCurrentEndexes([FromQuery] int aboneId)
        {
            var result = await _arilService.GetCurrentEndexes(aboneId, true);
            return Ok(result);
        }

        [HttpGet("GetCurrentEndexesAll")]
        public async Task<ActionResult<GetEndOfMonthEndexesResponse>> GetCurrentEndexesAll([FromQuery] int musteriId)
        {
            var result = await _arilService.GetCurrentEndexesAll(musteriId);
            return Ok(result);
        }

        [HttpPost("check-and-fetch")]
        public async Task<IActionResult> CheckAndFetchData()
        {

            var today = DateTime.UtcNow.Date;

            // Son alınan verinin tarihini al
            var lastDataDate = await _arilService.GetVeriDurumuAsync(); // Tarihi döndürmelidir

            if (lastDataDate == null)
            {
                lastDataDate = DateTime.MinValue;
            }

            // Eksik günleri bul
            var missingDates = new List<DateTime>();
            for (var date = lastDataDate.Value.AddDays(1); date <= today; date = date.AddDays(1))
            {
                missingDates.Add(date);
            }

            if (!missingDates.Any())
            {
                return Ok(new { message = "Tüm veriler güncel. Yeni bir işlem gerekmez." });
            }

            // Eksik tarihler için veri çek ve kaydet
            var results = new List<object>();

            foreach (var date in missingDates)
            {
                var result = await _arilService.FetchAndSaveDataAsync(date);
                results.Add(new
                {
                    date = date,
                    success = result,
                    message = result ? $"{date:yyyy-MM-dd} tarihli veriler başarıyla çekildi." : $"{date:yyyy-MM-dd} tarihli veriler çekilemedi."
                });
            }

            // Sonuçları döndür
            return Ok(new { message = "Eksik veriler kontrol edildi ve tamamlandı.", results });



            //var today = DateTime.UtcNow.Date;
            //var yesterday = today.AddDays(-1);

            //// Bugün ve dünün veri durumlarını kontrol et
            //var todayDataStatus = await _arilService.GetVeriDurumuAsync(today);
            //var yesterdayDataStatus = await _arilService.GetVeriDurumuAsync(yesterday);

            //// Sonuçları kaydetmek için liste veya dictionary kullanabilirsiniz
            //var results = new List<object>();

            //// Eğer bugünün verileri eksikse, çek ve kaydet
            //if (!todayDataStatus)
            //{
            //    var todayResult = await _arilService.FetchAndSaveDataAsync(today);
            //    results.Add(new
            //    {
            //        date = today,
            //        success = todayResult,
            //        message = todayResult ? "Bugünün verileri başarıyla çekildi." : "Bugünün verileri çekilemedi."
            //    });
            //}

            //// Eğer dünün verileri eksikse, çek ve kaydet
            //if (!yesterdayDataStatus)
            //{
            //    var yesterdayResult = await _arilService.FetchAndSaveDataAsync(yesterday);
            //    results.Add(new
            //    {
            //        date = yesterday,
            //        success = yesterdayResult,
            //        message = yesterdayResult ? "Dünün verileri başarıyla çekildi." : "Dünün verileri çekilemedi."
            //    });
            //}

            //// Eğer hem bugünün hem de dünün verileri zaten çekilmişse
            //if (results.Count == 0)
            //{
            //    return Ok(new { message = "Bugün ve dün için veriler zaten çekilmiş." });
            //}

            //// Çekilen verileri döndür
            //return Ok(new { message = "Veri kontrol ve çekme işlemi tamamlandı.", results });
        }
    }
}
