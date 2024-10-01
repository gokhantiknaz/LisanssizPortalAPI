using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Responses;
using Humanity.Application.Services;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Humanity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifeController : Controller
    {
        //DZ9Sat4RWq3OwrCHeF0hFpUpR3AVYFVS33KQzKoR
        //https://humanity-9e850-default-rtdb.firebaseio.com/

        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "https://humanity-9e850-default-rtdb.firebaseio.com/",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = "DZ9Sat4RWq3OwrCHeF0hFpUpR3AVYFVS33KQzKoR"
        };

        // Firebase client
        IFirebaseClient client;

        private readonly ILoggerService _logService;
       
        public TarifeController(ILoggerService loggerService)
        {
            _logService = loggerService;
            client = new FireSharp.FirebaseClient(config);
        }


        [HttpGet]
        public async Task<ActionResult<TarifeTanim>> Get()
        {
            FirebaseResponse response = await client.GetAsync("TarifeTanim");
            List<TarifeTanim> result = response.ResultAs<List<TarifeTanim>>();
            return Ok(result);
        }

        [HttpGet("TarifeFiyat")]
        public async Task<ActionResult<TarifeFiyat>> GetTarifeFiyat()
        {
            FirebaseResponse response = await client.GetAsync("TarifeFiyat");
            List<TarifeFiyat> result = response.ResultAs<List<TarifeFiyat>>();
            return Ok(result);
        }
    }
}
