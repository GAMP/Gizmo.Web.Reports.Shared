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
        [Inject()]
        protected HostEnvironmentService HostEnvironment
        {
            get; set;
        } = null!;

        /// <summary>
        /// Gets component report model.
        /// </summary>
        [Parameter()]
        public TModel Model { get; set; } = null!;

        /// <summary>
        /// Gets export metadata.
        /// </summary>
        [Parameter()]
        public ReportExportMetadata ExportMetadata { get; set; } = null!;

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
