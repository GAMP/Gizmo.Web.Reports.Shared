using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System;
using System.Linq;

namespace Gizmo.Web.Reports
{
    public static class ReportModelHelper
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> TypeCache = new();
        private static readonly Predicate<PropertyInfo> PropertyFilter = (p) => { return p.GetCustomAttribute<ReportFilterFieldAttribute>() != null; };

        /// <summary>
        /// Set filter default values.
        /// </summary>
        /// <param name="reportFilter">Report filter.</param>
        /// <remarks>
        /// Default value will only be set if <see cref="DefaultValueAttribute"/> is applied to a filter object property.
        /// </remarks>
        public static void SetDefaultValues(IReportFilter reportFilter)
        {
            var filterType = reportFilter.GetType();
            var filterProperties = GetFilterProperties(filterType);
            foreach (var property in filterProperties)
                SetDefaultValue(reportFilter, property);
        }

        /// <summary>
        /// Set filter values.
        /// </summary>
        /// <param name="reportFilter">Report filter.</param>
        /// <param name="filterValues">Filter values.</param>
        /// <param name="setDefaultValue">Indicates if we should attempt to set default value on a property in case a value is not present in <paramref name="setDefaultValue"/> dictionary.</param>
        public static void SetValues(IReportFilter reportFilter, IDictionary<string, object?> filterValues, bool setDefaultValue = true)
        {
            var filterType = reportFilter.GetType();
            var filterProperties = GetFilterProperties(filterType);

            foreach (var filterProperty in filterProperties)
            {
                //check if property was found on filter object
                if (filterValues.TryGetValue(filterProperty.Name, out var value))
                {
                    SetValue(reportFilter, filterProperty, value);
                }
                else
                {
                    if (setDefaultValue)
                        SetDefaultValue(reportFilter, filterProperty);
                }
            }

        }

        /// <summary>
        /// Gets filter values.
        /// </summary>
        /// <param name="reportFilter">Report filter.</param>
        /// <returns>Key/Value filter parameters.</returns>
        public static IDictionary<string, object?> GetFilterValues(IReportFilter reportFilter)
        {
            var filterType = reportFilter.GetType();
            var filterProperties = GetFilterProperties(filterType);
            return filterProperties.ToDictionary(p => p.Name, p => p.GetValue(reportFilter));
        }

        public static TValue? GetFilterValue<TValue>(IReportFilter reportFilter, PropertyInfo filterProperty)
        {
            var value = (TValue?)filterProperty.GetValue(reportFilter);
            return value;
        }

        public static void SetValue(IReportFilter reportFilter, PropertyInfo propertyInfo, object? value)
        {
            SetValue((object)reportFilter, propertyInfo, value);
        }

        private static void SetValue(object targetObject, PropertyInfo propertyInfo, object? value)
        {
            object? convertedValue = value;

            //check if the value type matches filter property type, if not try to convert it
            //this can throw NotSupportedException if compatible filter does not exist
            if (value != null && propertyInfo.PropertyType != value.GetType())
            {
                var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                convertedValue = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            }

            //TODO : check if property is nullable

            propertyInfo.SetValue(targetObject, convertedValue);
        }

        public static bool SetDefaultValue(IReportFilter reportFilter, PropertyInfo propertyInfo)
        {
            //set to null if property supports it
            if (Nullable.GetUnderlyingType(propertyInfo.PropertyType!) != null)
            {
                propertyInfo.SetValue(reportFilter, null);
            }

            //set any default value that is specified by attribute
            var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute != null)
            {
                propertyInfo.SetValue(reportFilter, defaultValueAttribute.Value);
                return true;
            }          

            return false;
        }

        /// <summary>
        /// Gets filter properties.
        /// </summary>
        /// <param name="reportFilter">Filter instance.</param>
        /// <returns>Filter properties.</returns>
        /// <remarks>
        /// Only properties marked with <see cref="ReportFilterFieldAttribute"/> will be included.
        /// </remarks>
        public static IEnumerable<PropertyInfo> GetFilterProperties(IReportFilter reportFilter)
        {
            return GetFilterProperties(reportFilter.GetType());
        }

        /// <summary>
        /// Get filter properties.
        /// </summary>
        /// <param name="type">Filter type.</param>
        /// <returns>Filter properties.</returns>
        /// <remarks>
        /// Only properties marked with <see cref="ReportFilterFieldAttribute"/> will be included.
        /// </remarks>
        private static IEnumerable<PropertyInfo> GetFilterProperties(Type type)
        {
            return TypeCache.GetOrAdd(type, type.GetProperties().Where(p => PropertyFilter(p)));
        }

        public static Expression<Func<T>> MemberExpression<T>(object obj, string propertyName)
        {
            Expression memberExpr = Expression.PropertyOrField(Expression.Constant(obj), propertyName);
            return Expression.Lambda<Func<T>>(memberExpr);
        }

        public static Expression<Func<T>> MemberExpression<T>(object obj, PropertyInfo propertyInfo)
        {
            Expression memberExpr = Expression.MakeMemberAccess(Expression.Constant(obj), propertyInfo);
            return Expression.Lambda<Func<T>>(memberExpr);
        }

        /// <summary>
        /// Gets filter values represented in JSON format.
        /// </summary>
        /// <param name="filter">Filter instance.</param>
        /// <returns>Key/Value JSON serialized filters.</returns>
        public static string GetFilterValuesJson(IReportFilter filter)
        {
            var filterValues = GetFilterValues(filter);
            return JsonSerializer.Serialize(filterValues);
        }

        /// <summary>
        /// Returns parsed filter values.
        /// </summary>
        /// <param name="filterType">Report filter object type.</param>
        /// <param name="json">Values JSON.</param>
        /// <returns>Key/Value filter parameters.</returns>
        /// <remarks>
        /// Any extra parameters present in <paramref name="json"/> that does not match any properties on filter type <paramref name="filterType"/> will be ignored.<br></br>
        /// The returned dictionary will have key value object type set based on filter property and should be directly settable on appropriate filter property.
        /// </remarks>
        public static IDictionary<string, object?> GetFilterValuesFromJson(Type filterType, string json)
        {
            //get all filter properties
            var filterProperties = GetFilterProperties(filterType);

            //get values present in json, we could deserialize to <string,object> but it would be easier to user <string,JsonElement>
            //so we can handle all the values parsing in similar way
            var jsonValues = JsonSerializer.Deserialize<IDictionary<string, JsonElement>>(json)!;

            //create result set
            var results = new Dictionary<string, object?>();

            foreach (var property in filterProperties)
            {
                if (jsonValues.TryGetValue(property.Name, out var jsonElement))
                {
                    object? value;

                    try
                    {
                        switch (jsonElement.ValueKind)
                        {
                            case JsonValueKind.String:
                                value = jsonElement.GetString();
                                break;
                            case JsonValueKind.Number:
                                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                                value = converter.ConvertFrom(null, CultureInfo.InvariantCulture, jsonElement.ToString());
                                break;
                            case JsonValueKind.False:
                            case JsonValueKind.True:
                                value = jsonElement.GetBoolean();
                                break;
                            case JsonValueKind.Null:
                                value = null;
                                break;
                            //object values not supported
                            case JsonValueKind.Object:
                            default:
                                continue;
                        }
                    }
                    catch (NotSupportedException)
                    {
                        //do not include any values that failed to convert
                        //they could be set to their default values in the filter in such cases
                        continue;
                    }

                    //TODO : check if property is nullable

                    results.Add(property.Name, value);
                }
                else
                {
                    //json contained a property that is not present
                    //on the filter object (model)
                }
            }
            return results;
        }
    }
}
