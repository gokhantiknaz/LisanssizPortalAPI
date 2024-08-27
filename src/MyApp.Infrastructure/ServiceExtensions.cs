using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Core.Services;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;

namespace MyApp.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<LisanssizContext>(options =>
                options.UseNpgsql("Server=.;Port=5432;Database=Humanity;;Username=<Your_Username>;Password=1234qqqQ",
                x => x.MigrationsAssembly("MyApp.Infrastructure")));

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