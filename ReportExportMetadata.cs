using System;
using System.Collections.Generic;
using System.Linq;

namespace Gizmo.Web.Reports.Shared
{
    public sealed class ReportExportMetadata
    {
        public ReportExportMetadata() { }

        /// <summary>
        /// Gets exporting user username.
        /// </summary>
        public string ExporterUserName
        {
            get; init;
        } = string.Empty;

        /// <summary>
        /// Gets exporting user first name.
        /// </summary>
        public string? ExporterFirstName
        {
            get; init;
        }

        /// <summary>
        /// Gets exporting user last name.
        /// </summary>
        public string? ExporterLastName
        {
            get; init;
        }

        /// <summary>
        /// Gets report name.
        /// </summary>
        public string ReportName
        {
            get;
            init;
        } = string.Empty;

        /// <summary>
        /// Gets optional report preset name.
        /// </summary>
        public string? PresetName
        {
            get; init;
        }

        /// <summary>
        /// Gets export date time.
        /// </summary>
        public DateTime ExportTime
        {
            get; init;
        }

        /// <summary>
        /// Gets report start date.
        /// </summary>
        public DateTime DateFrom
        {
            get;
            init;
        }

        /// <summary>
        /// Gets report end date.
        /// </summary>
        public DateTime DateTo
        {
            get; init;
        }

        /// <summary>
        /// Gets export filters.
        /// </summary>
        public IEnumerable<ReportExportFilterMetdata> Filters {  get; init; } = Enumerable.Empty<ReportExportFilterMetdata>();
    }
}
