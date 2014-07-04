using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class PostEditor: Global.UserPage{
        public override void onGetRequest() {
            base.onGetRequest();
            if (!string.IsNullOrEmpty(Request.GroupString["id"])) {
                long ID = Convert.ToInt64(Request.GroupString["id"]);
                if (ID > 0) {
                    XiaoYang.Post.Post _item = XiaoYang.Post.Post.GetInstance(ID);
                    PageData.Add("Item", _item);
                }
            }
            PageData.Add("ConfigList", Xy.WebSetting.WebSettingCollection.ConfigNameList);
        }
    }
}
