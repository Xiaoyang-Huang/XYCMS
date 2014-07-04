using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class PowerAction : Global.UserPage {

        public override void ValidateUser() {
            base.ValidateUser();
            if (CurrentUser == null || !CurrentUser.HasPower("BO-PowerManage")) {
                throw new Exception(WebSetting.Translate["action-ignore"]);
            }
        }

        public override void ValidateUrl() {
            if (!string.IsNullOrEmpty(Request.GroupString["type"])) {
                long resultID = 0;
                switch (Request.GroupString["type"]) {
                    case "add":
                        if (!Xy.Tools.Web.Form.ValidateForm(Request.Form)) throw new Exception(WebSetting.Translate["error-empty"]);
                        resultID = XiaoYang.User.PowerList.Add(Request.Form);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", resultID));
                        break;
                    case "del":
                        XiaoYang.User.PowerList.Del(Request.QueryString);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", Request.QueryString["ID"]));
                        break;
                    case "addgroup":
                        if (!Xy.Tools.Web.Form.ValidateForm(Request.Form)) throw new Exception(WebSetting.Translate["error-empty"]);
                        resultID = XiaoYang.User.UserGroup.Add(Request.Form);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", resultID));
                        break;
                    case "delgroup":
                        XiaoYang.User.UserGroup.Del(Request.QueryString);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", Request.QueryString["ID"]));
                        break;
                    case "getpowerbygroup":
                        Response.Write("[");
                        Xy.Data.Procedure _pc = new Xy.Data.Procedure("getPowerList", @"select [PowerList].* from [PowerShip] left join [PowerList] on [PowerList].[ID] = [PowerShip].[PowerList] where [PowerShip].[UserGroup] = " + Request.QueryString["ID"]);
                        System.Data.DataTable _dt = _pc.InvokeProcedureFill();
                        for (int i = 0; i < _dt.Rows.Count; i++) {
                            System.Data.DataRow _dr = _dt.Rows[i];
                            if (i > 0) Response.Write(',');
                            Response.Write(string.Format("{{id:\"{0}\",key:\"{1}\",description:\"{2}\"}}", _dr["ID"].ToString(), _dr["Key"].ToString(), _dr["Description"].ToString()));
                        }
                        Response.Write("]");
                        break;
                    case "addship":
                        if (!Xy.Tools.Web.Form.ValidateForm(Request.Form)) throw new Exception(WebSetting.Translate["error-empty"]);
                        resultID = XiaoYang.User.PowerShip.Add(Request.Form);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", resultID));
                        break;
                    case "delship":
                        XiaoYang.User.PowerShip.Del(Request.Form);
                        Response.Write(string.Format("{{ status:'success', id:{0} }}", Request.Form["PowerList"]));
                        break;
                }
                if (string.Compare(Request.GroupString["type"], "getpowerbygroup") != 0) XiaoYang.User.UserPermissionCollection.ClearCache();
            }
        }

        public override Xy.Web.Page.PageErrorState onError(Exception ex) {
            Response.Clear();
            //Response.Write(string.Format("{{ status:\"failed\", message:\"{0}\" }}", ex.Message.Replace(Environment.NewLine, "\\n").Replace('"', '\'')));
            return Xy.Web.Page.PageErrorState.ThrowOut;
        }
    }
}
