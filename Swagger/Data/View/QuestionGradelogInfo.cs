using System.Collections.Generic;
using System.Linq;

namespace Swagger.Data.View
{
    public class QuestionGradelogInfo
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
        /// 智商
        /// </summary>
        public TestGrade iq { get; set; }

        /// <summary>
        /// 情商
        /// </summary>
        public TestGrade eq { get; set; }

        /// <summary>
        /// 爱商
        /// </summary>
        public TestGrade lq { get; set; }

        /// <summary>
        /// 健商
        /// </summary>
        public TestGrade hq { get; set; }

        /// <summary>
        /// 逆商
        /// </summary>
        public TestGrade aq { get; set; }

        /// <summary>
        /// 财商
        /// </summary>
        public TestGrade fq { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 最高分
        /// </summary>
        public int maxscore { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        public int rank { get; set; }

        /// <summary>
        /// 综合
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 是否分享
        /// </summary>
        public int isshare { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string dateflag { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long addtime { get; set; }

        public QuestionGradelogInfo()
        {
            iq = new TestGrade();
            eq = new TestGrade();
            lq = new TestGrade();
            hq = new TestGrade();
            aq = new TestGrade();
            fq = new TestGrade();
        }

        public QuestionGradelogInfo(int userid)
        {
            user = new ActUserInfo(userid);
            iq = new TestGrade();
            eq = new TestGrade();
            lq = new TestGrade();
            hq = new TestGrade();
            aq = new TestGrade();
            fq = new TestGrade();

            total = iq.Grade + eq.Grade + lq.Grade + hq.Grade + aq.Grade + fq.Grade;
            SetMaxScore();
            string sum = "";
            rank = 1;
            summary = sum;
        }

        public void SetMaxScore()
        {
            var dic = new Dictionary<int, TestGrade>()
            {
                {0,iq},{1,eq},{2,lq},{3,hq},{4,aq},{5,fq}
            };
            var maxScore = dic.OrderByDescending(x => x.Value.Grade).FirstOrDefault();
            maxScore.Value.ismax = 1;
            maxscore = maxScore.Key;
        }
    }
}

