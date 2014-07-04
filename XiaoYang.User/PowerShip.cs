using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.User {
    public partial class PowerShip : Xy.Data.IDataModel {

        static void RegisterEvents() { }

        private static System.Data.DataTable _powerShipTable;

        public static System.Data.DataTable GetByGroup(int inUserGroup) {
            if (_powerShipTable == null) {
                _powerShipTable = GetAll();
            }
            System.Data.DataView dv = new System.Data.DataView(_powerShipTable);
            dv.RowFilter = string.Format("UserGroup = {0}", inUserGroup);
            return dv.ToTable();
        }

        public static void ClearCache() {
            _powerShipTable = null;
        }
    }
}
