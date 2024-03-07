using System.Collections.Generic;

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
        public string DisplayName { get; init; } = string.Empty;

        /// <summary>
        /// Gets filter property name.
        /// </summary>
        public string FilterName { get; init; } = string.Empty;

        /// <summary>
        /// Gets filter values.
        /// </summary>
        /// <remarks>
        /// The Key will represents the selected value (id,guid etc), the value represents display value.
        /// </remarks>
        public IEnumerable<KeyValuePair<string,string>> Values { get; init; } = [];
    }
}
