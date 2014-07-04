using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Global {
    public class UserPage : ThemePage {

        public override void ValidateUser() {
            base.ValidateUser();
            if (CurrentUser == null || !CurrentUser.HasPower("BO-Login")) {
                throw new Exception(WebSetting.Translate["action-ignore"]);
            }
        }
    }
}
