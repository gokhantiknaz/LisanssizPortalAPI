using Microsoft.EntityFrameworkCore;
using Humanity.Domain.Entities;

namespace Humanity.Infrastructure.Data
{
    public class LisanssizContext : DbContext
    {
        public LisanssizContext(DbContextOptions<LisanssizContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
