using System;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Report range filter.
    /// </summary>
    public sealed class ReportRangeFilter
    {
        /// <summary>
        /// Represents an non range filter.
        /// </summary>
        /// <remarks>
        /// This instance will be passed to <see cref="IReportModule.GenerateReportAsync(ReportRangeFilter, System.Threading.CancellationToken)"/> when the report has indicated
        /// that it does not support range filters by <see cref="ReportModuleAttribute.SupportsRangeFilter"/> option.
        /// </remarks>
        public static readonly ReportRangeFilter None = new();

        /// <summary>
        /// Gets selected range preset used to generate the start and end ranges.
        /// </summary>
        public ReportRangeType RangeType { get; init; }

        /// <summary>
        /// Report range start.
        /// </summary>
        public DateTime? Start { get; init; }

        /// <summary>
        /// Report range end.
        /// </summary>
        public DateTime? End { get; init; }
    }
}
