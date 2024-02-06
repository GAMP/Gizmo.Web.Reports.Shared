namespace Gizmo.Web.Reports
{

    /// <inheritdoc cref="IReportResult"/>
    /// <typeparam name="TReportModel">Report model type.</typeparam>
    /// <typeparam name="TReportAdditionalModel">Additional report model type.</typeparam>
    public sealed class ReportResult<TReportModel, TReportAdditionalModel> : IReportResult
       where TReportModel : class
       where TReportAdditionalModel : class
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="reportModel">Report model.</param>
        /// <param name="reportModelAdditional">Additional report model</param>
        public ReportResult(TReportModel reportModel, TReportAdditionalModel? reportModelAdditional = null)
        {
            _reportModelAdditional = reportModelAdditional;
            _reportModel = reportModel;
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="reportModel">Report model.</param>
        public ReportResult(TReportModel report) : this(report, default)
        {
        }

        private readonly TReportModel _reportModel;
        private readonly TReportAdditionalModel? _reportModelAdditional;

        /// <inheritdoc cref="IReportResult.ReportModel"/>
        public TReportAdditionalModel? ReportModelAdditional => _reportModelAdditional;

        /// <inheritdoc cref="IReportResult.ReportModelAdditional"/>
        public TReportModel ReportModel => _reportModel;

        object IReportResult.ReportModel => _reportModel;

        object? IReportResult.ReportModelAdditional => _reportModelAdditional;
    }
}
