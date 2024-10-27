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

        [HttpPost]
        public async Task<ActionResult<GetOwnerConsumptionsResponse>> Create(GetOwnerConsumptionsRequest request)
        {
            DateTime pilktarih = DateTime.ParseExact(request.StartDate, "yyyyMMddHHmmss", null);
            DateTime pSonTarih = DateTime.ParseExact(request.EndDate, "yyyyMMddHHmmss", null);

            var result = await _arilService.GetOwnerConsumptions(-1,pilktarih, pSonTarih);
            return Ok(result);
        }

        [HttpGet("GetEndOfMonthEndexes")]
        public async Task<ActionResult<GetEndOfMonthEndexesResponse>> GetEndOfMonthEndexes([FromQuery] int aboneId, [FromQuery] string donem, [FromQuery] string donemSon)
        {
          
            var result = await _arilService.GetEndOfMonthEndexes(aboneId,donem, donemSon,true);
            return Ok(result);
        }

        [HttpGet("GetCurrentEndexes")]
        public async Task<ActionResult<GetEndOfMonthEndexesResponse>> GetCurrentEndexes([FromQuery] int aboneId)
        {
            var result = await _arilService.GetCurrentEndexes(aboneId,true);
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
