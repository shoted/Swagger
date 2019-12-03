using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Swagger.Common
{
    public static class CTools
    {
        private static readonly Regex _regUrl = new Regex("\\(upver=(?<ver>\\d+)\\)", RegexOptions.Compiled);
        private const string DEFSTR = "";

        public static string ToJsonString(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;
            content = Regex.Replace(content, "\"", "&quot;");
            content = Regex.Replace(content, "\\\\", "\\\\");
            content = Regex.Replace(content, "\b", "\\b");
            content = Regex.Replace(content, "\f", "\\f");
            content = Regex.Replace(content, "\n", "\\n");
            content = Regex.Replace(content, "\r", "\\r");
            content = Regex.Replace(content, "\t", "\\t");
            content = Regex.Replace(content, "\0", "");
            return content;
        }

        public static string SetMd5(string content)
        {
            StringBuilder builder = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
                // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
                foreach (var s in data)
                {
                    builder.Append(s.ToString("x2"));
                }
            }
            return builder.ToString();
        }

        public static string JoinJsonString(string key, string value)
        {
            return "\"" + CTools.ToJsonString(key) + "\", \"" + CTools.ToJsonString(value) + "\"";
        }

        public static double GetTimeStamp()
        {
            return CTools.GetTimeStamp(DateTime.UtcNow);
        }

        public static double GetTimeStamp(DateTime dt)
        {
            return (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }

        public static DateTime GetUNIXTime(double value)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value).AddHours(8.0);
        }

        public static T GetDefault<T>()
        {
            return default(T);
        }

        public static string UnicodeToChinese(string content)
        {
            return new Regex("(?i)\\\\u([0-9a-f]{4})").Replace(content, (MatchEvaluator)(m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString()));
        }

        public static int GetRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next();
        }

        public static string GetRandom2()
        {
            return DateTime.Now.ToString("dHms");
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static int GetRandom(int minValue, int maxValue)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(minValue, maxValue);
        }

        public static bool IsNumber(string numbers)
        {
            return numbers != null && Regex.IsMatch(numbers, "^\\d*$");
        }

        public static int ToInt(string numbers)
        {
            return CTools.ToInt(numbers, 0);
        }

        public static int ToInt(string numbers, int ndefault)
        {
            int result;
            if (int.TryParse(numbers, out result))
                return result;
            return ndefault;
        }

        public static long ToInt64(string numbers)
        {
            return CTools.ToInt64(numbers, 0L);
        }

        public static long ToInt64(string numbers, long lDefault)
        {
            long result;
            if (long.TryParse(numbers, out result))
                return result;
            return lDefault;
        }

        public static Decimal ToDecimal(string value)
        {
            return CTools.ToDecimal(value, new Decimal(0));
        }

        public static Decimal ToDecimal(string value, Decimal defvalue)
        {
            Decimal result;
            if (Decimal.TryParse(value, out result))
                return result;
            return defvalue;
        }

        public static long ToInt64(double numbers)
        {
            return CTools.ToInt64(numbers.ToString("0"), 0L);
        }

        public static DateTime ToDateTime(string time)
        {
            return CTools.ToDateTime(time, DateTime.Now);
        }

        public static DateTime ToDateTime(string time, DateTime datetime)
        {
            DateTime result;
            if (DateTime.TryParse(time, out result))
                return result;
            return datetime;
        }

        public static string Substring(string content, int length)
        {
            if (string.IsNullOrEmpty(content))
                return "";
            if (content.Length <= length)
                return content;
            return content.Substring(0, length);
        }

        public static string SubstringEnd(string content, int length)
        {
            if (string.IsNullOrEmpty(content))
                return "";
            if (content.Length <= length)
                return content;
            return content.Substring(content.Length - length, length);
        }

        public static int Length(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string HtmlEncode(string value)
        {
            return HttpUtility.HtmlEncode(HttpUtility.UrlDecode(value));
        }

        public static long getCurrentTimeMillis()
        {
            return (long)new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks).TotalMilliseconds;
        }

        public static long convertToTimeMillis(DateTime dt)
        {
            return (long)new TimeSpan(dt.ToUniversalTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks).TotalMilliseconds;
        }

        public static DateTime convertFromTimeMillis(long ms)
        {
            return TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local).AddMilliseconds(ms);
        }

        public static string ConvertStoneToString(long stone)
        {
            if (stone >= 100000000L || stone <= -100000000L)
                return (stone % 100000000L == 0L ? (stone / 100000000L).ToString() : ((double)stone / 100000000.0).ToString("f2")) + "亿";
            if (stone >= 10000L && stone < 100000000L || stone > -100000000L && stone <= -10000L)
                return (stone % 10000L == 0L ? (stone / 10000L).ToString() : ((double)stone / 10000.0).ToString("f2")) + "万";
            return stone.ToString();
        }

        public static string GetStrFromConfig(string key, string def = "")
        {
            if (string.IsNullOrEmpty(key))
                return def;
            return def;
        }
    }
}
