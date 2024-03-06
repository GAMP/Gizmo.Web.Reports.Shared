using System;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// This attribute must be used on any property present in the filter model that will be used for filtering.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ReportFilterFieldAttribute : Attribute
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public ReportFilterFieldAttribute()
        {
            Field = ReportFilterFields.Custom;
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="filterField">Field type.</param>
        public ReportFilterFieldAttribute(ReportFilterFields filterField)
        {
            Field = filterField;
        }

        /// <summary>
        /// Specifies filter field type.
        /// </summary>
        /// <remarks>
        /// This parameter allows us to set the filter field type to an know value such as user id and similar.<br></br>
        /// Default value is equal to <see cref="ReportFilterFields.Custom"/>
        /// </remarks>
        public ReportFilterFields Field { get; init; } = ReportFilterFields.Custom;

        /// <summary>
        /// Specifies if filter is hidden from user.
        /// </summary>
        /// <remarks>
        /// This can be used to hide any report filter parameters that should not be visible to the user.
        /// </remarks>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// Specifies if filter can be set by the user.
        /// </summary>
        /// <remarks>
        /// This can be used if we want to display current filter value but not allow user to set it.
        /// </remarks>
        public bool IsUserSettable { get; set; } = true;
    }
}
