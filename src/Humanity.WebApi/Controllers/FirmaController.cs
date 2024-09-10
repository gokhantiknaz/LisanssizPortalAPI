using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FirmaController : Controller
    {
        private readonly IFirmaService _firmaService;
        private readonly ILoggerService _logService;

        public FirmaController(IFirmaService firmaService, ILoggerService loggerService)
        {
            _firmaService = firmaService;
            _logService = loggerService;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<FirmaRes>> Get(int id)
        {
            var result = await _firmaService.GetById(id);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult<FirmaRes>> GetAll()
        {
            var result = await _firmaService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FirmaRes>> Create(FirmaReq firma)
        {
            var result = await _firmaService.Create(firma);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FirmaRes>> Update(FirmaReq firma)
        {
            var result = await _firmaService.Update(firma);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FirmaRes>> SoftDelete(FirmaReq firma)
        {
            var result = await _firmaService.Delete(firma.Id);
            return Ok(result);
        }
        


    }
}
