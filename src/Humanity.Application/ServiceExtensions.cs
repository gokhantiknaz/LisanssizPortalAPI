using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;

namespace Humanity.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
    
            services.AddScoped<IMusteriService, MusteriService>();
            services.AddScoped<ICariKartService, CariKartService>();
            services.AddScoped<IFirmaService, FirmaService>();
        }
    }
}