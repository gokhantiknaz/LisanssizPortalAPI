using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using System.Reflection;

namespace Humanity.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IAboneService, AboneService>();
            services.AddScoped<IMusteriService, MusteriService>();
            services.AddScoped<IFirmaService, FirmaService>();
            services.AddScoped<IEndeksService, EndeksService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArilService, ArilService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}