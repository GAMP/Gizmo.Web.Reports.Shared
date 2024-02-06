namespace Gizmo.Web.Reports
{
    public interface IReportResult
    {
        /// <summary>
        /// Gets report model.
        /// </summary>
        object ReportModel { get; }

        /// <summary>
        /// Gets additional report model.
        /// </summary>
        object? ReportModelAdditional { get; }
    }
}
