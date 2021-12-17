using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
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
        }

        public static T GetService<T>() where T : notnull
        {
            return serviceProvider.GetRequiredService<T>();
        }

        public static void RegiserMainWindow<T>() where T : Window
        {
            ConfigureServices(Services);
            Services.AddSingleton<T>();
            serviceProvider = Services.BuildServiceProvider();
        }

        private static IServiceCollection Services { get; set; }

        private static IServiceProvider serviceProvider = null!;

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IOsuLocator, RegistryOsuLocator>();
            services.AddSingleton<OsuDbReader>();
            services.AddSingleton<Config>();
            services.AddSingleton<OsuDbService>();
            services.AddSingleton<DependencyEnsureService>();
            services.AddSingleton<DownloadService>();
            services.AddSingleton<RenderService>();
            services.AddSingleton<RenderOptionService>();

            services.AddTransient<ReplayViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<RecordDetailViewModel>();
            services.AddSingleton<RenderViewModel>();
        }
    }
}
