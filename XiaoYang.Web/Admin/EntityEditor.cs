using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityEditor : Global.UserPage {

        public override void ValidateUrl() {
            base.ValidateUrl();
            short _typeID = 0;
            switch (Request.GroupString["type"]) {
                case "add":
                    _typeID = Convert.ToInt16(Request.GroupString["id"]);
                    break;
                case "edit":
                    long _entityID = Convert.ToInt64(Request.GroupString["id"]);
                    XiaoYang.Entity.EntityBase _base = XiaoYang.Entity.EntityBase.GetInstance(_entityID);
                    if (_base == null) throw new Exception("can not found entity");
                    _typeID = _base.TypeID;
                    XiaoYang.Entity.EntityHelper _helper = new Entity.EntityHelper(_base.TypeID);
                    XiaoYang.Entity.Entity _entity = new Entity.EntityCollection(_helper.Get(_entityID), _base.TypeID)[0];
                    PageData.AddXyDataModel("Entity", _entity);
                    break;
            }
            System.Data.DataTable _typeChain = Entity.EntityType.Get(_typeID);
            if (_typeChain.Rows.Count == 0) throw new Exception("can not found entity type");
            Entity.EntityType _type = new Entity.EntityType();
            _type.Fill(_typeChain.Rows[0]);
            while (_type.ParentTypeID > 0) {
                _type = Entity.EntityType.GetInstance(_type.ParentTypeID);
                System.Data.DataRow _newRow = _typeChain.NewRow();
                foreach (string _columnName in _type.GetAttributesName()) {
                    _newRow[_columnName] = _type.GetAttributesValue(_columnName);
                }
                _typeChain.Rows.Add(_newRow);
            }
            _typeChain.DefaultView.Sort = "ID ASC";
            _typeChain = _typeChain.DefaultView.ToTable();
            PageData.AddXyDataModel("Type", _type);
            PageData.Add("TypeChain", _typeChain);
        }
    }
}
