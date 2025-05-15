using System;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Report sub type filed attribute.
    /// </summary>
    /// <remarks>
    /// Used to identify report sub type fields.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ReportSubTypeFieldAttribute : Attribute
    {
    }
}
