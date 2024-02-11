using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Base report module implementation.
    /// </summary>
    public abstract class ReportModuleBase<TReportFilter, TReportModel, TReportModelAdditional, TReportComponentType> : IReportModule
        where TReportFilter : IReportFilter, new()
        where TReportComponentType : Microsoft.AspNetCore.Components.IComponent
        where TReportModel : class
        where TReportModelAdditional : class
    {
        public ReportModuleBase(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly Type _reportComponentType = typeof(TReportComponentType);
        private readonly Type _reportModelType = typeof(TReportModel);
        private readonly Type _reportModelAdditionalType = typeof(TReportModelAdditional);
        protected TReportFilter _filterModel = new();
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        IReportFilter IReportModule.Filter => _filterModel;

        public Type ReportComponentType => _reportComponentType;

        public Type ReportModelType => _reportModelType;

        public Type ReportModelAddtionalType => _reportModelAdditionalType;

        /// <summary>
        /// Gets service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider => _serviceProvider;

        /// <summary>
        /// Gets logger.
        /// </summary>
        protected ILogger Logger => _logger;

        /// <summary>
        /// Gets concrete filter.
        /// </summary>
        /// <remarks>
        /// This model can be used to construct the desired report filter in <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/> method.<br></br>
        /// All the filter parameters will be set and validated before calling <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/>.
        /// </remarks>
        protected TReportFilter Filter => _filterModel;

        /// <inheritdoc cref="IReportModule.GenerateReportAsync(ReportRangeFilter, CancellationToken)"/>
        public abstract Task<ReportResult<TReportModel, TReportModelAdditional>> GenerateReportAsync(ReportRangeFilter reportRangeFilter, CancellationToken cancellationToken = default);

        async Task<IReportResult> IReportModule.GenerateReportAsync(ReportRangeFilter reportRangeFilter, CancellationToken cancellationToken) => await GenerateReportAsync(reportRangeFilter, cancellationToken);

        /// <summary>
        /// Creates result.
        /// </summary>
        /// <param name="model">Report model.</param>
        /// <param name="reportAdditionalModel">Additional report model.</param>
        /// <returns>Report result model.</returns>
        protected ReportResult<TReportModel, TReportModelAdditional> CreateResult(TReportModel model, TReportModelAdditional? reportAdditionalModel = null)
        {
            return new ReportResult<TReportModel, TReportModelAdditional>(model, reportAdditionalModel);
        }
    }
}
