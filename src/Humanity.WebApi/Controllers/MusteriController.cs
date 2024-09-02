using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
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


        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMusteriRes>> Get(int id)
        {
            var result = await _musteriService.GetMusteriById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRes>> Create(CreateMusteriReq musteri)
        {
            var result = await _musteriService.CreateMusteri(musteri);
            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<CreateCariKartRes>> Update(UpdateMusteriReq musteri)
        {
            var result = await _musteriService.Update(musteri);
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
        public async Task<ActionResult<GetAllActiveMusteriRes>> CariyeBagliUreticiGetir([FromQuery] int cariId)
        {
            var result = await _musteriService.CariyeBagliUreticiGetir(cariId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> MusteriyeBagliTuketicileriGetir([FromQuery]int aboneureticiId)
        {
            var result = await _musteriService.MusteriyeBagliTuketicileriGetir(aboneureticiId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<GetAllActiveMusteriRes>> GetBagimsizTuketiciler([FromQuery] int cariId)
        {
            var result = await _musteriService.GetBagimsizTuketiciler(cariId);
            return Ok(result);
        }
    }
}
