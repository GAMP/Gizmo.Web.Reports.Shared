using System.Collections.Generic;
using System.Linq;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Report export filter metdata.
    /// </summary>
    public sealed class ReportExportFilterMetdata
    {
        /// <summary>
        /// Gets filter values.
        /// </summary>
        public IEnumerable<string> Values { get; init; } = Enumerable.Empty<string>();
    }
}
