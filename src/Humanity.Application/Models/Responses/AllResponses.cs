using Humanity.Application.Models.DTOs.ListDTOS;
using Humanity.Application.Models.DTOs.Musteri;

namespace Humanity.Application.Models.Responses
{
    public class CreateMusteriRes
    {
        public MusteriDTO Data { get; set; }
    }

    public class CreateCariKartRes
    {
        public CariKartDTO Data { get; set; }
    }

    public class GetTuketiciListRes
    {
        public IList<TuketiciTableDTO> Data { get; set; }
    }
}
