using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using System.Reflection;
using Humanity.Domain.Entities;
using Humanity.Application.Models.DTOs.Musteri;

namespace Humanity.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IAboneService, AboneService>();

            services.AddScoped<IMusteriService<Musteri,MusteriDTO>, MusteriService<Musteri, MusteriDTO>>();
           
            //services.AddScoped(typeof(IMusteriService<>), typeof(MusteriService<>));
            
            services.AddScoped<IFirmaService, FirmaService>();
            services.AddScoped<IEndeksService, EndeksService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArilService, ArilService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}