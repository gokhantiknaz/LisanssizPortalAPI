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

            var result = await _arilService.GetOwnerConsumption(-1,aboneId, basTarih, sonTarih);
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
    }
}
