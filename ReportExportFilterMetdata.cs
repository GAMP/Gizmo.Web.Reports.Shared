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
        /// Gets filter name.
        /// </summary>
        /// <remarks>
        /// This will be equal to licalized filter name or filter property name if localization attributes are not set.
        /// </remarks>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets filter values.
        /// </summary>
        /// <remarks>
        /// Only non-null values will be included, this might be revised in the future.
        /// </remarks>
        public IEnumerable<string> Values { get; init; } = Enumerable.Empty<string>();
    }
}
