using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;

namespace Gizmo.Web.Reports.Services
{
    /// <summary>
    /// Report link generation service.
    /// </summary>
    public sealed class ReportLinkGenerationService
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="navigationManager">Navigation manager.</param>
        public ReportLinkGenerationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        private readonly NavigationManager _navigationManager;

        /// <summary>
        /// Creates report uri.
        /// </summary>
        /// <param name="reportId">Report id.</param>
        /// <param name="parameterName">Query parameter name.</param>
        /// <param name="parameterValue">Query parameter value.</param>
        /// <returns>Report URI.</returns>
        /// <remarks>
        /// URI is always generated based on currently location.
        /// </remarks>
        public string CreateReportUri(string reportId, string parameterName, string parameterValue)
        {
            return CreateReportUri(reportId, [new KeyValuePair<string,string>(parameterName,parameterValue)]);
        }

        /// <summary>
        /// Creates report uri.
        /// </summary>
        /// <param name="reportId">Report id.</param>
        /// <param name="parameters">Query parameters.</param>
        /// <returns>Report URI.</returns>
        /// <remarks>
        /// URI is always generated based on currently location.
        /// </remarks>
        public string CreateReportUri(string reportId, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var uriBuilder = new UriBuilder(_navigationManager.Uri)
            {
            };
            var queryBuilder = new QueryBuilder
            {
                { "reportId", reportId.ToString() },
            };

            foreach (var parameter in parameters)
            {
                queryBuilder.Add(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = queryBuilder.ToString();
            return uriBuilder.ToString();
        }

        public string CreateReportUri(string reportId, ReportRangeType rangeType, DateTime? rangeStart, DateTime? rangeEnd, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var uriBuilder = new UriBuilder(_navigationManager.Uri)
            {
            };
            var queryBuilder = new QueryBuilder
            {
                { "ReportId", reportId.ToString() },
                { "ReportRangeType", rangeType.ToString() }
            };

            if (rangeStart != null)
                queryBuilder.Add("RangeStart", rangeStart?.ToString());           

            if (rangeEnd != null)
                queryBuilder.Add("RangeEnd", rangeEnd?.ToString());

            foreach (var parameter in parameters)
            {
                queryBuilder.Add(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = queryBuilder.ToString();
            return uriBuilder.ToString();
        }
    }
}
