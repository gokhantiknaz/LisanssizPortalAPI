using Humanity.Application.Models.DTOs.ListDTOS;
using Humanity.Application.Models.DTOs.Musteri;

namespace Humanity.Application.Models.Responses
{
    public class CreateMusteriRes
    {
        public MusteriDTO Data { get; set; }
    }

    
    public class GetTuketiciListRes
    {
        public IList<TuketiciTableDTO> Data { get; set; }
    }

    public class FirmaRes
    {
      
    }

    public class GetFirmaRes
    {
        public FirmaDTO Data { get; set; }
    }

    public class CreateAboneRes
    {
        public AboneDTO Data { get; set; }
    }

    public class GetAboneRes
    {
        public AboneDTO Data { get; set; }
    }

    public class GetAboneResList
    {
        public List<AboneDTO> Data { get; set; }
    }

    


}
