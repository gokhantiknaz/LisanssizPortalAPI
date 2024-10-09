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
    
            services.AddScoped<IMusteriService, MusteriService>();
            services.AddScoped<ICariKartService, CariKartService>();
            services.AddScoped<IFirmaService, FirmaService>();
            services.AddScoped<IEndeksService, EndeksService>();

            services.AddScoped<IArilService, ArilService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}