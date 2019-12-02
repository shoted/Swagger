using System.Collections.Generic;
using System.Net;

namespace Swagger.Utils
{
    public class WJson
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// http状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// 添加或修改data指定键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        public void AddDataItem(string key, object val)
        {
            if (Data != null)
            {
                if (Data.ContainsKey(key))
                    Data[key] = val;
                else
                    Data.Add(key, val);
            }
            else
            {
                Data = new Dictionary<string, object> { { key, val } };
            }
        }

        public void SetValue(bool isSuccess, HttpStatusCode code, string message)
        {
            IsSuccess = isSuccess;
            Code = (int)code;
            Message = message;
        }
    }
}