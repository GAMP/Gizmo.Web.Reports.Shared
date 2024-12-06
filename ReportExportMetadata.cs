using System;
using System.Collections.Generic;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Report export metadata.
    /// </summary>
    public sealed class ReportExportMetadata
    {
        /// <summary>
        /// Gets exporting user username.
        /// </summary>
        /// <remarks>
        /// This value will be null when export is done by the system.
        /// </remarks>
        public string? ExporterUserName
        {
            get; init;
        }

        /// <summary>
        /// Gets exporting user first name.
        /// </summary>
        /// <remarks>
        /// This value will be null when export is done by the system or not set for the user.
        /// </remarks>
        public string? ExporterFirstName
        {
            get; init;
        }

        /// <summary>
        /// Gets exporting user last name.
        /// </summary>
        /// <remarks>
        /// This value will be null when export is done by the system or not set for the user.
        /// </remarks>
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
        /// Gets report range type.
        /// </summary>
        public ReportRangeType ReportRangeType
        {
            get;set;
        }

        /// <summary>
        /// Gets optional preset range name.
        /// </summary>
        public string? ReportRangeTypeName
        {
            get; init;
        }

        /// <summary>
        /// Gets optional report start date.
        /// </summary>
        public DateTime? DateFrom
        {
            get;
            init;
        }

        /// <summary>
        /// Gets optional report end date.
        /// </summary>
        public DateTime? DateTo
        {
            get; init;
        }

        /// <summary>
        /// Gets export filters.
        /// </summary>
        public IEnumerable<ReportExportFilterMetadata> Filters {  get; init; } = [];

        /// <summary>
        /// Gets export date time.
        /// </summary>
        public DateTime ExportTime
        {
            get; init;
        }
    }
}
