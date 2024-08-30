using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MusteriController : Controller
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRes>> CreateUser(CreateMusteriReq musteri)
        {
            var result = await _musteriService.CreateMusteri(musteri);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ValidateUserRes>> ValidateUser(ValidateMusteriReq req)
        {
            var result = await _musteriService.ValidateMusteri(req);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> GetAllActiveMusteri()
        {
            var result = await _musteriService.GetAllMusteri();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> GetAllActiveMusteriUretici()
        {
            var result = await _musteriService.GetAllMusteriUretici();
            return Ok(result);
        }
    }
}
