using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Domain.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public class MusteriController : Controller
    {
        private readonly IMusteriService _musteriService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IArilService _arilService;


        public MusteriController(IMusteriService musteriService, IUnitOfWork unitOfWork, IArilService arilService)
        {
            _musteriService = musteriService;
            _unitOfWork = unitOfWork;
            _arilService = arilService; 
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMusteriRes>> Get(int id)
        {
            var result = await _musteriService.GetMusteriById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateMusteriReq>> Create(CreateMusteriReq musteri)
        {
            var result = await _musteriService.CreateMusteri(musteri);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CreateMusteriReq>> Update(UpdateMusteriReq musteri)
        {
            var result = await _musteriService.Update(musteri);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> GetAllActiveMusteri()
        {
            var result = await _musteriService.GetAllMusteri();
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> MusteriyeBagliUreticiGetir([FromQuery] int cariId)
        {
            var result = await _musteriService.MusteriyeBagliUreticiGetir(cariId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetTuketiciListRes>> MusteriyeBagliTuketicileriGetir([FromQuery]int aboneureticiId)
        {
            var result = await _musteriService.MusteriyeBagliTuketicileriGetir(aboneureticiId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<GetTuketiciListRes>> GetBagimsizTuketiciler([FromQuery] int cariId)
        {
            var result = await _musteriService.GetBagimsizTuketiciler(cariId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetTuketiciListRes>> ArilBagliTuketiciGetir([FromQuery] int musteriid)
        {
            var result = await _arilService.GetCustomerPortalSubscriptions(musteriid);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetTuketiciListRes>> ArilBagliTuketiciKaydet([FromQuery] int musteriid)
        {
            var result = await _musteriService.ArilBagliTuketiciKaydet(musteriid);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<bool>> KaydedilenAboneEndeksleriAl([FromQuery] int musteriid)
        {
            var result = await _musteriService.KaydedilenAboneEndeksleriAl(musteriid);
            return Ok(result);
        }


    }
}
