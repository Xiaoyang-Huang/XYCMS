using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public partial class EntityType : Xy.Data.IDataModel {
        static void RegisterEvents() {
            _procedures[R("EditActive")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityType_ValidateEditActive);
            _procedures[R("EditActive")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityType_BeforeEditActive);
        }

        static void EntityType_ValidateEditActive(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short _typeID = Convert.ToInt16(procedure.GetItem("ID"));
            bool _isActive = Convert.ToBoolean(procedure.GetItem("IsActive"));
            EntityType _type = EntityType.GetInstance(_typeID, DB);
            if (_isActive) {
                if (_type.ParentTypeID > 0) {
                    if (!EntityType.GetInstance(_type.ParentTypeID).IsActive) throw new Exception("Superior type not actived");
                }
            }
        }

        static void EntityType_BeforeEditActive(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {

        }
    }
}
