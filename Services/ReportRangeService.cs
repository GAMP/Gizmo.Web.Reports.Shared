using System;
using Gizmo.Server.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gizmo.Web.Reports.Services
{
    /// <summary>
    /// Report module range generation service.
    /// </summary>
    public sealed class ReportRangeService
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="businessOptions">Business options.</param>
        /// <param name="logger">Logger.</param>
        public ReportRangeService(IOptionsMonitor<BusinessOptions> businessOptions, ILogger<ReportRangeService> logger)
        {
            _businessOptions = businessOptions;
            _logger = logger;
        }

        private readonly IOptionsMonitor<BusinessOptions> _businessOptions;
        private readonly ILogger<ReportRangeService> _logger;

        private ILogger Logger => _logger;

        /// <summary>
        /// Converts report range type to date start/end range.
        /// </summary>
        /// <param name="rangeType">Report range type.</param>
        /// <param name="rangeStart">Range start.</param>
        /// <param name="rangeEnd">Range end.</param>
        /// <param name="startDate">Optional start date.</param>
        /// <remarks>Passing <see cref="Reports.ReportRangeType.Custom"/> will throw <see cref="NotSupportedException"/>.</remarks>
        /// <exception cref="NotSupportedException"></exception>
        public void CreateReportRange(Web.Reports.ReportRangeType rangeType, out DateTime rangeStart, out DateTime rangeEnd, DateTime? startDate = null)
        {
            bool businessDayStartValid = false;
            bool businessDayEndValid = false;

            //parse default business day start
            if (!(businessDayStartValid = TimeOnly.TryParse(_businessOptions.CurrentValue.BusinessDayStart, out var businessDayStartTime)))
                Logger.LogWarning("Invalid business day start value : {dayTime} specified.", _businessOptions.CurrentValue.BusinessDayStart);

            //parse default business day end
            if (!(businessDayEndValid = TimeOnly.TryParse(_businessOptions.CurrentValue.BusinessDayEnd, out var businessDayEndTime)))
                Logger.LogWarning("Invalid business day end value : {dayTime} specified.", _businessOptions.CurrentValue.BusinessDayEnd);

            //create a starting point date , use provider value or current time as base
            var dateOnlyStart = DateOnly.FromDateTime(startDate ?? DateTime.Now);

            switch (rangeType)
            {
                case Reports.ReportRangeType.Day:
                    //no change
                    break;
                case Reports.ReportRangeType.Week:
                    //get effective start day
                    var businessWeekStartDay = _businessOptions.CurrentValue.BusinessStartWeekDay ?? DayOfWeek.Monday;

                    //this will calculate difference in days between week days
                    var dayDifference = (int)businessWeekStartDay - (int)dateOnlyStart.DayOfWeek;

                    //add the difference to start
                    dateOnlyStart = dateOnlyStart.AddDays(dayDifference);
                    break;
                case Reports.ReportRangeType.Month:
                    //when using monthly we dont care about business start weekday, should always start from start of month
                    dateOnlyStart = new DateOnly(dateOnlyStart.Year, dateOnlyStart.Month, 1);
                    break;
                case Reports.ReportRangeType.Year:
                    //when using year we dont care about business start weekday, should always start from start of year
                    dateOnlyStart = new DateOnly(dateOnlyStart.Year, 1, 1);
                    break;
                default:
                    throw new NotSupportedException("Filter range not supported.");
            }

            //create range start date/time, it will be equal to the date only start and optionally will add a business day start time
            rangeStart = new DateTime(dateOnlyStart, businessDayStartValid ? businessDayStartTime : TimeOnly.MinValue);

            int rangeTotalDays = 0;
            switch (rangeType)
            {
                case Reports.ReportRangeType.Day:
                    //if business day starts after business day end then we need to rol over to next day
                    //otherwise the range is represented within same day
                    rangeTotalDays = businessDayStartValid ? (businessDayStartTime > businessDayEndTime ? 1 : 0) : 1;
                    break;
                case Reports.ReportRangeType.Week:
                    rangeTotalDays = 7;
                    break;
                case Reports.ReportRangeType.Month:
                    rangeTotalDays = DateTime.DaysInMonth(rangeStart.Year, rangeStart.Month);
                    break;
                case Reports.ReportRangeType.Year:
                    rangeTotalDays = 365 - rangeStart.DayOfYear;
                    break;
                default:
                    break;
            }

            //by default range will be equal to start plus one day
            rangeEnd = rangeStart.AddDays(rangeTotalDays);

            //add end time to range end
            rangeEnd = new DateTime(DateOnly.FromDateTime(rangeEnd), businessDayEndTime);
        }
    }
}
