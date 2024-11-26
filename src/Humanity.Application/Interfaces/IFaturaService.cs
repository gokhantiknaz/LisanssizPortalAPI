using Humanity.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IFaturaService
    {

        Task<List<FaturaDTO>> AboneAylikFaturaHesapla(int aboneid, string donem);
    }
}
