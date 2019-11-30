using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Common
{
    public static class ConvertUtil
    {
        public static int ToInt(this object obj, int defVal = 0)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return defVal;
            }
        }
    }
}
