using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class EntityList: Global.UserPage {
        public override void ValidateUrl() {
            base.ValidateUrl();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(Convert.ToInt64(Request.GroupString["typeID"]));
            System.Data.DataTable _dt = new System.Data.DataTable();
            _dt.Columns.Add("ID", typeof(string));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Key", typeof(string));
            _dt.Columns.Add("TableID", typeof(string));
            _dt.Columns.Add("TableName", typeof(string));
            _dt.Columns.Add("TableKey", typeof(string));
            _dt.Columns.Add("ToggleKey", typeof(string));
            _dt.Columns.Add("IsShow", typeof(bool));
            _dt.Columns.Add("Column", typeof(int));
            for (int i = 0; i < _cache.FieldList.Count; i++) {
                System.Data.DataRow _row = _dt.NewRow();
                XiaoYang.Entity.EntityField _field = _cache.FieldList[i];
                _row["ID"] = _field.ID;
                _row["Name"] = _field.Name;
                _row["Key"] = _field.Key;
                _row["TableID"] = _field.Table.ID;
                _row["TableName"] = _field.Table.Name;
                _row["TableKey"] = _field.Table.Key;
                if (!string.IsNullOrEmpty(Request.Form["Filter"])) {
                    _row["IsShow"] = Request.Form["Filter"].IndexOf(_field.Table.Key + "." + _field.Key + ",") > -1;
                } else {
                    _row["IsShow"] = false;
                }
                _row["ToggleKey"] = _field.Key + "." + _field.Table.Key;
                _row["Column"] = 0;
                _dt.Rows.Add(_row);
            }

            int _lastRow = 0;
            int _showRowCount = 0;
            string _currentTable = string.Empty;
            for (int i = 0; i < _dt.Rows.Count; i++) {
                System.Data.DataRow _row = _dt.Rows[i];
                if (string.IsNullOrEmpty(_currentTable)) _currentTable = _row["TableKey"].ToString();

                if (string.Compare(_row["TableKey"].ToString(), _currentTable) != 0) {
                    _currentTable = _row["TableKey"].ToString();
                    _dt.Rows[_lastRow]["Column"] = _showRowCount;
                    _showRowCount = 0;
                }

                if (Convert.ToBoolean(_row["IsShow"])) {
                    _showRowCount++;
                    _lastRow = i;
                }

                if (i == _dt.Rows.Count - 1) _dt.Rows[_lastRow]["Column"] = _showRowCount;
            }
            PageData.Add("Attributes", _dt);
        }
    }
}
