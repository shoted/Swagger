namespace Swagger.Data.View
{
    public class TreasureConfigInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int tid { get; set; }

        /// <summary>
        /// 宝箱名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 达成分数
        /// </summary>
        public int needscore { get; set; }

        /// <summary>
        /// 奖励类型
        /// </summary>
        public int awardtype { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public UserTreasurelogInfo info { get; set; }     
    }
}
