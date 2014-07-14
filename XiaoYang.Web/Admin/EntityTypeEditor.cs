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
            System.Data.DataTable _attributes = Entity.EntityAttribute.GetAllByTypeID(_id);
            PageData.Add("Attributes", _attributes);

            //System.Data.DataTable _baseKeys = _attributes.Clone();
            //System.Data.DataRow _baseKey;
            //_baseKey = _baseKeys.NewRow(); XiaoYang.Tools.Entity.EntityBase_ID.FillRow(_baseKey); _baseKeys.Rows.Add(_baseKey);
            //_baseKey = _baseKeys.NewRow(); XiaoYang.Tools.Entity.EntityBase_TypeID.FillRow(_baseKey); _baseKeys.Rows.Add(_baseKey);
            //_baseKey = _baseKeys.NewRow(); XiaoYang.Tools.Entity.EntityBase_IsActive.FillRow(_baseKey); _baseKeys.Rows.Add(_baseKey);
            //_baseKey = _baseKeys.NewRow(); XiaoYang.Tools.Entity.EntityBase_UpdateTime.FillRow(_baseKey); _baseKeys.Rows.Add(_baseKey);
            //PageData.Add("BaseKeys", _baseKeys);
        }
    }
}
