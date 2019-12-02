using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Data.Ado;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("question")]
    public class Question : TemplatePage
    {

        /// <summary>
        /// 获取测试题目信息
        /// </summary>
        [HttpGet]
        public void Get()
        {
            List<QuestionConfigInfo> question = null;
            using (var db = DbConfig.GetDB())
            {
                if (UserId > 0)
                {
                    var grade = ActAdo.p_question_user_daygrade(UserId, "", db);
                    if (grade != null)
                    {
                        WJson.AddDataItem("grade", grade);
                        WJson.SetValue(false, HttpStatusCode.BadRequest, "今日已答过题");
                        return;
                    }
                }
                question = ActAdo.p_question_config_rand(db);
            }
            WJson.AddDataItem("question", question);
            WJson.SetValue(true, HttpStatusCode.OK, "获取数据成功");
        }

        /// <summary>
        /// 答题
        /// </summary>
        /// <remarks>
        /// 答案格式 1,2,3
        /// </remarks>
        /// <param name="answer">答案</param>
        [HttpPost]
        public void Post(string answer = "")
        {
            var answerArr = answer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (answerArr.Length < 3)
            {
                WJson.SetValue(false, HttpStatusCode.BadRequest, "参数错误");
                return;
            }
            foreach (var item in answerArr)
            {
                int ans = CTools.ToInt(item);
                if (ans > 5 || ans < 1)
                {
                    WJson.SetValue(false, HttpStatusCode.BadRequest, "选项不存在");
                    return;
                }
            }
            QuestionGradelogInfo gradeInfo = new QuestionGradelogInfo(UserId);
            using (var db = DbConfig.GetDB())
            {
                for (int i = 0; i < answerArr.Length; i++)
                {
                    ActAdo.p_question_answerlog_insert(UserId, i, CTools.ToInt(answerArr[i]), db);
                }
                ActAdo.p_question_gradelog_insert(UserId, gradeInfo, db);
            }
            WJson.AddDataItem("grade", gradeInfo);
            WJson.SetValue(true, HttpStatusCode.Created, "");
        }
    }
}