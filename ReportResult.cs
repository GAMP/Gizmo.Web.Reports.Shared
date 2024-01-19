using System.Collections.Generic;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Report generation result.
    /// </summary>
    public sealed class ReportResult(object report)
    {
        private readonly object _report = report;

        /// <summary>
        /// Gets additional parameters.
        /// </summary>
        /// <remarks>
        /// Any additional report parameters can be provided here.<br></br>
        /// The will be passed to the visual component through the parameter attribute <a href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#component-parameters"></a>.
        /// </remarks>
        public IDictionary<string, object> AdditionalParameters { get; init; } = new Dictionary<string,object>();

        /// <summary>
        /// Gets report.
        /// </summary>
        public object Report => _report;
    }
}
