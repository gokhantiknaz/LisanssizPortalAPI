using Microsoft.EntityFrameworkCore;
using Humanity.Domain.Entities;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Humanity.Application.Models.Responses.Dashboard;

namespace Humanity.Infrastructure.Data
{
    public class LisanssizContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
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

            builder.Entity<FirmaIletisim>().HasKey(table => new
            {
                table.FirmaId,
                table.IletisimId
            });

            builder.Entity<User>(user =>
            {
                user.Property(u => u.FirstName).HasMaxLength(256).IsRequired();
                user.Property(u => u.LastName).HasMaxLength(256);
            });

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<AboneAylikTuketim>().HasNoKey();
            builder.Entity<AylikBazdaTumAbonelerTuketimSummary>().HasNoKey();

        }


        public DbSet<AylikBazdaTumAbonelerTuketimSummary> AylikUTuketimSummaryretimTuketim { get; set; }

        public DbSet<AboneAylikTuketim> AboneAylikTuketim { get; set; }

        public DbSet<Musteri> Musteri { get; set; }

        public DbSet<Iletisim> Iletisim { get; set; }

        public DbSet<Abone> Abone { get; set; }

        public DbSet<AboneIletisim> AboneIletisim { get; set; }


        public DbSet<AboneSayac> AboneSayac { get; set; }

        public DbSet<MusteriIletisim> MusteriIletisim { get; set; }

        public DbSet<AboneOzelKod> MusteriOzelKod { get; set; }


        public DbSet<AboneUretici> AboneUretici { get; set; }

        public DbSet<AboneTuketici> AboneTuketici { get; set; }


        public DbSet<Firma> Firma { get; set; }

        public DbSet<FirmaIletisim> FirmaIletisim { get; set; }


        public DbSet<MusteriEntegrasyon> MusteriEntegrasyon { get; set; }

        public DbSet<Logs> Logs { get; set; }

        public DbSet<AboneSaatlikEndeks> AboneSaatlikEndeks { get; set; }

        public DbSet<OwnerConsumpiton> OwnerConsumpiton { get; set; }

        public DbSet<AboneEndeks> AboneEndeks { get; set; }
        public DbSet<AboneEndeksPeriod> AboneEndeksPeriod { get; set; }

    }
}
