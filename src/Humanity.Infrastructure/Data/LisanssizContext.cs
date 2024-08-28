using Microsoft.EntityFrameworkCore;
using Humanity.Domain.Entities;

namespace Humanity.Infrastructure.Data
{
    public class LisanssizContext : DbContext
    {
        public LisanssizContext(DbContextOptions<LisanssizContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AboneIletisim>().HasKey(table => new {
                table.AboneId,
                table.IletisimId
            });


            builder.Entity<MusteriIletisim>().HasKey(table => new {
                table.MusteriId,
                table.IletisimId
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Iletisim> letisim { get; set; }
        public DbSet<Musteri> Musteri { get; set; }

        public DbSet<Abone> Abone{ get; set; }

        public DbSet<AboneIletisim> AboneIletisim { get; set; }

        public DbSet<AboneSayac> AboneSayac { get; set; }

        public DbSet<MusteriIletisim> MusteriIletisim { get; set; }

        public DbSet<MusteriOzelKod> MusteriOzelKod { get; set; }

        public DbSet<UreticiLisans> UreticiLisans { get; set; }

    }
}
