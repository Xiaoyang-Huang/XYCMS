using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityEdit: Global.UserPage {
        public override void ValidateUrl() {
            base.ValidateUrl();
            switch (Request.GroupString["type"]) {
                case "add":
                    break;
                case "edit":
                    break;
            }
        }
    }
}
