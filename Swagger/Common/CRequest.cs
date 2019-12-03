using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Swagger.Common
{
    public static class CRequest
    {
        public static IHttpContextAccessor CurrentContext = null;

        public static int GetInt(string query)
        {
            return GetInt(query, 0);
        }
        public static int GetInt(string query, int def)
        {
            if (CurrentContext.HttpContext.Request.Query.TryGetValue(query, out var value))
            {
                return CTools.ToInt(value);
            }

            if (CurrentContext.HttpContext.Request.Form.TryGetValue(query, out value))
            {
                return CTools.ToInt(value);
            }
            return def;
        }

        public static long GetInt64(string query)
        {
            return GetInt64(query, 0);
        }

        public static long GetInt64(string query, long def)
        {

            if (CurrentContext.HttpContext.Request.Query.TryGetValue(query, out var value))
            {
                return CTools.ToInt64(value);
            }
            if (CurrentContext.HttpContext.Request.Form.TryGetValue(query, out value))
            {
                return CTools.ToInt64(value);
            }
            return def;
        }

        public static string GetString(string query)
        {
            return GetString(query, string.Empty);
        }

        public static string GetString(string query, string def)
        {
            if (CurrentContext.HttpContext.Request.Query.TryGetValue(query, out var value))
            {
                return value;
            }
            if (CurrentContext.HttpContext.Request.Form.TryGetValue(query, out value))
            {
                return value;
            }
            return def;
        }
    }
}
