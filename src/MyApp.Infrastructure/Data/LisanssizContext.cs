using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Data
{
    public class LisanssizContext : DbContext
    {
        public LisanssizContext(DbContextOptions<LisanssizContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
