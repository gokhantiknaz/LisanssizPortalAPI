using Humanity.Application.Core.Services;
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
    }
}
