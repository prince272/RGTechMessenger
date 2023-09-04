using Microsoft.Extensions.DependencyInjection;
using RGTechMessenger.Core.Extensions.SmsSender;
using RGTechMessenger.Infrastructure.EmailSender.MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Infrastructure.SmsSender.Fake
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeSmsSender(this IServiceCollection services)
        {
            services.AddTransient<ISmsSender, FakeSmsSender>();
            return services;
        }
    }
}
