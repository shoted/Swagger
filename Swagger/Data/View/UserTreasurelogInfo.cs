namespace Swagger.Data.View
{
    public class UserTreasurelogInfo
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
        /// 宝箱id
        /// </summary>
        public int tid { get; set; }

        /// <summary>
        /// 获得奖励类型
        /// </summary>
        public int awardtype { get; set; }

        /// <summary>
        /// 获得奖励
        /// </summary>
        public long getaward { get; set; }

        /// <summary>
        /// 当前分数
        /// </summary>
        public int userscore { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long addtime { get; set; }


    }
}
