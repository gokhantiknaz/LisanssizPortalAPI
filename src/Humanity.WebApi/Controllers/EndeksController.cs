using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        public async Task<ActionResult<AylikEndeksRes>> Get([FromQuery] int aboneId, [FromQuery] string donem)
        {
            var result = await _endeksService.GetAboneDonemEndeks(aboneId, donem);
            return Ok(result);
        }

        [HttpGet("GetSaatlikEndeks")]

        public async Task<ActionResult<List<SaatlikEndeksRes>>> GetSaatlikEndeks([FromQuery] int aboneId, [FromQuery] string donem)
        {
            var result = await _endeksService.GetAboneSaatlikEndeks(aboneId, donem);
            return Ok(result);
        }

        [HttpGet("GetAboneSaatlikEndeksOzet")]

        public async Task<ActionResult<List<SaatlikEndeksRes>>> GetAboneSaatlikEndeksOzet([FromQuery] int aboneId, [FromQuery] string donem)
        {
            var result = await _endeksService.GetAboneSaatlikEndeksOzet(aboneId, donem);
            return Ok(result);
        }

        /// <summary>
        /// saatlik endeksleri kaydeden method.saatlik request alır
        /// </summary>
        /// <param name="endeksReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SaatlikEndeksRes>> Create(List<SaatlikEndeksRequest> endeksReq)
        {
            var result = await _endeksService.Create(endeksReq);
            return Ok(result);
        }
        [HttpGet("GetAboneBazliTuketim")]

        public async Task<ActionResult<List<SaatlikEndeksRes>>> GetAboneBazliTuketim()
        {
            return Ok();
        }

        
    }
}
