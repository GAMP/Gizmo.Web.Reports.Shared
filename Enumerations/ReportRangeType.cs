namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Represent report ranges.
    /// </summary>
    public enum ReportRangeType
    {
        /// <summary>
        /// Custom date/time ranges.
        /// </summary>
        Custom = 0,
        /// <summary>
        /// Daily range.
        /// </summary>
        Day = 1,
        /// <summary>
        /// Weekly range.
        /// </summary>
        Week = 2,
        /// <summary>
        /// Monthly range.
        /// </summary>
        Month = 3,
        /// <summary>
        /// Yeraly range.
        /// </summary>
        Year = 4,
    }
}
