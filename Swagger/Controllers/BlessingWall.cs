using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Swagger.Common;
using Swagger.Data.Ado;
using Swagger.Data.View;
using Swagger.Utils;

namespace Swagger.Controllers
{
    [ApiController]
    [Route("blessingWall")]
    public class BlessingWall : TemplatePage
    {
        /// <summary>
        /// 获取祝福墙
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="token">邀请加密串</param>
        /// <returns>祝福墙列表</returns>
        [HttpGet]
        public void Get(int page = 1, int pageSize = 10, string token = "")
        {
            if (page <= 0 || pageSize <= 0)
            {
                WJson.SetValue(false, HttpStatusCode.BadRequest, "页码错误");
                return;
            }
            int shareid = 0;
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var shareStr = UDes.Decode(token);
                    shareid = CTools.ToInt(Regex.Matches(shareStr, @"shareid=(?<shareid>\d*)")[0].Groups["shareid"].Value);
                }
                catch (Exception ex)
                {
                    CLog.WriteLog("token = " + token);
                    CLog.WriteLog(ex.Message + ex.StackTrace);
                }
            }

            int total = 0;
            int joinnums = 0;
            List<BlessingWallInfo> data = null;
            using (var db = DbConfig.GetDB())
            {
                data = ActAdo.p_blessing_wall_list(0, page, pageSize, ref total, ref joinnums, db);
            }
            PageViewModel pageViewModel = new PageViewModel(page, pageSize, total).CalcPageData();

            WJson.AddDataItem("blessing", data);
            WJson.AddDataItem("joinNums", joinnums);
            WJson.AddDataItem("pagination", pageViewModel);
            WJson.AddDataItem("shareUser", new ActUserInfo(shareid));
            WJson.SetValue(true, HttpStatusCode.OK, "获取数据成功");
        }

        /// <summary>
        /// 发送祝福
        /// </summary>
        /// <param name="content">祝福内容</param>
        /// <param name="classid">消息类型</param>
        /// <param name="token">邀请加密串</param>
        /// <returns>是否成功</returns>
        [HttpPost]
        public void Post(string content, int classid = 0, string token = "")
        {
            if (string.IsNullOrEmpty(content) || classid < 0)
            {
                WJson.SetValue(false, HttpStatusCode.BadRequest, "参数错误");
                return;
            }
            //var response = CheckApi.Check(0, content);
            //if (response != null && response.code == 200 && response.level == 2 &&
            //    (response.label == 100 || response.label == 200 || response.label == 500 || response.label == 600))
            //{
            //    WJson.SetValue(false, HttpStatusCode.BadRequest, "包含非法关键字: " + response.words);
            //    return BadRequest();
            //}
            bool result = false;
            int status = 0;
            using (var db = DbConfig.GetDB())
            {
                int isNewUser = 0;
                int shareid = 0;
                if (!string.IsNullOrEmpty(token) && token != "0")
                {
                    try
                    {
                        var shareStr = UDes.Decode(token);
                        shareid = CTools.ToInt(Regex.Matches(shareStr, @"shareid=(?<shareid>\d*)")[0].Groups["shareid"].Value);
                    }
                    catch (Exception ex)
                    {
                        CLog.WriteLog("token = " + token);
                        CLog.WriteLog(ex.Message + ex.StackTrace);
                    }
                }
                content = CTools.HtmlEncode(content);
                var getScore = 0;
                result = ActAdo.p_blessing_wall_insert(UserId, content, classid, isNewUser, shareid, out getScore, db);
                if (getScore > 0)
                {
                    status = 1;
                }
            }
            if (result)
            {
                WJson.AddDataItem("unlockStatus", status);
                WJson.AddDataItem("blessing", new BlessingWallInfo(UserId, content, classid));
                WJson.SetValue(true, HttpStatusCode.Created, "创建成功");
            }
            else
            {
                WJson.SetValue(false, HttpStatusCode.InternalServerError, "网络繁忙");
            }
        }
    }
}
