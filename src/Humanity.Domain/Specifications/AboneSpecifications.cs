using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Specifications
{
    public static class AboneSpecifications
    {
        public static ISpecification<Abone> GetaboneById(int aboneId)
        {
            var spec = new BaseSpecification<Abone>(x => x.IsDeleted == false && x.Id==aboneId);
            return spec;
        }

        public static BaseSpecification<Abone> GetAllActiveUsersSpec()
        {
            var spec =  new BaseSpecification<Abone>(x => x.IsDeleted == false);
         
            return spec;
        }

        public static BaseSpecification<AboneUretici> GetUreticiByabone(int aboneId)
        {
            var specAbone = new BaseSpecification<AboneUretici>(x => x.Abone.SahisTip == Enums.Enums.SahisTip.Uretici && x.Abone.Id==aboneId);
            specAbone.AddInclude(a => a.Abone);

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
            return specAbone;
        }

        public static BaseSpecification<Abone> GetAboneById(int aboneid)
        {
            var specAbone = new BaseSpecification<Abone>(x => x.Id==aboneid);
            return specAbone;
        }

     
    }
}
