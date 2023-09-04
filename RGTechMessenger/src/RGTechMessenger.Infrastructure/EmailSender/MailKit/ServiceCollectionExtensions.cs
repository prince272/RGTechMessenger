using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RGTechMessenger.Core.Utilities;
using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RGTechMessenger.Core.Extensions.EmailSender;
using Microsoft.Extensions.Configuration;

namespace RGTechMessenger.Infrastructure.EmailSender.MailKit
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMailKitEmailSender(this IServiceCollection services, Action<MailKitEmailOptions> options)
        {
            services.Configure(options);
            services.AddMailKitEmailSender();
            return services;
        }

        public static IServiceCollection AddMailKitEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailKitEmailOptions>(configuration);
            services.AddMailKitEmailSender();
            return services;
        }

        public static IServiceCollection AddMailKitEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            return services;
        }
    }
}
