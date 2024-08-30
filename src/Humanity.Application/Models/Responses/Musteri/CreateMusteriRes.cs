using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Requests.Musteri
{
    public class CreateMusteriRes
    {
        public MusteriDTO Data { get; set; }
    }

    public class CreateCariKartRes
    {
        public CariKartDTO Data { get; set; }
    }
}
