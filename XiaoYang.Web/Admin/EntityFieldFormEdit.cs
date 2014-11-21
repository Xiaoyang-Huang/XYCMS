using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityFieldFormEdit: Global.UserPage {
        public override void ValidateUrl() {
            base.ValidateUrl();
            if (!string.IsNullOrEmpty(Request.GroupString["id"])) {
                long _id = Convert.ToInt64(Request.GroupString["id"]);
                XiaoYang.Entity.EntityFieldForm _item = XiaoYang.Entity.EntityFieldForm.GetInstance(_id);
                PageData.AddSplitedXyDataModel("Item", _item);
            }
        }
    }
}
