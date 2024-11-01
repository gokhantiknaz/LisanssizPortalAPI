using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.DTOs;

namespace Humanity.Application.Interfaces
{
    public interface IMusteriService<T, Dto> where Dto : class
    {
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllMusteri();
        Task<CustomResponseDto<Dto>> GetMusteriById(int id);
        Task<CustomResponseDto<Dto>> CreateMusteri(MusteriDTO musteri);
        Task<CustomResponseDto<Dto>> Update(MusteriDTO req);

        //Task<GetMusteriRes> GetMusteriById(int id);

        //Task<GetAllActiveMusteriRes> GetAllMusteri();
        //Task<GetAllActiveMusteriRes> MusteriyeBagliUreticiGetir(int musteriId);
        //Task<GetTuketiciListRes> MusteriyeBagliTuketicileriGetir(int aboneureticiId);
        //Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId);
        //Task<CreateMusteriRes> Update(UpdateMusteriReq musteri);
        //Task<MusteriEntegrasyonDTO> GetMusteriEntegrasyon(int musteriId);
        //Task<bool> ArilBagliTuketiciKaydet(int musteriid);
        //Task<bool> KaydedilenAboneEndeksleriAl(int musteriID);
    }
}
