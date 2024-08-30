using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses.Musteri
{
    public class GetAllActiveMusteriRes
    {
        public IList<MusteriDTO> Data { get; set; }
    }

    public class GetAllActiveCariKartRes
    {
        public IList<CariKartDTO> Data { get; set; }
    }
}
