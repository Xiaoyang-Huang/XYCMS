using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class AccessAction : Xy.Web.Page.PageAbstract {

        int _errCount = 0;
        public override void Validate() {
            if (!string.IsNullOrEmpty(Request.GroupString["type"])) {
                string error = string.Empty;
                switch (Request.GroupString["type"]) {
                    case "login":
                        if (!string.IsNullOrEmpty(Session["errCount"])) {
                            _errCount = Convert.ToInt32(Session["errCount"]) + 1;
                        }
                        if (_errCount > 5) {
                            if (string.Compare(Request.Form["captcha"], Session["login-SecureCode"], true) != 0) throw new Exception(WebSetting.Translate["login-failed"]);
                        }
                        Xy.Web.Security.IUser _u = User.User.Login(Request.Form["username"], Request.Form["password"], true, out error);
                        if (_u.HasPower("BO-Login")) _u.WriteCookie(WebSetting.SessionOutTime, URL.Domain);
                        if (!string.IsNullOrEmpty(error)) {
                            Session["errCount"] = _errCount.ToString();
                            throw new Exception(WebSetting.Translate["login-failed"]);
                        } else {
                            Session["errCount"] = "0";
                            if (!string.IsNullOrEmpty(Request.Form["remember"])) {
                                Xy.Tools.Web.Cookie.Add("UserKey", Xy.Tools.Web.Cookie.Get("UserKey"), 60 * 24 * 7);
                            }
                        }
                        break;
                    case "logout":
                        XiaoYang.User.User.Logout();
                        Response.Redirect("index." + WebSetting.Config["ext"]);
                        break;
                }
            }
            Response.Write("{status:'success',message:'Success'}");
        }

        public override Xy.Web.Page.PageErrorState onError(Exception ex) {
            Response.Write(string.Format("{{status:'{0}',message:'{1}',count:{2}}}", "failed", ex.Message.Replace('\'', '"').Replace(Environment.NewLine, "<br />"), _errCount));
            return Xy.Web.Page.PageErrorState.Handled;
        }
    }
}
