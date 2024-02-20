using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Gizmo.Web.Reports.Components
{
    /// <summary>
    /// Optional base class to use with report components.
    /// </summary>
    /// <typeparam name="TModel">Report model type.</typeparam>
    public abstract class ReportComponentBase<TModel> : ComponentBase where TModel : class, new()
    {
        /// <summary>
        /// Gets component report model.
        /// </summary>
        [Parameter()]
        public TModel Model { get; set; } = null!;

        /// <summary>
        /// Gets export metadata.
        /// </summary>
        /// <remarks>
        /// Currently this parameter will only be set when rendering reports on server.
        /// </remarks>
        [Parameter()]
        public ReportExportMetadata? ExportMetadata { get; set; }

        /// <summary>
        /// Gets unmatched parameters values.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UnmatchedValues
        {
            get; set;
        } = [];

        protected override Task OnParametersSetAsync()
        {
            Model ??= new();

            return base.OnParametersSetAsync();
        }
    }
}
