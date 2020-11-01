using Microsoft.Extensions.DependencyInjection;
using Nava.Venda.Domain;
using System;

namespace Nava.Venda.SqlAdapter.Microsoft.Extensions.DependencyInjection
{
    public static class NavaVendaSqlAdapterServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlAdapter(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IVendaSqlAdapter, VendaSqlAdapter>();

            return services;
        }
    }
}
