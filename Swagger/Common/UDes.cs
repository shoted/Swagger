using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Swagger.Common
{
    public class UDes
    {
        public static string Key = "C908FEA1";

        public static string Encode(string str)
        {
            return UDes.Encode(str, UDes.Key);
        }

        public static string Decode(string str)
        {
            return UDes.Decode(str, UDes.Key);
        }

        public static string Encode(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                cryptoServiceProvider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                cryptoServiceProvider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte num in memoryStream.ToArray())
                    stringBuilder.AppendFormat("{0:X2}", (object)num);
                memoryStream.Close();
                return stringBuilder.ToString();
            }
            catch 
            {
                return string.Empty;
            }
        }

        public static string Decode(string str, string key)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                cryptoServiceProvider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                cryptoServiceProvider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] buffer = new byte[str.Length / 2];
                for (int index = 0; index < str.Length / 2; ++index)
                {
                    int int32 = Convert.ToInt32(str.Substring(index * 2, 2), 16);
                    buffer[index] = (byte)int32;
                }
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(buffer, 0, buffer.Length);
                cryptoStream.FlushFinalBlock();
                memoryStream.Close();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                CLog.WriteLog("Decode:" + ex.Message + ex.StackTrace);
                return string.Empty;
            }
        }
    }
}
