using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashBoardService;
        private readonly ILoggerService _logService;

        public DashboardController(IDashboardService dashBoardService, ILoggerService loggerService)
        {
            _dashBoardService = dashBoardService;
            _logService = loggerService;
        }

        [HttpGet("AboneAylikTuketimGetir")]

        public async Task<ActionResult<List<SaatlikEndeksRes>>> AboneAylikTuketimGetir()
        {
            var result = await _dashBoardService.AboneAylikTuketimGetir();
            return Ok(result);
        }

        [HttpGet("AboneUretimTuketimKarsilastirma")]

        public async Task<ActionResult<List<YillikUretimTuketim>>> AboneUretimTuketimKarsilastirma()
        {
            var result = await _dashBoardService.AboneUretimTuketimKarsilastirma();
            return Ok(result);
        }

        [HttpGet("AylikUretimTuketimKarsilastirma")]

        public async Task<ActionResult<List<YillikUretimTuketim>>> AylikUretimTuketimKarsilastirma()
        {
            var result = await _dashBoardService.AylikUretimTuketimKarsilastirma();
            return Ok(result);
        }

        [HttpGet("AylikBazdaTumAbonelerTuketimSummary")]

        public async Task<ActionResult<List<AylikBazdaTumAbonelerTuketimSummary>>> AylikBazdaTumAbonelerTuketimSummary()
        {
            var result = await _dashBoardService.YillikToplamTuketimGetir();
            return Ok(result);
        }

        [HttpGet("AylikBazdaUretimSummary")]

        public async Task<ActionResult<List<AylikBazdaTumAbonelerTuketimSummary>>> AylikBazdaTumAbonelerUretimSummary()
        {
            var result = await _dashBoardService.YillikToplamUretimmGetir();
            return Ok(result);
        }
    }
}
