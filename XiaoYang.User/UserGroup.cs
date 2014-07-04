using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.User {
    public partial class UserGroup : Xy.Data.IDataModel {

        static void RegisterEvents() {
            _procedures[R("Del")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(Del_BeforeInvoke);
        }

        static void Del_BeforeInvoke(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            PowerShip.DelByGroup(Convert.ToInt32(produce.GetItem("ID")), DB);
        }

        private static Dictionary<int, string> _powerGroup;

        private static void InitCache() {
            _powerGroup = new Dictionary<int, string>();
            System.Data.DataTable _dt = GetAll();
            foreach (System.Data.DataRow _dr in _dt.Rows) {
                _powerGroup.Add(Convert.ToInt32(_dr["ID"]), _dr["Key"].ToString());
            }
        }

        public static int GetByKey(string inKey) {
            if (_powerGroup == null) {
                InitCache();
            }
            foreach (int _key in _powerGroup.Keys) {
                if (string.Compare(_powerGroup[_key], inKey, true) == 0) {
                    return _key;
                }
            }
            return 0;
        }

        public static void ClearCache() {
            _powerGroup = null;
        }

        public static int Add(string inName, string inKey, string inPowerList) {
            Xy.Data.Procedure item = UserGroup.GetProcedure(R("Add"));
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            return EditPowerList((int)item.InvokeProcedureResult(), inPowerList);
        }

        public static bool Edit(int inID, string inPowerList) {
            return EditPowerList(inID, inPowerList) > 0;
        }

        private static int EditPowerList(int inID, string inPowerList) {
            PowerShip.DelByGroup(inID);
            foreach (string _powerId in inPowerList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                PowerShip.Add(Convert.ToInt32(_powerId), inID);
            }
            return inID;
        }
    }
}
