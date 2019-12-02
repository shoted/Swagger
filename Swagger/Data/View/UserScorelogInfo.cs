namespace Swagger.Data.View
{
    public class UserScorelogInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int rid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// 渠道id
        /// </summary>
        public int mid { get; set; }

        /// <summary>
        /// 完成次数
        /// </summary>
        public int donetimes { get; set; }

        /// <summary>
        /// 获得积分
        /// </summary>
        public int getscore { get; set; }

        /// <summary>
        /// 帖子集合
        /// </summary>
        public string topic { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string dateflag { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long addtime { get; set; }

    }
}
