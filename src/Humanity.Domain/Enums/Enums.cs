using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Enums
{
    public class Enums
    {
        public enum SahisTip
        {
            None = 0,
            Uretici = 1,
            IcTUketici,
            DisTuketici
        }

        public enum UretimSekli
        {
            Çatı = 1,
            Ges = 2
        }

        public enum LisansBilgisi
        {
            Lisanslı = 1,
            Lisanssız = 2
        }

        public enum MahsupTipi
        {
            Saatlik = 1,
            Aylık
        }

        public enum GercekTuzel
        {
            Gerçek = 1,
            Tuzel
        }
    }
}
