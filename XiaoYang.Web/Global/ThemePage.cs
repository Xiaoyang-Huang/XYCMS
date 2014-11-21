using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Global {
    public class ThemePage : Xy.Web.Page.UserPage {

        public XiaoYang.User.User U;

        protected override Xy.Web.Security.IUser GetCurrentUser() {
            if (!string.IsNullOrEmpty(Xy.Tools.Web.Cookie.Get("UserKey"))) {
                string error = string.Empty;
                Xy.Web.Security.IUser tempIU = XiaoYang.User.User.LoginByKey(Xy.Tools.Web.Cookie.Get("UserKey"), out error);
                if (!string.IsNullOrEmpty(error)) {
                    return null;
                }
                return tempIU;
            }
            return null;
        }

        public override void ValidateUser() {
            if (CurrentUser != null) {
                U = XiaoYang.User.User.GetInstance(CurrentUser.ID);
                PageData.AddSplitedXyDataModel("CurrentUser", U);
            }
        }

        public override void onGetRequest() {
            string theme = Xy.Tools.Web.Cookie.Get("theme");
            if (!string.IsNullOrEmpty(Request.QueryString["theme"])) {
                theme = Request.QueryString["theme"];
                Xy.Tools.Web.Cookie.Add("theme", Request.QueryString["theme"]);
            }
            if (!string.IsNullOrEmpty(theme)) {
                ChangeConfig("admin_" + theme);
            }
        }
    }
}
