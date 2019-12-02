namespace Swagger.Data.View
{
    public class UserScoreInfo
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
        /// 分数
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
    }
}
