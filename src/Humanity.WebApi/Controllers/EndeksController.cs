using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Humanity.Application.Models.Requests;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndeksController : Controller
    {
        private readonly IEndeksService _endeksService;
        private readonly ILoggerService _logService;

        public EndeksController(IEndeksService endeksService, ILoggerService loggerService)
        {
            _endeksService = endeksService;
            _logService = loggerService;
        }


        [HttpGet]
        public async Task<ActionResult<AylikEndeksRes>> Get([FromQuery] int musteriId, [FromQuery] string donem)
        {
            var result = await _endeksService.GetAboneDonemEndeks(musteriId, donem);
            return Ok(result);
        }

        [HttpGet("GetSaatlikEndeks")]

        public async Task<ActionResult<List<SaatlikEndeksRes>>> GetSaatlikEndeks([FromQuery] int musteriId, [FromQuery] string donem)
        {
            var result = await _endeksService.GetAboneSaatlikEndeks(musteriId, donem);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SaatlikEndeksRes>> Create(List<SaatlikEndeksRequest> endeksReq)
        {
            var result = await _endeksService.Create(endeksReq);
            return Ok(result);
        }
    }
}
