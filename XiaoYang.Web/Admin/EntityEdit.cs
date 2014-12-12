using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityEdit: Global.UserPage {
        public override void ValidateUrl() {
            base.ValidateUrl();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(Convert.ToInt64(Request.GroupString["ID"]));
            XiaoYang.Entity.IHandler _handle = new XiaoYang.Entity.DefaultHandler();
            switch (Request.GroupString["type"]) {
                case "add":
                    break;
                case "edit":
                    
                    break;
                case "del":
                    break;
            }
        }
    }
}
