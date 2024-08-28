using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Core.Services;
using Humanity.Domain.Core.Repositories;
using Humanity.Infrastructure.Data;
using Humanity.Infrastructure.Repositories;
using Humanity.Infrastructure.Services;

namespace Humanity.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<LisanssizContext>(options =>
                options.UseNpgsql("Server=localhost;Port=5432;Database=Humanity;;Username=postgres;Password=1234qqqQ",
                x => x.MigrationsAssembly("Humanity.Infrastructure")));

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<LisanssizContext>>();

            using (var dbContext = new LisanssizContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}