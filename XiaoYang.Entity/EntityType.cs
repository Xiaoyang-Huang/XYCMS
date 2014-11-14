using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity{
    partial class EntityType{
        public static void RegisterEvents() {
            _procedures[R("EditActive")].BeforeInvoke += EditActive_BeforeInvoke;
            _procedures[R("EditActive")].AfterInvoke += EditActivee_AfterInvoke;
            _procedures[R("EditActive")].OnError += EditActive_onError;
        }

        private static void EditActive_onError(Exception exception, Xy.Data.DataBase DB) {
            DB.RollbackTransation();
            DB.Close();
        }
        private static void EditActivee_AfterInvoke(Xy.Data.ProcedureResult result, Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            DB.CommitTransaction();
            DB.Close();
        }

        private static void EditActive_BeforeInvoke(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            long ID = Convert.ToInt64(procedure.GetItem("ID"));
            bool IsActive = Convert.ToBoolean(procedure.GetItem("IsActive"));
            DB.Open();
            DB.StartTransaction();
            if (IsActive) {
                UpdateTable(ID, DB);
            } else {
                DropTable(ID, DB);
            }
        }

        

        public static string[] Check(long inTypeID, Xy.Data.DataBase inDB = null) {
            List<string> _errors = new List<string>();

            System.Data.DataTable _dataTable = EntityTable.GetListByTypeID(inTypeID, inDB);
            if (_dataTable.Rows.Count == 0) {
                _errors.Add("please add table into type.");
            } else {
                bool _hasMain = false;
                foreach (System.Data.DataRow _row in _dataTable.Rows) {
                    EntityTable _tempTable = new EntityTable();
                    _tempTable.Fill(_row);
                    if (_tempTable.Main) {
                        if (_hasMain) { 
                            _errors.Add("can not apply more than one Main table."); 
                        } else {
                            _hasMain = true;
                        }
                    }
                    bool _hasPrimary = false;
                    bool _hasForeign = false;
                    System.Data.DataTable _dataField = EntityField.GetListByTabldID(_tempTable.ID, inDB);
                    if (_dataField.Rows.Count == 0) {
                        _errors.Add(string.Format("please add field into table {0}.", _tempTable.Name));
                    } else { 
                        foreach(System.Data.DataRow _rowField in _dataField.Rows){
                            EntityField _tempField = new EntityField();
                            _tempField.Fill(_rowField);
                            if (_tempField.Primary) {
                                _hasPrimary = true;
                                if (_tempField.Null) _errors.Add(string.Format("table {0} field {1} can not set Null as it's a primary key", _tempTable.Name, _tempField.Name));
                            }
                            if (_tempField.Foreign) {
                                _hasForeign = true;
                                if (_tempField.Null) _errors.Add(string.Format("table {0} field {1} can not set Null as it's a foreign key", _tempTable.Name, _tempField.Name));
                            }
                        }
                        if (!_hasPrimary) _errors.Add(string.Format("table {0} did't have Primary key.", _tempTable.Name));
                        if (!_tempTable.Main) {
                            if (!_hasForeign) _errors.Add(string.Format("table {0} did't have Foreign key as it's not Main table.", _tempTable.Name));
                        } else {
                            if (_hasForeign) _errors.Add(string.Format("table {0} cannot have Foreign key as it's Main table.", _tempTable.Name));
                        }
                    }
                }
                if (!_hasMain) { _errors.Add("please select a table as Main table."); }
            }
            return _errors.ToArray();
        }

        public static void UpdateTable(long inTypeID, Xy.Data.DataBase inDB) {
            if(Check(inTypeID).Length > 0) throw new Exception("Update type failed while check process.");
            List<string> _commands = new List<string>();
            StringBuilder _logStatus = new StringBuilder();
            EntityType _type = EntityType.GetInstance(inTypeID);
            System.Data.DataTable _dataTable = EntityTable.GetListByTypeID(inTypeID);
            EntityTable _mainTable = null;
            EntityField _mainKey = null;
            bool _hasKeyTemp = false;
            for (int i = 0; i < _dataTable.Rows.Count; i++) {
                StringBuilder _cmd = new StringBuilder();
                EntityTable _tempTable = new EntityTable();
                _tempTable.Fill(_dataTable.Rows[i]);
                _tempTable.Key = string.Concat(EntityTable.TABLE_PREFIX, _tempTable.Key);
                if (_tempTable.Main) _mainTable = _tempTable;
                _cmd.AppendFormat("CREATE TABLE [{0}](", _tempTable.Key).AppendLine();
                System.Data.DataTable _dataField = EntityField.GetListByTabldID(_tempTable.ID);
                for (int j = 0; j < _dataField.Rows.Count; j++) {
                    EntityField _tempField = new EntityField();
                    _tempField.Fill(_dataField.Rows[j]);
                    _cmd.AppendFormat("[{0}] {1} {2} {3}{4}", 
                        _tempField.Name,
                        _tempField.SqlDeclare,
                        _tempField.Increase ? "IDENTITY(1,1)" : string.Empty, 
                        _tempField.Null ? "Null" : "NOT NULL",
                        j == _dataField.Rows.Count - 1 ? string.Empty : ",");
                    if (_tempField.Primary) {
                        _commands.Add(string.Format("ALTER TABLE [{0}] ADD CONSTRAINT PK_{0}_{1} PRIMARY KEY({1})", _tempTable.Key, _tempField.Key));
                        if (_tempTable.Main) _mainKey = _tempField;
                    }
                    if (_tempField.Foreign) {
                        if (_mainKey == null) {
                            _hasKeyTemp = true;
                            _commands.Add(string.Format("ALTER TABLE [{0}] ADD CONSTRAINT FK_{0}_{1} FOREIGN KEY({1}) REFERENCES [MainKeyHold]", _tempTable.Key, _tempField.Key));
                        } else {
                            _commands.Add(string.Format("ALTER TABLE [{0}] ADD CONSTRAINT FK_{0}_{1} FOREIGN KEY({1}) REFERENCES [{2}]({3})", _tempTable.Key, _tempField.Key, _mainTable.Key, _mainKey.Key));
                        }
                    }
                }
                _cmd.AppendLine(")");
                _commands.Insert(0, _cmd.ToString());
            }
            if (_hasKeyTemp) {
                string _keyHoldReplace = string.Format("[{0}]({1})", _mainTable.Key, _mainKey.Key);
                for (int i = 0; i < _commands.Count; i++) {
                    _commands[i].Replace("[MainKeyHold]", _keyHoldReplace);
                }
            }
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("CreateTable", string.Join(Environment.NewLine, _commands.ToArray()));
            _procedure.InvokeProcedure(inDB);
        }

        public static void DropTable(long inTypeID, Xy.Data.DataBase inDB) {
            StringBuilder _command = new StringBuilder();
            System.Data.DataTable _dataTables = EntityTable.GetListByTypeID(inTypeID, inDB);
            EntityTable _mainTable = null;
            for (int i = 0; i < _dataTables.Rows.Count; i++) {
                EntityTable _table = new EntityTable();
                _table.Fill(_dataTables.Rows[i]);
                _table.Key = string.Concat(EntityTable.TABLE_PREFIX, _table.Key);
                if (!_table.Main) {
                    _command.AppendFormat("DROP TABLE {0};", _table.Key);
                } else {
                    _mainTable = _table;
                }
            }
            _command.AppendFormat("DROP TABLE {0};", _mainTable.Key);
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("DropTable", _command.ToString());
            _procedure.InvokeProcedure(inDB);
        }
    }
}
