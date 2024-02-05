using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Base report module implementation.
    /// </summary>
    public abstract class ReportModuleBase<TReportFilter, TReportComponentType> : IReportModule 
        where TReportFilter : IReportFilter, new() 
        where TReportComponentType : Microsoft.AspNetCore.Components.IComponent
    {
        public ReportModuleBase(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly Type _reportComponentType = typeof(TReportComponentType);
        protected TReportFilter _filterModel = new();
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        IReportFilter IReportModule.Filter => _filterModel;

        public Type ReportComponentType => _reportComponentType;

        protected IServiceProvider ServiceProvider => _serviceProvider;

        protected ILogger Logger => _logger;

        /// <summary>
        /// Gets concrete filter.
        /// </summary>
        /// <remarks>
        /// This model can be used to construct the desired report filter in <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/> method.<br></br>
        /// All the filter parameters will be set and validated before calling <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/>.
        /// </remarks>
        protected TReportFilter Filter => _filterModel;

        public abstract Task<ReportResult> GenerateReportAsync(ReportRangeFilter reportRangeFilter, CancellationToken cancellationToken = default);
    }
}
