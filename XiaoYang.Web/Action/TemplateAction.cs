using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class TemplateAction: XiaoYang.Web.Global.UserPage{
        public override void ValidateUrl() {
            switch (Request.GroupString["class"]) {
                case "attr":
                    switch (Request.GroupString["type"]) {
                        case "add":
                            XiaoYang.Entity.EntityAttributeDisplay.Add(Request.Form);
                            break;
                        case "edit":
                            XiaoYang.Entity.EntityAttributeDisplay.Edit(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityAttributeDisplay.Del(Request.Form);
                            break;
                    }
                    Response.Redirect("/entityAttributeDisplay_list." + WebSetting.Config["ext"]);
                    break;
            }
        }
    }
}
