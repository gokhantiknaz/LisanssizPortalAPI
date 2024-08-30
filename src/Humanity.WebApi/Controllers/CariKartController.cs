using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CariKartController:Controller
    {
        private readonly ICariKartService _cariService;

        public CariKartController(ICariKartService cariService)
        {
            _cariService = cariService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCariKartRes>> Create(CreateCariKartReq musteri)
        {
            var result = await _cariService.Create(musteri);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CreateCariKartRes>> Update(UpdateCariKartReq musteri)
        {
            var result = await _cariService.Update(musteri);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllActiveCariKartRes>> GetAll()
        {
            var result = await _cariService.GetAllCariKart();
            return Ok(result);
        }
    }
}
