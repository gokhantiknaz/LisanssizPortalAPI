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
        public async Task<ActionResult<CreateCariKartRes>> Create(CreateCariKartReq cariKart)
        {
            var result = await _cariService.Create(cariKart);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CreateCariKartRes>> Update(UpdateCariKartReq cariKart)
        {
            var result = await _cariService.Update(cariKart);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CreateCariKartRes>> Delete(int cariId)
        {
            var result = await _cariService.Delete(cariId);
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
