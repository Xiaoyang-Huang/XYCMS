using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public partial class EntityAttribute : Xy.Data.IDataModel, IEntityAttribute {

        public string Table { get {
            throw new NotImplementedException();
        } }

        static void RegisterEvents() {
            _procedures[R("Add")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_BeforeAdd);
        }

        static void EntityAttribute_BeforeAdd(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short _typeId = Convert.ToInt16(procedure.GetItem("TypeID"));
            string _key = procedure.GetItem("Key").ToString();
            System.Data.DataTable _dt = GetAllByTypeID(_typeId, DB);
            foreach (System.Data.DataRow _row in _dt.Rows) {
                if (string.Compare(_row["Key"].ToString(), _key, true) == 0) throw new Exception(_key + " was exist in the type chain");
            }
        }

        public static System.Data.DataTable GetAllByTypeID(short inTypeID, Xy.Data.DataBase DB = null) {
            EntityType _type = EntityType.GetInstance(inTypeID);
            System.Data.DataTable _dt = GetByTypeID(inTypeID, DB);
            if (_type.ParentTypeID > 0) {
                System.Data.DataTable _parentAttrs = GetAllByTypeID(_type.ParentTypeID);
                foreach (System.Data.DataRow _row in _parentAttrs.Rows) {
                    _dt.ImportRow(_row);
                }
            }
            return _dt;
        }
        public static System.Data.DataTable GetAllByTypeID(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
            return GetByTypeID(Convert.ToInt16(values["TypeID"]), DB);
        }


        public IEntityAttributeHandle Handle {
            get { throw new NotImplementedException(); }
        }
    }
}
