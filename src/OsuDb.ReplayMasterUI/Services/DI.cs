using Microsoft.Extensions.DependencyInjection;
using OsuDb.Core;
using OsuDb.ReplayMasterUI.ViewModels;
using System;

namespace OsuDb.ReplayMasterUI.Services
{
    internal static class DI
    {
        static DI()
        {
            Services = new ServiceCollection();
            ConfigureServices(Services);
            serviceProvider = Services.BuildServiceProvider();
        }

        public static T GetService<T>() where T : notnull
        {
            return serviceProvider.GetRequiredService<T>();
        }

        private static IServiceCollection Services { get; set; }

        private static readonly IServiceProvider serviceProvider;

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IOsuLocator, RegistryOsuLocator>();
            services.AddSingleton<OsuDbReader>();
            services.AddSingleton<Config>();
            services.AddSingleton<OsuDbService>();

            services.AddTransient<ReplayViewModel>();
            
        }
    }
}
