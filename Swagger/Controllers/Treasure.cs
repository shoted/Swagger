using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Data.Ado;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Controllers
{
  
    [ApiController]
    [Route("treasure")]
    public class Treasure : TemplatePage
    {
        /// <summary>
        /// 获取宝箱
        /// </summary>
        [HttpGet]
        public void Get()
        {
            List<TreasureConfigInfo> treasure = null;
            List<UserTreasurelogInfo> treasureLog = null;
            UserScoreInfo myScore = null;
            int nextLevelScore = 0;
            using (var db = DbConfig.GetDB())
            {
                int score = 0;
                treasure = ActAdo.p_treasure_config_listall(db);
                myScore = ActAdo.p_user_score_my(UserId, db);
                if (myScore != null)
                {
                    score = myScore.score;
                    if (treasure != null)
                        myScore.Level = treasure.Where(y => y.needscore <= score).OrderByDescending(y => y.tid).Select(y => y.tid).FirstOrDefault();
                    //nextLevel = myScore.Level + 1;
                }
                treasureLog = ActAdo.p_user_treasurelog_byuserid(UserId, db);
                if (treasure != null && treasure.Count > 0)
                {
                    treasure.ForEach(x =>
                    {
                        if (treasureLog != null && treasureLog.Count > 0)
                            x.info = treasureLog.Find(y => y.tid == x.tid);
                        x.icon = x.tid <= 5 ? "purple-" : "orange-";
                        x.icon += score < x.needscore ? "none" : (x.info == null ? "isok" : "opened");
                    });
                    var nextLevel = treasure.OrderBy(x => x.needscore).FirstOrDefault(x => x.needscore > score);
                    if (nextLevel != null)
                    {
                        nextLevelScore = nextLevel.needscore - score;
                    }
                }
            }
            WJson.AddDataItem("score", myScore);
            WJson.AddDataItem("nextLevelScore", nextLevelScore);
            WJson.AddDataItem("treasure", treasure);
            WJson.AddDataItem("treasureLog", treasureLog);
            WJson.SetValue(true, HttpStatusCode.OK, "获取数据成功");
        }

        /// <summary>
        /// 开启宝箱
        /// </summary>
        /// <param name="tid">宝箱id</param>
        [HttpPost]
        public void Post(int tid = 0)
        {
            if (tid <= 0)
            {
                WJson.SetValue(false, HttpStatusCode.BadRequest, "宝箱不存在");
            }

            int awardType = 0;
            long getStone = 0;
            string outmsg = "";
            bool result = false;
            using (var db = DbConfig.GetDB())
            {
                result = ActAdo.p_treasure_open(UserId, tid, out awardType, out getStone, out outmsg, db);
            }
            if (!result)
            {
                WJson.SetValue(false, HttpStatusCode.BadRequest, outmsg);
                return;
            }
            string awardImg = "";
            string awardTypeStr = "";
            string awardStr = "";
            if (awardType == 1)
            {
                awardStr = "恭喜您获得15周年乐秀(30天)";
                awardImg = "http://pp.lexun.com/showvipimages/images/lexiu/20191126/20191126211816_99a58505d251.png";
                if (UCommon.GetUserSex(UserId) != 1)
                {
                    awardImg = "http://pp.lexun.com/showvipimages/images/lexiu/20191126/20191126211859_b4a17dc18215.png";
                }
                awardTypeStr = "15周年乐秀(30天)";
            }
            else if (awardType == 2)
            {
                awardStr = "恭喜您获得15周年勋章(30天)";
                awardImg = "http://sjgscdn.lexun.com/actcenter/images/medal.png";
                awardTypeStr = "15周年勋章(30天)";
            }
            else if (awardType == 0 && getStone > 0)
            {
                if (!UCommon.AddStone(UserId, getStone, UCommon.Sid, "开启宝箱", out outmsg))
                {
                    CLog.WriteLog(string.Format("开启宝箱加币失败 userid => {0} stone => {1}", UserId, getStone));
                }
                awardStr = "恭喜您获得乐币" + CTools.ConvertStoneToString(getStone);
                awardImg = "http://sjgscdn.lexun.com/actcenter/images/stone.png";
                awardTypeStr = "乐币";
            }
            if (awardType == 1 || awardType == 2) getStone = 1;
            WJson.AddDataItem("award", new { getStone = getStone, awardType = awardTypeStr, awardImg = awardImg, tid = tid });
            WJson.SetValue(true, HttpStatusCode.Created, awardStr);
        }
    }
}
