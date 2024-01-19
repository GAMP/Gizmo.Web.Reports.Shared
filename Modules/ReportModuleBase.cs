using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Base report module implementation.
    /// </summary>
    public abstract class ReportModuleBase<TReportFilter, TReportComponentType> : IReportModule where TReportFilter : IReportFilter, new() where TReportComponentType : Microsoft.AspNetCore.Components.IComponent
    {
        private readonly Type _reportComponentType = typeof(TReportComponentType);
        protected TReportFilter _filterModel = new();

        public IReportFilter Filter => _filterModel;

        public Type ReportComponentType => _reportComponentType;

        /// <summary>
        /// Gets concrete filter.
        /// </summary>
        /// <remarks>
        /// This model can be used to construct the desired report filter in <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/> method.<br></br>
        /// All the filter parameters will be set and validated before calling <see cref="GenerateReportAsync(ReportRangeFilter, CancellationToken)"/>.
        /// </remarks>
        protected TReportFilter FilterConcrete => _filterModel;

        public abstract Task<ReportResult> GenerateReportAsync(ReportRangeFilter reportRangeFilter, CancellationToken cancellationToken = default);
    }
}
