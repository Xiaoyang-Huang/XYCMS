using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public interface IHandler {
        void Init(long inTypeID, Xy.Data.DataBase DB);
        Object Add(System.Collections.Specialized.NameValueCollection inForm);
        int Del(System.Collections.Specialized.NameValueCollection inForm);
        Entity Get(System.Collections.Specialized.NameValueCollection inForm);
        int Update(System.Collections.Specialized.NameValueCollection inForm);
        EntityCollection GetList(System.Collections.Specialized.NameValueCollection inForm, ref int TotalCount);
        //System.Data.DataTable GetList(int inPageIndex, int inPageSize, string inWhere, string inOrder, out int inTotalCount);
    }

    public class DefaultHandler : IHandler {

        private Cache _cache;
        private Xy.Data.DataBase _db;

        public DefaultHandler() { }

        public void Init(long inTypeID, Xy.Data.DataBase DB) {
            _cache = CacheManager.Get(inTypeID);
            _db = DB;
        }

        public Object Add(System.Collections.Specialized.NameValueCollection inForm){
            return Add(inForm[_cache.Type.Key]);
        }
        private Object Add(string inEntityXml) {
            System.Xml.XPath.XPathNavigator _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(inEntityXml)).CreateNavigator();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(8);
            StringBuilder _command = new StringBuilder();
            int _fieldPoint = 0;
            for (int i = 0; i < _cache.TableList.Count; i++) {
                XiaoYang.Entity.EntityTable _table = _cache.TableList[i];
                System.Xml.XPath.XPathNodeIterator _tableIter = _xml.Select(_cache.Type.Key + "/" + _table.Key);
                if (_tableIter.Count > 0) {
                    int _tableFieldStart = _fieldPoint;
                    while (_tableIter.MoveNext()) {
                        bool _edit = false;
                        int _commandStart = _command.Length;
                        _command.Append("Insert into [").Append(_table.Key).AppendLine("](").Append("\t");
                        int _parameterOffset = _command.Length;
                        _command.AppendLine().AppendLine(")VALUES(").Append("\t");
                        int _valueOffset = _command.Length;
                        _command.AppendLine().AppendLine(");");
                        if (_table.Main) _command.AppendLine("set @PrimaryKey = SCOPE_IDENTITY();");
                        int _commandLength = _command.Length;
                        _fieldPoint = _tableFieldStart;
                        for (int j = _fieldPoint; j < _cache.FieldList.Count; j = _fieldPoint) {
                            XiaoYang.Entity.EntityField _field = _cache.FieldList[j];
                            if (_field.Table != _table) break;
                            if (_field.Increase) {
                                _fieldPoint++; continue;
                            }
                            if (_field.Foreign) {
                                _command.Insert(_parameterOffset, string.Format("{1}[{0}]", _field.Key, _edit ? "," : string.Empty));
                                _parameterOffset += _command.Length - _commandLength; _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _command.Insert(_valueOffset, string.Format("{0}@PrimaryKey", _edit ? "," : string.Empty));
                                _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _fieldPoint++; _edit = true; continue;
                            }
                            System.Xml.XPath.XPathNavigator _value = _tableIter.Current.SelectSingleNode(_field.Key);
                            if (_value != null) {
                                _command.Insert(_parameterOffset, string.Format("{1}[{0}]", _field.Key, _edit ? "," : string.Empty));
                                _parameterOffset += _command.Length - _commandLength; _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _command.Insert(_valueOffset, string.Format("{1}'{0}'", _value.Value, _edit ? "," : string.Empty));
                                _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _edit = true;
                            }
                            _fieldPoint++;
                        }
                    }
                }
            }
            Xy.Data.ProcedureParameter _primaryKey = new Xy.Data.ProcedureParameter("PrimaryKey", System.Data.DbType.Int64);
            _primaryKey.Value = 0;
            _primaryKey.Direction = System.Data.ParameterDirection.InputOutput;
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("AddNew", _command.ToString());
            _procedure.AddItem(_primaryKey);
            _procedure.InvokeProcedure();
            return _procedure.GetItem("PrimaryKey");
        }

        public int Del(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public Entity Get(System.Collections.Specialized.NameValueCollection inForm) {
            return Get(inForm[_cache.PrimaryField.Key]);
        }
        public Entity Get(string inPrimaryKey) {
            StringBuilder _command = new StringBuilder();
            StringBuilder _xml = new StringBuilder();
            _command.Append("Declare @PrimaryKey ").AppendLine(_cache.PrimaryField.SqlDeclare);
            _command.AppendFormat("Set @PrimaryKey = '{0}'", inPrimaryKey).AppendLine();
            for (int i = 0; i < _cache.FieldList.Count; i++) {
                EntityField _field = _cache.FieldList[i];
                if (_field.Primary && _field.Table.Main || _field.Foreign)
                    _command.AppendFormat("select * from [{0}] where [{1}] = @PrimaryKey", _field.Table.Key, _field.Key).AppendLine();
            }
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("GetEntity", _command.ToString());
            System.Data.DataSet _ds = _procedure.InvokeProcedureFillSet(_db);
            _xml.AppendFormat("<{0}>", _cache.Type.Key);
            for (int i = 0; i < _cache.TableList.Count; i++) {
                EntityTable _entityTable = _cache.TableList[i];
                System.Data.DataTable _table = _ds.Tables[i];
                if (_table.Rows.Count == 0) continue;
                for (int j = 0; j < _table.Rows.Count; j++) {
                    _xml.AppendFormat("<{0}>", _entityTable.Key);
                    for (int k = 0; k < _table.Columns.Count; k++) {
                        if (_table.Rows[j][k] == DBNull.Value) continue;
                        _xml.AppendFormat(@"<{0}><![CDATA[{1}]]></{0}>", _table.Columns[k].ColumnName, Convert.ToString(_table.Rows[j][k]));
                    }
                    _xml.AppendFormat("</{0}>", _entityTable.Key);
                }
            }
            _xml.AppendFormat("</{0}>", _cache.Type.Key);
            Console.WriteLine(_xml.ToString());
            return new Entity(_cache, _xml.ToString());
        }

        public int Update(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public EntityCollection GetList(System.Collections.Specialized.NameValueCollection inForm, ref int TotalCount) {
            int _pageIndex = Convert.ToInt32(inForm["PageIndex"]);
            int _pageSize = Convert.ToInt32(inForm["PageSize"]);
            string _where = inForm["Where"];
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("SplitPage");
            //@Select nvarchar(2048),
            //@TableName nvarchar(1024),
            //@Where nvarchar(2048),
            //@Order nvarchar(2048) = null,
            //@PageIndex int,
            //@PageSize int,
            //@TotalRowCount BIGINT=-1 output,
            //@OrderBy nvarchar(2048)
            _procedure.AddItem(new Xy.Data.ProcedureParameter("Select", System.Data.DbType.String, string.Empty, _cache.PrimaryField.Key));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("TableName", System.Data.DbType.String, string.Empty, _cache.MainTable.Key));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("Where", System.Data.DbType.String, string.Empty, _where));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("Order", System.Data.DbType.String, string.Empty, _cache.PrimaryField.Key + " Desc"));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("PageIndex", System.Data.DbType.Int32, string.Empty, _pageIndex));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("PageSize", System.Data.DbType.Int32, string.Empty, _pageSize));
            _procedure.AddItem(new Xy.Data.ProcedureParameter("TotalRowCount", System.Data.DbType.Int64, string.Empty, TotalCount) { Direction = System.Data.ParameterDirection.Output });
            _procedure.AddItem(new Xy.Data.ProcedureParameter("OrderBy", System.Data.DbType.String, string.Empty, _cache.PrimaryField.Key + " Desc"));
            System.Data.DataTable _result = _procedure.InvokeProcedureFill(_db);
            TotalCount = Convert.ToInt32(_procedure.GetItem("TotalRowCount"));

            EntityCollection _ec = new EntityCollection(_cache);
            for (int i = 0; i < _result.Rows.Count; i++) {
                string _primaryID = _result.Rows[i][_cache.PrimaryField.Key].ToString();
                Entity _tempEntity = Get(_primaryID);
                _ec.Add(_tempEntity);
            }
            return _ec;
        }


    }
}
