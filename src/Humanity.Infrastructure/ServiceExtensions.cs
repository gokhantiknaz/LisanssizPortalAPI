using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Core.Services;
using Humanity.Domain.Core.Repositories;
using Humanity.Infrastructure.Data;
using Humanity.Infrastructure.Repositories;
using Humanity.Infrastructure.Services;
using Humanity.Application.Repositories;
using Humanity.Infrastructure.Repositories.MusteriRepos;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using Microsoft.AspNetCore.Identity;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Humanity.Domain.Core.Models;

namespace Humanity.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<LisanssizContext>(options =>
            //    options.UseNpgsql("Server=ep-twilight-snow-a871iu69.eastus2.azure.neon.tech;Port=5432;Database=lisanssiz;;Username=lisanssiz_owner;Password=ohjSvY1mtDV6",
            //    x => x.MigrationsAssembly("Humanity.Infrastructure")));

            var jwtSettings = Configure<JwtSettings>(nameof(JwtSettings));

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            })
.AddEntityFrameworkStores<LisanssizContext>()
.AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero
    };
});
            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAboneRepository, AboneRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IHttpService, HttpUserService>();

            T Configure<T>(string sectionName) where T : class
            {
                var section = configuration.GetSection(sectionName);
                var settings = section.Get<T>();
                services.Configure<T>(section);

                return settings;
            }
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