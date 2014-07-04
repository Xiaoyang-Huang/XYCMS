using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.User {
    public partial class PowerList : Xy.Data.IDataModel {
        private static Dictionary<int, string> _powerList;

        public static void RegisterEvents() { }

        private static void InitCache() {
            _powerList = new Dictionary<int,string>();
            System.Data.DataTable _dt = GetAll();
            foreach (System.Data.DataRow _dr in _dt.Rows) {
                _powerList.Add(Convert.ToInt32(_dr["ID"]), _dr["Key"].ToString());
            }
        }

        public static int GetByKey(string inKey) {
            if (_powerList == null) {
                InitCache();
            }
            foreach (int _key in _powerList.Keys) {
                if (string.Compare(_powerList[_key], inKey, true) == 0) {
                    return _key;
                }
            }
            return 0;
        }

        public static void ClearCache() {
            _powerList = null;
        }
    }
}
