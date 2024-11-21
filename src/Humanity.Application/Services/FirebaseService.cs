using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Humanity.Application.Interfaces;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Services
{
    public class FirebaseService : IFirebaseService
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "https://humanity-9e850-default-rtdb.firebaseio.com/",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = "DZ9Sat4RWq3OwrCHeF0hFpUpR3AVYFVS33KQzKoR"
        };

        // Firebase client
        IFirebaseClient client;
        public FirebaseService()
        {
            client = new FireSharp.FirebaseClient(config);
        }


        public async Task<IEnumerable<DagitimFirma>> GetDagitimFirmalar()
        {
            FirebaseResponse response = await client.GetAsync("DagitimFirma");
            List<DagitimFirma> result = response.ResultAs<List<DagitimFirma>>();

            return result;
        }


        public async Task<IEnumerable<TarifeTanim>> GetTarifeTanim()
        {
            FirebaseResponse response = await client.GetAsync("TarifeTanim");
            List<TarifeTanim> result = response.ResultAs<List<TarifeTanim>>();
            return result;
        }


        public async Task<IEnumerable<TarifeFiyat>> GetTarifeFiyat()
        {
            FirebaseResponse response = await client.GetAsync("TarifeFiyat");
            List<TarifeFiyat> result = response.ResultAs<List<TarifeFiyat>>();

            return result;
        }

        public async Task<TarifeFiyat> GetTarifeFiyatById(int tarife)
        {
            FirebaseResponse response = await client.GetAsync($"TarifeFiyat/{tarife}");
            TarifeFiyat result = response.ResultAs<TarifeFiyat>();

            return result;
        }
    }
}
