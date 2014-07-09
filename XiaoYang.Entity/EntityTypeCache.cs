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

        private bool _isWebRelated;
        public bool IsWebRelated { get { return _isWebRelated; } }

        #region Cache Strings
        private string _cacheString_Select_Command = string.Empty;
        public string CacheString_Select_Command {
            get {
                if (string.IsNullOrEmpty(_cacheString_Select_Command)) {
                    _cacheString_Select_Command = "select " + CacheString_Select_Field + "from " + CacheString_Select_Table; ;
                }
                return _cacheString_Select_Command;
            }
        }
        private string _cacheString_Select_Field = string.Empty;
        public string CacheString_Select_Field {
            get {
                if (string.IsNullOrEmpty(_cacheString_Select_Field)) {
                    StringBuilder _command = new StringBuilder();
                    for (int i = 0; i < _attrs.Length; i++) {
                        EntityAttribute _attr = _attrs[i];
                        if (i > 0) _command.Append(", ");
                        if (_attr.IsMultiple) {
                            _command.AppendLine(string.Format("[{0}].[ID] as '{1}ID', [{0}].[{1}]", _attr.Table, _attr.Key));
                        } else {
                            _command.AppendLine(string.Format("[{0}].[{1}]", _attr.Table, _attr.Key));
                        }
                    }
                    _cacheString_Select_Field = _command.ToString();
                }
                return _cacheString_Select_Field;
            }
        }
        private string _cacheString_Select_Table = string.Empty;
        public string CacheString_Select_Table {
            get {
                if (string.IsNullOrEmpty(_cacheString_Select_Table)) {
                    StringBuilder _command = new StringBuilder();
                    for (int i = 0; i < _relatedTables.Length; i++) {
                        if (i == 0) {
                            _command.AppendLine(string.Format("[{0}]", _relatedTables[i]));
                        } else {
                            _command.AppendLine(string.Format("left join [{0}] on [{0}].[EntityID] = [EntityBase].[ID]", _relatedTables[i]));
                        }
                    }
                    _cacheString_Select_Table = _command.ToString();
                }
                return _cacheString_Select_Table;
            }
        }
        #endregion

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
                        _item = XiaoYang.Entity.EntityAttribute.EntityBase_ID;
                        break;
                    case 1:
                        _item = XiaoYang.Entity.EntityAttribute.EntityBase_TypeID;
                        break;
                    case 2:
                        _item = XiaoYang.Entity.EntityAttribute.EntityBase_IsActive;
                        break;
                    case 3:
                        _item = XiaoYang.Entity.EntityAttribute.EntityBase_UpdateTime;
                        break;
                }
                _attrs[i] = _item;
                _attrKeys[i] = _item.Key;
                _attrNames[i] = _item.Name;
                _attrDict.Add(_item.Key, i);
            }
            for (int i = 0; i < _attrsTable.Rows.Count; i++) {
                EntityAttribute _item = new EntityAttribute();
                _item.Fill(_attrsTable.Rows[i]);
                if (_item.IsMultiple) _hasMuli = true;
                if (!_tempRelatedTables.Contains(_item.Table)) _tempRelatedTables.Add(_item.Table);
                _attrs[i + 4] = _item;
                _attrKeys[i + 4] = _item.Key;
                _attrNames[i + 4] = _item.Name;
                _attrDict.Add(_item.Key, i + 4);
            }
            _relatedTables = _tempRelatedTables.ToArray();
        }
    }

}
