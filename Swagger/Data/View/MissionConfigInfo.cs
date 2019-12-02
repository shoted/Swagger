namespace Swagger.Data.View
{
    public class MissionConfigInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int mid { get; set; }

        /// <summary>
        /// 0,每日任务     1.账号任务
        /// </summary>
        public int mtype { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 可获得狂热值
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// 需要完成次数
        /// </summary>
        public int needtimes { get; set; }

        /// <summary>
        /// 最大完成次数
        /// </summary>
        public int maxtimes { get; set; }

        /// <summary>
        /// 是否新用户任务
        /// </summary>
        public int isnewuser { get; set; }

        /// <summary>
        /// 帖子集合
        /// </summary>
        public string topic { get; set; }

        /// <summary>
        /// 完成地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 链接状态
        /// </summary>
        public int urltype { get; set; }

        /// <summary>
        /// 剩余次数
        /// </summary>
        public string leftcount { get; set; }
    }
}
