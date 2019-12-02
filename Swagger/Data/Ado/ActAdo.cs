using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Swagger.Common;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Data.Ado
{
    public class ActAdo
    {
        #region 祝福墙

        public static List<BlessingWallInfo> p_blessing_wall_list(int status, int page, int pagesize, ref int total, ref int joinnums, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@status", status));
            parms.Add(new SqlParameter("@page", page));
            parms.Add(new SqlParameter("@pagesize", pagesize));

            SqlParameter pTotal = new SqlParameter("@total", SqlDbType.Int, 4);
            pTotal.Direction = ParameterDirection.InputOutput;
            pTotal.Value = total;
            parms.Add(pTotal);

            SqlParameter pJoinnums = new SqlParameter("@joinnums", SqlDbType.Int, 4);
            pJoinnums.Direction = ParameterDirection.InputOutput;
            pJoinnums.Value = joinnums;
            parms.Add(pJoinnums);

            db.fetch_procedure("p_blessing_wall_list", parms);

            total = (pTotal.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pTotal.Value);
            joinnums = (pJoinnums.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pJoinnums.Value);

            if (db.num_rows <= 0)
                return null;
            List<BlessingWallInfo> list = new List<BlessingWallInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                BlessingWallInfo c = new BlessingWallInfo();
                c.rid = (db.rows[i]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["rid"].ToString());
                var userid = (db.rows[i]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userid"].ToString());
                c.user = new ActUserInfo(userid);
                c.classid = (db.rows[i]["classid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["classid"].ToString());
                c.status = (db.rows[i]["status"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["status"].ToString());
                c.addtime = (db.rows[i]["addtime"] == System.DBNull.Value) ? 0 : UCommon.ConvertToLongTime(db.rows[i]["addtime"].ToString());
                c.content = (db.rows[i]["content"] == System.DBNull.Value) ? "" : db.rows[i]["content"].ToString();
                if (c.classid == 1)
                    c.content = UCommon.GetSourceUrl(c.content);
                list.Add(c);
            }
            return list;
        }

        public static bool p_blessing_wall_insert(int userid, string content, int classid, int isnewuser, int shareid, out int getscore, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@content", content));
            parms.Add(new SqlParameter("@classid", classid));
            parms.Add(new SqlParameter("@isnewuser", isnewuser));
            parms.Add(new SqlParameter("@shareid", shareid));

            SqlParameter pGetscore = new SqlParameter("@getscore", SqlDbType.Int, 4);
            pGetscore.Direction = ParameterDirection.Output;
            parms.Add(pGetscore);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_blessing_wall_insert", parms);

            getscore = (pGetscore.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pGetscore.Value);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            return retval >= 1;
        }

        #endregion

        #region 宝箱

        public static List<TreasureConfigInfo> p_treasure_config_listall(CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();


            db.fetch_procedure("p_treasure_config_listall");

            if (db.num_rows <= 0)
                return null;
            List<TreasureConfigInfo> list = new List<TreasureConfigInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                TreasureConfigInfo c = new TreasureConfigInfo();
                c.tid = (db.rows[i]["tid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["tid"].ToString());
                c.needscore = (db.rows[i]["needscore"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["needscore"].ToString());
                c.awardtype = (db.rows[i]["awardtype"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["awardtype"].ToString());
                c.name = (db.rows[i]["name"] == System.DBNull.Value) ? "" : db.rows[i]["name"].ToString();
                list.Add(c);
            }
            return list;
        }

        public static List<UserTreasurelogInfo> p_user_treasurelog_byuserid(int userid, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));

            db.fetch_procedure("p_user_treasurelog_byuserid", parms);

            if (db.num_rows <= 0)
                return null;
            List<UserTreasurelogInfo> list = new List<UserTreasurelogInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                UserTreasurelogInfo c = new UserTreasurelogInfo();
                c.rid = (db.rows[i]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["rid"].ToString());
                c.userid = (db.rows[i]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userid"].ToString());
                c.tid = (db.rows[i]["tid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["tid"].ToString());
                c.awardtype = (db.rows[i]["awardtype"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["awardtype"].ToString());
                c.userscore = (db.rows[i]["userscore"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userscore"].ToString());
                c.addtime = (db.rows[i]["addtime"] == System.DBNull.Value) ? 0 : UCommon.ConvertToLongTime(db.rows[i]["addtime"].ToString());
                c.getaward = (db.rows[i]["getaward"] == System.DBNull.Value) ? 0 : Convert.ToInt64(db.rows[i]["getaward"].ToString());
                list.Add(c);
            }
            return list;
        }

        public static bool p_treasure_open(int userid, int tid, out int awardtype, out long getaward, out string outmsg, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@tid", tid));

            SqlParameter pAwardtype = new SqlParameter("@awardtype", SqlDbType.Int, 4);
            pAwardtype.Direction = ParameterDirection.InputOutput;
            parms.Add(pAwardtype);

            SqlParameter pGetaward = new SqlParameter("@getaward", SqlDbType.BigInt, 8);
            pGetaward.Direction = ParameterDirection.InputOutput;
            parms.Add(pGetaward);

            SqlParameter pOutmsg = new SqlParameter("@outmsg", SqlDbType.VarChar, 128);
            pOutmsg.Direction = ParameterDirection.Output;
            parms.Add(pOutmsg);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_treasure_open", parms);

            outmsg = (pOutmsg.Value == System.DBNull.Value) ? "" : Convert.ToString(pOutmsg.Value);
            awardtype = (pAwardtype.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pAwardtype.Value);
            getaward = (pGetaward.Value == System.DBNull.Value) ? 0 : Convert.ToInt64(pGetaward.Value);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            if (retval >= 1)
                return true;

            return false;
        }

        #endregion

        #region 任务

        public static List<MissionConfigInfo> p_mission_config_listall(int isnewuser, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@isnewuser", isnewuser));

            db.fetch_procedure("p_mission_config_listall", parms);

            if (db.num_rows <= 0)
                return null;
            List<MissionConfigInfo> list = new List<MissionConfigInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                MissionConfigInfo c = new MissionConfigInfo();
                c.mid = (db.rows[i]["mid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["mid"].ToString());
                c.mtype = (db.rows[i]["mtype"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["mtype"].ToString());
                c.score = (db.rows[i]["score"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["score"].ToString());
                c.needtimes = (db.rows[i]["needtimes"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["needtimes"].ToString());
                c.maxtimes = (db.rows[i]["maxtimes"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["maxtimes"].ToString());
                c.isnewuser = (db.rows[i]["isnewuser"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["isnewuser"].ToString());
                c.remark = (db.rows[i]["remark"] == System.DBNull.Value) ? "" : db.rows[i]["remark"].ToString();
                c.topic = (db.rows[i]["topic"] == System.DBNull.Value) ? "" : db.rows[i]["topic"].ToString();
                list.Add(c);
            }
            return list;
        }

        public static List<UserScorelogInfo> p_mission_mylog(int userid, int isnewuser, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@isnewuser", isnewuser));

            db.fetch_procedure("p_mission_mylog", parms);

            if (db.num_rows <= 0)
                return null;
            List<UserScorelogInfo> list = new List<UserScorelogInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                UserScorelogInfo c = new UserScorelogInfo();
                c.rid = (db.rows[i]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["rid"].ToString());
                c.userid = (db.rows[i]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userid"].ToString());
                c.mid = (db.rows[i]["mid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["mid"].ToString());
                c.donetimes = (db.rows[i]["donetimes"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["donetimes"].ToString());
                c.getscore = (db.rows[i]["getscore"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["getscore"].ToString());
                c.topic = (db.rows[i]["topic"] == System.DBNull.Value) ? "" : db.rows[i]["topic"].ToString();
                c.dateflag = (db.rows[i]["dateflag"] == System.DBNull.Value) ? "" : (db.rows[i]["dateflag"].ToString());
                c.addtime = (db.rows[i]["addtime"] == System.DBNull.Value) ? 0 : UCommon.ConvertToLongTime(db.rows[i]["addtime"].ToString());
                list.Add(c);
            }
            return list;
        }

        public static bool p_user_mission_done(int userid, int mid, string topicid, out int getscore, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@mid", mid));
            parms.Add(new SqlParameter("@topicid", topicid));

            SqlParameter pGetscore = new SqlParameter("@getscore", SqlDbType.Int, 4);
            pGetscore.Direction = ParameterDirection.Output;
            parms.Add(pGetscore);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_user_mission_done", parms);

            getscore = (pGetscore.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pGetscore.Value);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            if (retval >= 1)
                return true;

            return false;
        }

        #endregion

        #region 积分

        public static UserScoreInfo p_user_score_my(int userid, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));

            db.fetch_procedure("p_user_score_my", parms);

            if (db.num_rows <= 0)
                return null;
            UserScoreInfo c = new UserScoreInfo();
            c.rid = (db.rows[0]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["rid"].ToString());
            var uid = (db.rows[0]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["userid"].ToString());
            c.user = new ActUserInfo(uid);
            c.score = (db.rows[0]["score"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["score"].ToString());
            return c;
        }

        #endregion

        #region 排行榜

        public static List<UserScoreInfo> p_user_score_rank(int userid, int page, int pagesize, ref int total, ref int rank, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@page", page));
            parms.Add(new SqlParameter("@pagesize", pagesize));

            SqlParameter pTotal = new SqlParameter("@total", SqlDbType.Int, 4);
            pTotal.Direction = ParameterDirection.InputOutput;
            pTotal.Value = total;
            parms.Add(pTotal);

            SqlParameter pRank = new SqlParameter("@rank", SqlDbType.Int, 4);
            pRank.Direction = ParameterDirection.Output;
            parms.Add(pRank);

            db.fetch_procedure("p_user_score_rank", parms);

            total = (pTotal.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pTotal.Value);
            rank = (pRank.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRank.Value);

            if (db.num_rows <= 0)
                return null;
            List<UserScoreInfo> list = new List<UserScoreInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                UserScoreInfo c = new UserScoreInfo();
                c.rid = (db.rows[i]["rn"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["rn"].ToString());
                var uid = (db.rows[i]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userid"].ToString());
                c.user = new ActUserInfo(uid);
                c.score = (db.rows[i]["score"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["score"].ToString());
                list.Add(c);
            }
            return list;
        }

        #endregion

        #region 测试

        public static List<QuestionConfigInfo> p_question_config_rand(CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();


            db.fetch_procedure("p_question_config_rand");

            if (db.num_rows <= 0)
                return null;
            List<QuestionConfigInfo> list = new List<QuestionConfigInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                QuestionConfigInfo c = new QuestionConfigInfo();
                c.qid = (db.rows[i]["qid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["qid"].ToString());
                c.title = (db.rows[i]["title"] == System.DBNull.Value) ? "" : db.rows[i]["title"].ToString();
                c.img = (db.rows[i]["img"] == System.DBNull.Value) ? "" : db.rows[i]["img"].ToString();
                c.optiona = (db.rows[i]["optionA"] == System.DBNull.Value) ? "" : db.rows[i]["optionA"].ToString();
                c.optionb = (db.rows[i]["optionB"] == System.DBNull.Value) ? "" : db.rows[i]["optionB"].ToString();
                c.optionc = (db.rows[i]["optionC"] == System.DBNull.Value) ? "" : db.rows[i]["optionC"].ToString();
                c.optiond = (db.rows[i]["optionD"] == System.DBNull.Value) ? "" : db.rows[i]["optionD"].ToString();
                c.optione = (db.rows[i]["optionE"] == System.DBNull.Value) ? "" : db.rows[i]["optionE"].ToString();
                list.Add(c);
            }
            return list;
        }

        public static bool p_question_answerlog_insert(int userid, int qid, int answer, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@qid", qid));
            parms.Add(new SqlParameter("@answer", answer));
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_question_answerlog_insert", parms);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            if (retval >= 1)
                return true;

            return false;
        }

        public static QuestionGradelogInfo p_question_user_daygrade(int userid, string date, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@date", date));

            db.fetch_procedure("p_question_user_daygrade", parms);

            if (db.num_rows <= 0)
                return null;
            QuestionGradelogInfo c = new QuestionGradelogInfo();
            c.dateflag = (db.rows[0]["dateflag"] == System.DBNull.Value) ? "" : db.rows[0]["dateflag"].ToString();
            c.rid = (db.rows[0]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["rid"].ToString());
            var uid = (db.rows[0]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["userid"].ToString());
            c.user = new ActUserInfo(uid);
            c.iq.Grade = (db.rows[0]["IQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["IQ"].ToString());
            c.eq.Grade = (db.rows[0]["EQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["EQ"].ToString());
            c.lq.Grade = (db.rows[0]["LQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["LQ"].ToString());
            c.hq.Grade = (db.rows[0]["HQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["HQ"].ToString());
            c.aq.Grade = (db.rows[0]["AQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["AQ"].ToString());
            c.fq.Grade = (db.rows[0]["FQ"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["FQ"].ToString());
            c.total = (db.rows[0]["total"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["total"].ToString());
            c.SetMaxScore();
            string sum = "";
            c.rank = 1;
            c.summary = sum;
            c.isshare = (db.rows[0]["isshare"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["isshare"].ToString());
            c.addtime = (db.rows[0]["addtime"] == System.DBNull.Value) ? 0 : UCommon.ConvertToLongTime(db.rows[0]["addtime"].ToString());
            return c;
        }

        public static bool p_question_gradelog_insert(int userid, QuestionGradelogInfo info, CDatabase db)
        {
            if (info == null) return false;
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@userid", userid));
            parms.Add(new SqlParameter("@IQ", info.iq.Grade));
            parms.Add(new SqlParameter("@EQ", info.eq.Grade));
            parms.Add(new SqlParameter("@LQ", info.lq.Grade));
            parms.Add(new SqlParameter("@HQ", info.hq.Grade));
            parms.Add(new SqlParameter("@AQ", info.aq.Grade));
            parms.Add(new SqlParameter("@FQ", info.fq.Grade));
            parms.Add(new SqlParameter("@total", info.total));
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);

            db.execute_procedure("p_question_gradelog_insert", parms);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            return retval >= 1;
        }

        public static List<GradeRankInfo> p_question_grade_rank(CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();


            db.fetch_procedure("p_question_grade_rank");

            if (db.num_rows <= 0)
                return null;
            List<GradeRankInfo> list = new List<GradeRankInfo>();
            for (int i = 0; i <= db.num_rows - 1; i++)
            {
                GradeRankInfo c = new GradeRankInfo();
                var uid = (db.rows[i]["userid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["userid"].ToString());
                c.user = new ActUserInfo(uid);
                c.grade = (db.rows[i]["grade"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[i]["grade"].ToString());
                list.Add(c);
            }
            return list;
        }

        public static bool p_question_grade_share(int rid, out int getscore, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@rid", rid));

            SqlParameter pGetscore = new SqlParameter("@getscore", SqlDbType.Int, 4);
            pGetscore.Direction = ParameterDirection.Output;
            parms.Add(pGetscore);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);


            db.execute_procedure("p_question_grade_share", parms);

            getscore = (pGetscore.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pGetscore.Value);

            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            return retval >= 1;
        }

        #endregion
    }
}
