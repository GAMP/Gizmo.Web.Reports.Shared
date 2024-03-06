using System;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Identifies report modules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ReportModuleAttribute : Attribute
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="componentType">Report module visual component type.</param>
        /// <param name="guid">Report module unique Guid string.</param>
        public ReportModuleAttribute(string guid)
        {
            _guid = Guid.Parse(guid);
        }

        private readonly Guid _guid;

        /// <summary>
        /// Report unique guid.
        /// </summary>
        public Guid Guid => _guid;

        /// <summary>
        /// Indicates that report supports range filtering.
        /// </summary>
        /// <remarks>
        /// The default value is true.
        /// </remarks>
        public bool SupportsRangeFilter { get; set; } = true;

        /// <summary>
        /// Indicates that report is hidden from user.
        /// </summary>
        public bool IsHidden { get; set; } = false;
    }
}
