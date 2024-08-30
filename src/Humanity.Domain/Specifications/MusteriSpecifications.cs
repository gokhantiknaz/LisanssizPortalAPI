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
        public static BaseSpecification<Musteri> GetUserByEmailAndPasswordSpec(int tckn)
        {
            return new BaseSpecification<Musteri>(x => x.Tckn == tckn && x.IsDeleted == false);
        }

        public static BaseSpecification<Musteri> GetAllActiveUsersSpec()
        {
            var spec =  new BaseSpecification<Musteri>(x => x.IsDeleted == false);
         
            return spec;
        }
    }
}
