using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<ActionResult<CreateAboneRes>> Create(AboneDTO abone)
        {
            var result = await _aboneService.CreateAbone(abone);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CreateMusteriReq>> Update(AboneDTO abone)
        {
            var result = await _aboneService.Update(abone);
            return Ok(result);
        }


        //[HttpGet("{musteriid:int}")]
        //public async Task<ActionResult<GetAboneRes>> GetMusteriAboneler(int musteriid)
        //{
        //    var result = await _aboneService.GetMusteriAboneler(musteriid);
        //    return Ok(result);
        //}


        [HttpGet("GetFirmaAboneler")]
        public async Task<ActionResult<GetAboneResList>> GetFirmaAboneler()
        {
            var result = await _aboneService.GetFirmaAboneler();
            return Ok(result);
        }

        [HttpGet("GetBagimsizTuketiciler")]
        public async Task<ActionResult<GetTuketiciListRes>> GetBagimsizTuketiciler([FromQuery] int musteriId)
        {
            var result = await _aboneService.GetBagimsizTuketiciler(musteriId);
            return Ok(result);
        }
    }
}
