using Gizmo.Web.Reports.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gizmo.Web.Reports
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds reports shared services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddReportsShared(this IServiceCollection services)
        {
            //the service must be added as scoped since it depends on NavigationManager which is scoped
            //not adding as scoped will lead to errors in WPF host
            services.TryAddScoped<ReportLinkGenerationService>();

            //add host environment service
            services.TryAddSingleton<HostEnvironmentService>();

            //add range generation service
            services.TryAddSingleton<ReportRangeService>();
            return services;
        }
    }
}
