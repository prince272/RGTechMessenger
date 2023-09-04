using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RGTechMessenger.Core.Extensions.EmailSender;
using RGTechMessenger.Core.Extensions.FileStorage;
using RGTechMessenger.Core.Extensions.SmsSender;
using RGTechMessenger.Infrastructure.EmailSender.MailKit;
using RGTechMessenger.Infrastructure.SmsSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Infrastructure.FileStorage.Local
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalStorage(this IServiceCollection services, Action<LocalFileStorageOptions> options)
        {
            services.Configure(options);
            services.AddLocalStorage();
            return services;
        }

        public static IServiceCollection AddLocalStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LocalFileStorageOptions>(configuration);
            services.AddLocalStorage();
            return services;
        }

        public static IServiceCollection AddLocalStorage(this IServiceCollection services)
        {
            services.AddTransient<IFileStorage, LocalFileStorage>();
            return services;
        }
    }
}
