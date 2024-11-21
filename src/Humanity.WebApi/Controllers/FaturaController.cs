using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FaturaController : Controller
    {
        private readonly IFaturaService _faturaService;

        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }

        [HttpGet]
        public async Task<ActionResult<AylikEndeksRes>> Get([FromQuery] int aboneId, [FromQuery] string donem)
        {
            var result = await _faturaService.AboneAylikFaturaHesapla(aboneId, donem);
            return Ok(result);
        }
    }
}
