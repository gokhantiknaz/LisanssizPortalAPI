using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Specifications
{
    public static class MusteriSpecifications
    {
        public static ISpecification<Musteri> GetMusteriById(int musteriId)
        {
            var spec = new BaseSpecification<Musteri>(x => x.IsDeleted == false && x.Id==musteriId);
            return spec;
        }

        public static BaseSpecification<Musteri> GetAllActiveUsersSpec()
        {
            var spec =  new BaseSpecification<Musteri>(x => x.IsDeleted == false);
         
            return spec;
        }

        public static BaseSpecification<AboneUretici> GetUreticiByCari(int cariId)
        {
            var specAbone = new BaseSpecification<AboneUretici>(x => x.Abone.SahisTip == Enums.Enums.SahisTip.Uretici && x.Abone.Musteri.CariKartId==cariId);
            specAbone.AddInclude(a => a.Abone);
            specAbone.AddInclude(a => a.Abone.Musteri);

            return specAbone;
        }

        public static BaseSpecification<AboneUretici> GetUreticiByMusteri(int musterid)
        {
            var specAbone = new BaseSpecification<AboneUretici>(x => x.Abone.SahisTip == Enums.Enums.SahisTip.Uretici && x.Abone.Musteri.Id == musterid);
            specAbone.AddInclude(a => a.Abone);
            specAbone.AddInclude(a => a.Abone.Musteri);

            return specAbone;
        }


        public static BaseSpecification<AboneTuketici> GetTuketiciByUretici(int ureticiAboneId)
        {
            var specAbone = new BaseSpecification<AboneTuketici>(x =>  x.UreticiAboneId== ureticiAboneId && x.Abone.SahisTip!=Enums.Enums.SahisTip.Uretici);

            return specAbone;
        }

        public static BaseSpecification<AboneUretici> GetUreticiByAboneId(int aboneid)
        {
            var specAbone = new BaseSpecification<AboneUretici>(x => x.Abone.SahisTip == Enums.Enums.SahisTip.Uretici && x.Abone.Id == aboneid);
            specAbone.AddInclude(a => a.Abone);
            specAbone.AddInclude(a => a.Abone.Musteri);

            return specAbone;
        }

        public static BaseSpecification<Abone> GetAboneById(int aboneid)
        {
            var specAbone = new BaseSpecification<Abone>(x => x.Id==aboneid);
            return specAbone;
        }

     
    }
}
