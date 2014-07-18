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
                    //long _entityID = Convert.ToInt64(Request.GroupString["id"]);
                    //XiaoYang.Entity.EntityBase _base = XiaoYang.Entity.EntityBase.GetInstance(_entityID);
                    //if (_base == null) throw new Exception("can not found entity");
                    //_typeID = _base.TypeID;
                    //XiaoYang.Entity.EntityHelper _helper = new Entity.EntityHelper(_base.TypeID);
                    //XiaoYang.Entity.Entity _entity = new Entity.EntityCollection(_helper.Get(_entityID), _base.TypeID)[0];
                    //PageData.AddXyDataModel("Entity", _entity);
                    break;
            }
            System.Data.DataTable _typeChain = new System.Data.DataTable();
            _typeChain.Columns.Add("ID", typeof(Int16));
            _typeChain.Columns.Add("Name", typeof(String));
            _typeChain.Columns.Add("Key", typeof(String));
            _typeChain.Columns.Add("IsDisplay", typeof(Boolean));
            _typeChain.Columns.Add("IsActive", typeof(Boolean));
            _typeChain.Columns.Add("UpdateTime", typeof(DateTime));
            _typeChain.Columns.Add("Description", typeof(String));
            _typeChain.Columns.Add("ParentTypeID", typeof(Int16));
            _typeChain.Columns.Add("Handle", typeof(String));
            _typeChain.Columns.Add("EditPage", typeof(String));
            Entity.EntityType _type = Entity.EntityType.GetInstance(_typeID);
            if (_type == null) throw new Exception("can not found entity type");
            PageData.AddXyDataModel("Type", _type);
            while (_type != null) {
                System.Data.DataRow _row = _typeChain.NewRow();
                _type.FillRow(_row);

                Xy.Web.Page.PageAbstract _editPage = _type.HandleInstance.GetEditPageClass();
                _editPage.Init(this, WebSetting);
                _editPage.SetNewContainer(new Xy.Web.HTMLContainer(WebSetting.Encoding));
                _editPage.Handle("editPage", string.Empty, true, true);
                _row["EditPage"] = _editPage.HTMLContainer.ToString();

                _typeChain.Rows.Add(_row);

                _type = Entity.EntityType.GetInstance(_type.ParentTypeID);
            }
            _typeChain.DefaultView.Sort = "ID ASC";
            _typeChain = _typeChain.DefaultView.ToTable();
            PageData.Add("TypeChain", _typeChain);
        }
    }
}
