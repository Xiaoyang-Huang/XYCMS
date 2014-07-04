using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityTypeCache {
        private static Dictionary<int, EntityTypeCacheItem> _store = new Dictionary<int, EntityTypeCacheItem>();

        public static EntityTypeCacheItem GetInstance(short TypeID, Xy.Data.DataBase DB = null) {
            if (!_store.ContainsKey(TypeID)) {
                _store.Add(TypeID, new EntityTypeCacheItem(TypeID, DB));
            }
            return _store[TypeID];
        }
    }

    public class EntityTypeCacheItem {

        private EntityType _type;
        public XiaoYang.Entity.EntityType TypeInstance { get { return _type; } }

        private XiaoYang.Entity.EntityAttribute[] _attrs;
        public XiaoYang.Entity.EntityAttribute[] Attributes { get { return _attrs; } }

        private string[] _relatedTables;
        public string[] RelatedTables { get { return _relatedTables; } }

        private string[] _attrKeys;
        public string[] AttributeKeys { get { return _attrKeys; } }

        private string[] _attrNames;
        public string[] AttributeNames { get { return _attrNames; } }

        private bool _hasMuli;
        public bool HasMulitple { get { return _hasMuli; } }

        

        private Xy.Data.Procedure _getSingleProcedureApplyToChild = null;
        private Xy.Data.Procedure _getSingleProcedureNotApplyToChild = null;
        private string _getCommandCache = string.Empty;
        private string _getFieldCache = string.Empty;
        private string _getTableCache = string.Empty;
        private string getSelectCommand() {
            StringBuilder _command = new StringBuilder();
            //_command.AppendLine("select ");
            _command = new StringBuilder();
            for (int i = 0; i < _attrs.Length; i++) {
                EntityAttribute _attr = _attrs[i];
                if (i > 0) _command.Append(", ");
                if (_attr.IsMultiple) {
                    _command.AppendLine(string.Format("[{0}].[ID] as '{1}ID', [{0}].[{1}]", _attr.Table, _attr.Key));
                } else {
                    _command.AppendLine(string.Format("[{0}].[{1}]", _attr.Table, _attr.Key));
                }
            }
            _getFieldCache = _command.ToString();
            _command = new StringBuilder();
            for (int i = 0; i < _relatedTables.Length; i++) {
                if (i == 0) {
                    _command.AppendLine(string.Format("[{0}]", _relatedTables[i]));
                } else {
                    _command.AppendLine(string.Format("left join [{0}] on [{0}].[EntityID] = [EntityBase].[ID]", _relatedTables[i]));
                }
            }
            _getTableCache = _command.ToString();
            return "select " + _getFieldCache + "from " + _getTableCache;
        }
        internal Xy.Data.Procedure GetProcedure(bool inApplyToChild, params long[] inEntityID) {
            bool _isSingle = inEntityID.Length > 1 ? false : true;
            if (inApplyToChild && _isSingle && _getSingleProcedureApplyToChild != null) return _getSingleProcedureApplyToChild;
            if (!inApplyToChild && _isSingle && _getSingleProcedureNotApplyToChild != null) return _getSingleProcedureNotApplyToChild;
            StringBuilder _command;
            if (_isSingle) {
                _getSingleProcedureApplyToChild = new Xy.Data.Procedure("GetWithChild", string.Format(_getCommandCache + "where {0} [EntityBase].[ID] = @ID", string.Empty), new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64));
                _getSingleProcedureNotApplyToChild = new Xy.Data.Procedure("GetWithoutChild", string.Format(_getCommandCache + "where {0} [EntityBase].[ID] = @ID", "[TypeID] = " + _type.ID + " and "), new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64));
                if (inApplyToChild) return _getSingleProcedureApplyToChild;
                else return _getSingleProcedureNotApplyToChild;
            } else {
                _command = new StringBuilder(_getCommandCache);
                _command.AppendFormat("where {0} [EntityBase].[ID] in (", inApplyToChild ? string.Empty : "[EntityBase].[TypeID] = " + _type.ID + " and ");
                for (int i = 0; i < inEntityID.Length; i++) {
                    if (i > 0) _command.Append(',');
                    _command.Append(inEntityID[i]);
                }
                _command.AppendLine(")");
                return new Xy.Data.Procedure("GetMultiEntity", _command.ToString());
            }
        }
        internal System.Data.DataTable GetListProcedure(string where, int pageIndex, int pageSize, string order, ref int totalRowCount) {
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
            _getList.SetItem("Select", _getFieldCache);
            _getList.SetItem("TableName", _getTableCache);
            _getList.SetItem("Where", "[TypeID] = " + _type.ID + (string.IsNullOrEmpty(where) ? string.Empty : " and " + where));
            _getList.SetItem("PageIndex", pageIndex);
            _getList.SetItem("PageSize", pageSize);
            _getList.SetItem("Order", "[EntityBase].[ID] desc");
            _getList.SetItem("OrderBy", string.IsNullOrEmpty(order) ? "[ID] desc" : order);
            _getList.SetItem("TotalRowCount", totalRowCount);
            System.Data.DataTable _dt = _getList.InvokeProcedureFill();
            totalRowCount = Convert.ToInt32(_getList.GetItem("TotalRowCount"));
            return _dt;
        }

        private Dictionary<string, int> _attrDict = new Dictionary<string, int>();
        public XiaoYang.Entity.EntityAttribute GetAttribute(string inKey) {
            return _attrs[_attrDict[inKey]];
        }

        internal EntityTypeCacheItem(short TypeID, Xy.Data.DataBase DB = null) {
            _hasMuli = false;
            _type = EntityType.GetInstance(TypeID);
            System.Data.DataTable _attrsTable = EntityAttribute.GetByTypeID(TypeID, DB);
            _attrs = new EntityAttribute[_attrsTable.Rows.Count + 4];
            _attrKeys = new string[_attrsTable.Rows.Count + 4];
            _attrNames = new string[_attrsTable.Rows.Count + 4];
            List<string> _tempRelatedTables = new List<string>();
            _tempRelatedTables.Add("EntityBase");
            for (int i = 0; i < 4; i++) {
                EntityAttribute _item = null;
                switch (i) {
                    case 0:
                        _item = new EntityAttribute() {
                            ID = -1, TypeID = -1, Name = "ID", Key = "ID",
                            Type = "System.Data.DbType.Int64|", Default = string.Empty,
                            IsMultiple = false, IsNull = false, Split = string.Empty, Description = "Defaut attirbute",
                            Display = -1
                        };
                        break;
                    case 1:
                        _item = new EntityAttribute() {
                            ID = -1, TypeID = -1, Name = "TypeID", Key = "TypeID",
                            Type = "System.Data.DbType.Int16|", Default = string.Empty,
                            IsMultiple = false, IsNull = false, Split = string.Empty, Description = "Defaut attirbute",
                            Display = -1
                        };
                        break;
                    case 2:
                        _item = new EntityAttribute() {
                            ID = -1, TypeID = -1, Name = "IsActive", Key = "IsActive",
                            Type = "System.Data.DbType.Boolean|", Default = string.Empty,
                            IsMultiple = false, IsNull = false, Split = string.Empty, Description = "Defaut attirbute",
                            Display = -1
                        };
                        break;
                    case 3:
                        _item = new EntityAttribute() {
                            ID = -1, TypeID = -1, Name = "UpdateTime", Key = "UpdateTime",
                            Type = "System.Data.DbType.DateTime|", Default = string.Empty,
                            IsMultiple = false, IsNull = false, Split = string.Empty, Description = "Defaut attirbute",
                            Display = -1
                        };
                        break;
                }
                _attrs[i] = _item;
                _attrKeys[i] = _item.Key;
                _attrNames[i] = _item.Name;
                _attrDict.Add(_item.Key, i);
            }
            for (int i = 0; i < _attrsTable.Rows.Count; i++) {
                EntityAttribute _item = new EntityAttribute();
                _item.Fill(_attrsTable.Rows[i]); //descrease make more comfortable and read-able sense
                if (_item.IsMultiple) _hasMuli = true;
                if (!_tempRelatedTables.Contains(_item.Table)) _tempRelatedTables.Add(_item.Table);
                _attrs[i + 4] = _item;
                _attrKeys[i + 4] = _item.Key;
                _attrNames[i + 4] = _item.Name;
                _attrDict.Add(_item.Key, i + 4);
            }
            _relatedTables = _tempRelatedTables.ToArray();
            _getCommandCache = getSelectCommand();
        }
    }

}
