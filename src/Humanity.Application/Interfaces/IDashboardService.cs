using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public  interface IDashboardService
    {
        Task<List<AboneAylikTuketim>> AboneAktifAylikTuketimGetir();

        Task<IEnumerable<YillikUretimTuketim>> AboneUretimTuketimKarsilastirma();

        Task<IEnumerable<AylikUretimTuketim>> AylikUretimTuketimKarsilastirma();

        Task<IEnumerable<AylikBazdaTumAbonelerTuketimSummary>> YillikToplamTuketimGetir();

        Task<IEnumerable<AylikBazdaTumAbonelerTuketimSummary>> YillikToplamUretimmGetir();

        Task<List<AylikEnYuksekEnDusukTuketimGunveMiktar>> AylikEnYuksekEnDusukTuketimGunveMiktar();

        Task<List<DailyProductionConsumption>> GunlukUretimTuketimGetir();
    }
}
