using System;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Implemented by <see cref="TimeProvider"/> instances that notify subscribers when the active time zone changes.
    /// </summary>
    public interface ITimeZoneChangedNotifier
    {
        event EventHandler TimeZoneChanged;
    }
}
