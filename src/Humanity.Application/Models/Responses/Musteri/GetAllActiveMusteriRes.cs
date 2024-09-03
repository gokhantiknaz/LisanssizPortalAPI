using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.ListDTOS;
using Humanity.Application.Models.DTOs.Musteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses.Musteri
{

    public class GetAllActiveMusteriTableRes
    {
        public IList<MusteriTableDTO> Data { get; set; }
    }
    public class GetAllActiveMusteriRes
    {
        public IList<MusteriDTO> Data { get; set; }
    }



    public class GetMusteriRes
    {
        public MusteriDTO Data { get; set; }
    }

    public class GetAllActiveCariKartRes
    {
        public IList<CariTableDTO> Data { get; set; }
    }

    public class GetCariRes
    {
        public CariKartDTO Data { get; set; }
    }
}
