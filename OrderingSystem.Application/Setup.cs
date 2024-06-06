using Microsoft.Extensions.DependencyInjection;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Application.Services;

namespace OrderingSystem.Application
{
    public static class Setup
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
