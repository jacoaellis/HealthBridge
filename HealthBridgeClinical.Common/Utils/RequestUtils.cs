using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace HealthBridgeClinical.Common.Utils
{
    public static class RequestUtils
    {
        public static string GetParameters<T>(T parameterObj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var parameterList = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                var propertyValue = property.GetValue(parameterObj);
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyName = property.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? property.Name;

                if (propertyValue is ICollection)
                {
                    var objList = propertyValue as IList;
                    foreach (object value in objList)
                    {
                        parameterList.Add($"{HttpUtility.UrlEncode(propertyName)}={HttpUtility.UrlEncode(value.ToString())}");
                    }
                }
                else if (propertyValue is DateTime)
                {
                    parameterList.Add($"{HttpUtility.UrlEncode(propertyName)}={HttpUtility.UrlEncode(((DateTime)propertyValue).ToString("o"))}");
                }
                else
                {
                    parameterList.Add($"{HttpUtility.UrlEncode(propertyName)}={HttpUtility.UrlEncode(propertyValue.ToString())}");
                }
            }

            return string.Join("&", parameterList);
        }
    }
}
