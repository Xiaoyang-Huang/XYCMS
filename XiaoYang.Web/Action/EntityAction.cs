using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class EntityAction: Global.UserPage {
        public override void ValidateUrl() {
            base.ValidateUrl();
            switch (Request.GroupString["type"]) {
                case "type":
                    switch (Request.GroupString["action"]) { 
                        case "add":
                            XiaoYang.Entity.EntityType.Add(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityType.Del(Request.Form);
                            break;
                        case "check":
                            string[] _errors = XiaoYang.Entity.EntityType.Check(Convert.ToInt64(Request.Form["ID"]));
                            if (_errors.Length > 0) {
                                Response.Write(string.Format("{{status:'{0}',message:[\"{1}\"]}}", "failed", string.Join("\",\"", _errors)));
                            }
                            break;
                        case "active":
                            XiaoYang.Entity.EntityType.EditActive(Request.Form);
                            break;
                        case "available":
                            XiaoYang.Entity.EntityType.EditAvailable(Request.Form);
                            break;
                    }
                    break;
                case "table":
                    switch (Request.GroupString["action"]) {
                        case "add":
                            XiaoYang.Entity.EntityTable.Add(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityTable.Del(Request.Form);
                            break;
                    }
                    break;
                case "field":
                    switch (Request.GroupString["action"]) { 
                        case "add":
                            XiaoYang.Entity.EntityField.Add(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityField.Del(Request.Form);
                            break;
                    }
                    break;
                case "form":
                    switch (Request.GroupString["action"]) {
                        case "add":
                            Response.Write("{status:'success',message:'success',id:'" + XiaoYang.Entity.EntityFieldForm.Add(Request.Form["Name"], Server.UrlDecode(Request.Form["Resource"]), Server.UrlDecode(Request.Form["Template"])) + "'}");
                            break;
                        case "edit":
                            XiaoYang.Entity.EntityFieldForm.Edit(Convert.ToInt64(Request.Form["ID"]), Request.Form["Name"], Server.UrlDecode(Request.Form["Resource"]), Server.UrlDecode(Request.Form["Template"]));
                            break;
                        case "del":
                            XiaoYang.Entity.EntityFieldForm.Del(Request.Form);
                            break;
                    }
                    break;
            }
            if (!HTMLContainer.HasContent) {
                Response.Write("{status:'success',message:'success'}");
            }
        }
    }
}
