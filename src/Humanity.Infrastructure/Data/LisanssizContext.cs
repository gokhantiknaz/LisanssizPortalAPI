using Microsoft.EntityFrameworkCore;
using Humanity.Domain.Entities;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Humanity.Infrastructure.Data
{
    public class LisanssizContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public LisanssizContext(DbContextOptions<LisanssizContext> options) : base(options)
        { }

        protected LisanssizContext()
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<AboneIletisim>().HasKey(table => new
            {
                table.AboneId,
                table.IletisimId
            });

            builder.Entity<MusteriIletisim>().HasKey(table => new
            {
                table.MusteriId,
                table.IletisimId
            });

            builder.Entity<CariIletisim>().HasKey(table => new
            {
                table.CariKartId,
                table.IletisimId
            });


            builder.Entity<FirmaIletisim>().HasKey(table => new
            {
                table.FirmaId,
                table.IletisimId
            });

        }

        public DbSet<CariKart> CariKart { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Iletisim> Iletisim { get; set; }

        public DbSet<Abone> Abone { get; set; }

        public DbSet<AboneIletisim> AboneIletisim { get; set; }

        public DbSet<CariIletisim> CariIletisim { get; set; }

        public DbSet<AboneSayac> AboneSayac { get; set; }

        public DbSet<MusteriIletisim> MusteriIletisim { get; set; }

        public DbSet<MusteriOzelKod> MusteriOzelKod { get; set; }


        public DbSet<AboneUretici> AboneUretici { get; set; }

        public DbSet<AboneTuketici> AboneTuketici { get; set; }


        public DbSet<Firma> Firma{ get; set; }

        public DbSet<FirmaIletisim> FirmaIletisim { get; set; }


        public DbSet<FirmaEntegrasyon> FirmaEntegrasyon { get; set; }

        public DbSet<Logs> Logs { get; set; }

    }
}
