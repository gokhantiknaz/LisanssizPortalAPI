using Microsoft.Extensions.DependencyInjection;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;

namespace Humanity.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}