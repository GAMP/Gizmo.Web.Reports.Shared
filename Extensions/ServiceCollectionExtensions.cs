using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Gizmo.Web.Reports
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds server reports.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddServerReports(this IServiceCollection services)
        {
            Assembly.Load("Gizmo.Web.Reports");

            //use all currently loaded assemblies
            //at this stage any external assemblies should be already loaded into app domain
            var assembiles = AppDomain.CurrentDomain.GetAssemblies();

            //get all types representing IReport module and have report attribute applied
            var reportModuleTypes = assembiles.SelectMany(assembly => assembly.GetTypes()
            .Where(type => type.GetCustomAttributes<ReportModuleAttribute>().Any())
            .Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IReportModule)))
            .ToList());

            foreach (var reportType in reportModuleTypes)
            {
                //get and check report attribute
                var reportAttribute = reportType.GetCustomAttribute<ReportModuleAttribute>();
                if (reportAttribute == null)
                    continue;

                services.AddTransient(reportType);
                services.AddTransient(sp => (IReportModule)sp.GetRequiredService(reportType));
                services.AddKeyedTransient(reportAttribute.Guid.ToString(), (sp, key) => (IReportModule)sp.GetRequiredService(reportType));
            }

            return services;
        }
    }
}
