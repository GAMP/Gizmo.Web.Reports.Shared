using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Gizmo.Web.Reports.Components
{
    /// <summary>
    /// Optional base class to use with report components.
    /// </summary>
    /// <typeparam name="TModel">Report model type.</typeparam>
    public abstract class ReportComponentBase<TModel> : ComponentBase
    {
        /// <summary>
        /// Gets component report model.
        /// </summary>
        [Parameter()]
        public TModel Model { get; set; } = default!;

        /// <summary>
        /// Gets unmatched parameters values.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UnmatchedValues
        {
            get; set;
        } = [];
    }
}
