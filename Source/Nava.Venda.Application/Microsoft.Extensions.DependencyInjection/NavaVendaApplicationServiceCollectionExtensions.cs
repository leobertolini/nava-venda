using Nava.Venda.Application;
using Nava.Venda.Domain;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NavaVendaApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IVendaService, VendaService>();

            return services;
        }
    }
}
