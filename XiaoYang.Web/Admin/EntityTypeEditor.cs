using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityTypeEditor : Global.UserPage{
        public override void ValidateUrl() {
            short _id = Convert.ToInt16(Request.GroupString["id"]);
            if (_id > 0) {
                XiaoYang.Entity.EntityType _currentEntityType = XiaoYang.Entity.EntityType.GetInstance(_id);
                PageData.AddXyDataModel("Item", _currentEntityType);
            }
            PageData.Add("Attributes", Entity.EntityAttribute.GetByTypeID(_id));
        }
    }
}
