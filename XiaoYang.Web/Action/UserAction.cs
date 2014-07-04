using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class UserAction : Global.UserPage {

        public override void ValidateUser() {
            base.ValidateUser();
            if (CurrentUser == null || !CurrentUser.HasPower("BO-UserManage")) {
                throw new Exception(WebSetting.Translate["action-ignore"]);
            }
        }

        public override void ValidateUrl() {
            if (!string.IsNullOrEmpty(Request.GroupString["type"])) {
                long resultID = 0;
                switch (Request.GroupString["type"]) {
                    case "getlist":
                        PageData.Add("UserList", XiaoYang.User.User.GetList(Request.Form));
                        break;
                    case "add":
                        resultID = XiaoYang.User.User.Add(Request.Form);
                        break;
                    case "edit":
                        //XiaoYang.User.User.(Request.Form);
                        if (!string.IsNullOrEmpty(Request.Form["Nickname"])) XiaoYang.User.User.EditNickname(Request.Form);
                        if (!string.IsNullOrEmpty(Request.Form["Password"])) XiaoYang.User.User.EditPassword(Request.Form);
                        if (!string.IsNullOrEmpty(Request.Form["UserGroup"]) && Convert.ToInt32(Request.Form["UserGroup"]) > 0) XiaoYang.User.User.EditUserGroup(Request.Form);
                        break;
                    case "del":
                        XiaoYang.User.User.Del(Request.Form);
                        break;
                }
            }
            Response.Write("{status:'success',message:'Success'}");
        }

        public override Xy.Web.Page.PageErrorState  onError(Exception ex){
            Response.Write(string.Format("{{status:'{0}',message:'{1}'}}", "failed", ex.Message.Replace('\'', '"').Replace(Environment.NewLine, "<br />")));
            return Xy.Web.Page.PageErrorState.Handled;
        }
    }
}
