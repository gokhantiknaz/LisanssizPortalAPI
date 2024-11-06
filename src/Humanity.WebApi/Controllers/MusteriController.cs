using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Humanity.WebApi.Controllers
{

    [AllowAnonymous]
    public class MusteriController : CustomBaseController
    {
        private readonly IMusteriService<Musteri, MusteriDTO> _musteriService;
        private readonly IArilService _arilService;

        public MusteriController(IMusteriService<Musteri, MusteriDTO> musteriService,IArilService arilService)
        {
            _musteriService = musteriService;
            _arilService = arilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveMusteri()
        {
            return CreateActionResult(await _musteriService.GetAllMusteri());
        }

 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return CreateActionResult(await _musteriService.GetMusteriById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MusteriDTO musteri)
        {
            return CreateActionResult(await _musteriService.CreateMusteri(musteri));
          
        }


        [HttpPut]
        public async Task<IActionResult> Update(MusteriDTO musteri)
        {
            return CreateActionResult(await _musteriService.Update(musteri));
        }


        [HttpGet]
        public async Task<IActionResult> MusteriyeBagliUreticiGetir([FromQuery] int musteriid)
        {
            var result = await _musteriService.MusteriyeBagliUreticiGetir(musteriid);
            return CreateActionResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetTuketiciListRes>> MusteriyeBagliTuketicileriGetir([FromQuery] int aboneureticiId)
        {
            var result = await _musteriService.MusteriyeBagliTuketicileriGetir(aboneureticiId);
            return Ok(result);
        }


        //[HttpGet]
        //public async Task<ActionResult<GetTuketiciListRes>> GetBagimsizTuketiciler([FromQuery] int cariId)
        //{
        //    var result = await _musteriService.GetBagimsizTuketiciler(cariId);
        //    return Ok(result);
        //}

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
