using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AboneController :  Controller
    {
        private readonly IAboneService _aboneService;
        private readonly ILoggerService _logService;

        public AboneController(IAboneService aboneService, ILoggerService loggerService)
        {
            _aboneService = aboneService;
            _logService = loggerService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetAboneRes>> Get(int id)
        {
            var result = await _aboneService.GetAboneById(id);
            return Ok(result);
        }

        [HttpGet("{musteriid:int}")]
        public async Task<ActionResult<List<GetAboneRes>>> GetMusteriAboneler(int musteriid)
        {
            var result = await _aboneService.GetMusteriAboneler(musteriid);
            return Ok(result);
        }


        [HttpGet("{firmaid:int}")]
        public async Task<ActionResult<List<GetAboneRes>>> GetFirmaAboneler()
        {
            var result = await _aboneService.GetFirmaAboneler();
            return Ok(result);
        }
    }
}
