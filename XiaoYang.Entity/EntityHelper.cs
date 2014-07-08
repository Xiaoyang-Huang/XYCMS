using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityHelper {


        private Xy.Data.DataBase _db;
        private bool _applyToChild;
        private string _childQueryWhere;

        //private EntityType _type;
        //private XiaoYang.Entity.EntityAttribute[] _attrs;
        //private string[] _relatedTables;

        private EntityTypeCacheItem _cache;
        public EntityTypeCacheItem CacheInstance { get { return _cache; } }

        public EntityHelper(short inEntityID, bool inApplyToChild = false, Xy.Data.DataBase DB = null) {
            if (inEntityID <= 0) throw new Exception("Type ID is incorrect");
            _db = DB;
            if (_db == null) _db = new Xy.Data.DataBase();
            _cache = EntityTypeCache.GetInstance(inEntityID, _db);
            _applyToChild = inApplyToChild;
            _childQueryWhere = "[TypeID] = " + _cache.TypeInstance.ID + " and ";
            if (!_cache.TypeInstance.IsActive) throw new Exception("Type is not available");
        }

        public System.Data.DataTable Get(params long[] inEntityID) {
            if (inEntityID.Length == 0) return new System.Data.DataTable();

            //Xy.Data.Procedure _procedure = _cache.GetProcedure(_applyToChild, inEntityID);
            Xy.Data.Procedure _procedure;
            bool _isSingle = inEntityID.Length > 1 ? false : true;
            StringBuilder _command;
            if (_isSingle) {
                if (_applyToChild){
                    _procedure = new Xy.Data.Procedure("GetWithChild", string.Format(_cache.CacheString_Select_Command + "where {0} [EntityBase].[ID] = @ID", string.Empty), new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64));
                }else{
                    _procedure = new Xy.Data.Procedure("GetWithoutChild", string.Format(_cache.CacheString_Select_Command + "where {0} [EntityBase].[ID] = @ID", "[TypeID] = " + _cache.TypeInstance.ID + " and "), new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64));
                }
            } else {
                _command = new StringBuilder(_cache.CacheString_Select_Command);
                _command.AppendFormat("where {0} [EntityBase].[ID] in (", _applyToChild ? string.Empty : "[EntityBase].[TypeID] = " + _cache.TypeInstance.ID + " and ");
                for (int i = 0; i < inEntityID.Length; i++) {
                    if (i > 0) _command.Append(',');
                    _command.Append(inEntityID[i]);
                }
                _command.AppendLine(")");
                _procedure = new Xy.Data.Procedure("GetMultiEntity", _command.ToString());
            }
            if (inEntityID.Length == 1) _procedure.SetItem("ID", inEntityID[0]);
            return _procedure.InvokeProcedureFill(_db);
        }

        public EntityCollection GetList(System.Collections.Specialized.NameValueCollection nvc, ref int totalRowCount) {
            return GetList(nvc["Where"], Convert.ToInt32(nvc["PageIndex"]), Convert.ToInt32(nvc["PageSize"]), nvc["Order"], ref totalRowCount);
        }
        public EntityCollection GetList(string where, int pageIndex,int pageSize, string order, ref int totalRowCount) {
            Xy.Data.Procedure _getList = new Xy.Data.Procedure("Entity_SplitPage",
                new Xy.Data.ProcedureParameter[] { 
                    new Xy.Data.ProcedureParameter("Select", System.Data.DbType.String),
                    new Xy.Data.ProcedureParameter("TableName", System.Data.DbType.String),
                    new Xy.Data.ProcedureParameter("Where", System.Data.DbType.String),
                    new Xy.Data.ProcedureParameter("PageIndex", System.Data.DbType.Int32),
                    new Xy.Data.ProcedureParameter("PageSize", System.Data.DbType.Int32),
                    new Xy.Data.ProcedureParameter("Order", System.Data.DbType.String),
                    new Xy.Data.ProcedureParameter("OrderBy", System.Data.DbType.String),
                    new Xy.Data.ProcedureParameter("TotalRowCount", System.Data.DbType.Int32){ Direction = System.Data.ParameterDirection.InputOutput}
            });
            _getList.SetItem("Select", _cache.CacheString_Select_Field);
            _getList.SetItem("TableName", _cache.CacheString_Select_Table);
            _getList.SetItem("Where", "[TypeID] = " + _cache.TypeInstance.ID + (string.IsNullOrEmpty(where) ? string.Empty : " and " + where));
            _getList.SetItem("PageIndex", pageIndex);
            _getList.SetItem("PageSize", pageSize);
            _getList.SetItem("Order", "[EntityBase].[ID] desc");
            _getList.SetItem("OrderBy", string.IsNullOrEmpty(order) ? "[ID] desc" : order);
            _getList.SetItem("TotalRowCount", totalRowCount);
            System.Data.DataTable _dt = _getList.InvokeProcedureFill();
            totalRowCount = Convert.ToInt32(_getList.GetItem("TotalRowCount"));
            EntityCollection _ec = new EntityCollection(_dt, _cache.TypeInstance.ID);
            return _ec;
        }

        static System.Text.RegularExpressions.Regex _attrTest = new System.Text.RegularExpressions.Regex(@"^(?<name>[A-Za-z_]+)(?<id>\d+)$", System.Text.RegularExpressions.RegexOptions.Compiled);
        public int Edit(System.Collections.Specialized.NameValueCollection inValues, params long[] inEntityID) {
            if (inEntityID.Length == 0) throw new Exception("Entity ID not found");
            StringBuilder _command = new StringBuilder();
            bool _ifCode = false;
            foreach (long _currentID in inEntityID) {
                EntityBase _base = EntityBase.GetInstance(_currentID);
                _ifCode = false;
                foreach (string _valueKeys in inValues.AllKeys) {
                    string _valueName = _valueKeys, _valueID = string.Empty;
                    if (_attrTest.IsMatch(_valueKeys)) {
                        System.Text.RegularExpressions.Match _result = _attrTest.Match(_valueKeys);
                        _valueName = _result.Groups["name"].Value;
                        _valueID = _result.Groups["id"].Value;
                    }
                    foreach (XiaoYang.Entity.EntityAttribute _attr in _cache.Attributes) {
                        if (string.Compare(_attr.Key, _valueName) == 0) {
                            if (_attr.ID == -1) {
                                continue;
                            }
                            if (!_ifCode) {
                                _command.AppendLine(string.Format("if exists(select [ID] from [EntityBase] where {1} [ID] = {0})", _currentID, _applyToChild ? string.Empty : _childQueryWhere));
                                _command.AppendLine("begin");
                                _ifCode = true;
                            }
                            if (_attr.IsMultiple) {
                                if (_applyToChild) {
                                    _command.AppendLine(string.Format("if exists(select [ID] from [EntityBase] where {1} [ID] = {0})", _currentID, _childQueryWhere));
                                    _command.AppendLine("begin");
                                }
                                if (!string.IsNullOrEmpty(_valueID)) {
                                    _command.AppendLine(string.Format("Update [{0}] set [{1}] = '{2}' where [ID] = {3}", _attr.Table, _attr.Key, inValues[_valueKeys], _valueID));
                                } else {
                                    _command.AppendLine(string.Format("Delete from [{0}] where [EntityID] = {1}", _attr.Table, _currentID));
                                    foreach (string mulAttrValue in inValues[_valueKeys].Split(_attr.Split[0])) {
                                        _command.AppendLine(string.Format("Insert [{0}]([EntityID],[{1}])values({2},'{3}')", _attr.Table, _attr.Key, _currentID, mulAttrValue));
                                    }
                                }
                                if (_applyToChild) _command.AppendLine("end");
                            } else {
                                _command.AppendLine(string.Format("Update [{0}] set [{1}] = '{2}' where [EntityID] = {3}", _attr.Table, _attr.Key, inValues[_valueKeys], _currentID));
                            }
                        }
                    }
                }
                if (inValues["IsActive"] != null) _base.IsActive = Convert.ToBoolean(inValues["IsActive"]);
                _command.AppendLine(string.Format("Update [EntityBase] set [IsActive] = '{0}' where [ID] = {1}", _base.IsActive, _currentID));
                _command.AppendLine(string.Format("Update [EntityBase] set [UpdateTime] = GetDate() where [ID] = {0}", _currentID));
                if (_ifCode) _command.AppendLine("end");
            }
            if (_command.Length > 0) {
                Xy.Data.Procedure _procedure = new Xy.Data.Procedure("EditEntity" + _cache.TypeInstance.Key, _command.ToString());
                return _procedure.InvokeProcedure(_db);
            }
            return 0;
        }

        public long Add(System.Collections.Specialized.NameValueCollection inValues) {
            bool _isActive = false;
            if (inValues["IsActive"] != null && Convert.ToBoolean(inValues["IsActive"])) _isActive = true;
            StringBuilder _command = new StringBuilder();
            StringBuilder _field, _value, _commandFormMultiplyAttribute;
            bool _hasSimpleField;
            foreach (string _table in _cache.RelatedTables) {
                _hasSimpleField = false;
                _commandFormMultiplyAttribute = new StringBuilder();
                _field = new StringBuilder(); _field.Append("[EntityID]");
                _value = new StringBuilder(); _value.Append("@EntityID");
                foreach (EntityAttribute _attr in _cache.Attributes) {
                    if (_attr.ID == -1) continue;
                    if (string.Compare(_attr.Table, _table) == 0) {
                        string _tempValue = inValues[_attr.Key];
                        if (string.IsNullOrEmpty(_tempValue)) {
                            if (!string.IsNullOrEmpty(_attr.Default)) {
                                _tempValue = _attr.Default;
                            } else if (!_attr.IsNull) {
                                throw new Exception(string.Format("can not put Null on the field '{0}'", _attr.Key));
                            } else {
                                continue;
                            }
                        }
                        if (_attr.IsMultiple) {
                            foreach (string mulAttrValue in inValues[_attr.Key].Split(_attr.Split[0])) {
                                _commandFormMultiplyAttribute.AppendLine(string.Format("Insert [{0}]([EntityID],[{1}])values(@EntityID,'{2}')", _attr.Table, _attr.Key, mulAttrValue));
                            }
                        } else {
                            _hasSimpleField = true;
                            _field.AppendFormat(",[{0}]", _attr.Key);
                            _value.AppendFormat(",'{0}'", _tempValue);
                        }
                    }
                }
                if (_hasSimpleField) {
                    _command.AppendLine(string.Format("Insert into [{0}](", _table));
                    _command.AppendLine(_field.ToString());
                    _command.AppendLine(")values(");
                    _command.AppendLine(_value.ToString());
                    _command.AppendLine(")");
                }
                _command.Append(_commandFormMultiplyAttribute.ToString());
            }
            if (_command.Length > 0) {
                Xy.Data.Procedure _procedure = new Xy.Data.Procedure("AddEntity" + _cache.TypeInstance.Key, _command.ToString());
                _procedure.AddItem("EntityID", System.Data.DbType.Int64);
                _db.Open();
                _db.StartTransaction();
                try {
                    long _entityID = EntityBase.Add(_cache.TypeInstance.ID, _isActive, _db);
                    _procedure.SetItem("EntityID", _entityID);
                    _procedure.InvokeProcedure(_db);
                    _db.CommitTransaction();
                    return _entityID;
                } catch {
                    _db.RollbackTransation();
                    throw;
                } finally {
                    _db.Close();
                }
            }
            return 0;
        }

        public int Del(params long[] inEntityID) {
            StringBuilder _command = new StringBuilder();
            _command.AppendLine("Delete From [EntityBase] Where [ID] = @ID");
            foreach (string _tableName in _cache.RelatedTables) {
                _command.AppendLine(string.Format("Delete From [{0}] Where [ID] = @ID", _tableName));
            }
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("DelEntity" + _cache.TypeInstance.Key, _command.ToString());
            _procedure.AddItem("ID", System.Data.DbType.Int64);
            return _procedure.InvokeProcedure(_db);
        }
    }
}
