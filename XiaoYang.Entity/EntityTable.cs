using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityTable : Xy.Data.IDataModel {

        public static readonly string TABLE_PREFIX = "Entity_";

        static void RegisterEvents() {
            _procedures[R("Del")].BeforeInvoke += Del_BeforeInvoke;
        }

        static void Del_BeforeInvoke(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            System.Data.DataTable _dt = EntityField.GetListByTabldID(Convert.ToInt64(procedure.GetItem("ID")), DB);
            for (int i = 0; i < _dt.Rows.Count; i++) {
                EntityField.Del(Convert.ToInt64(_dt.Rows[i]["ID"]), DB);
            }
        }

    }
}
