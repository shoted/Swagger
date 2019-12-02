using Swagger.Common;
using System;

namespace Swagger.Data.View
{
    public class BlessingWallInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int rid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public ActUserInfo user { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int classid { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long addtime { get; set; }

        public BlessingWallInfo()
        {
        }

        public BlessingWallInfo(int userid,string content,int classid)
        {
            user = new ActUserInfo(userid);
            this.classid = classid;
            this.content = content;
            addtime = CTools.convertToTimeMillis(DateTime.Now);
        }
    }
}
