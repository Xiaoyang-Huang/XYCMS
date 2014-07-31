using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity.Cache {
    public class EntityCache {
        private EntityAttribute[] _attrs;
        public EntityAttribute[] Attributes { get { return _attrs; } }

        private bool _isNeedInput = false;
        public bool IsNeedInput { get { return _isNeedInput; } }

        public EntityCache(short inTypeID) {
            System.Data.DataTable _attrsTable = EntityAttribute.GetByTypeID(inTypeID);
            _attrs = new EntityAttribute[_attrsTable.Rows.Count];
            for (int i = 0; i < _attrsTable.Rows.Count; i++) {
                EntityAttribute _attr = new EntityAttribute();
                _attr.Fill(_attrsTable.Rows[i]);
                _attrs[i] = _attr;
                if (_attr.Display > 0) _isNeedInput = true;
            }
        }
    }

    public class EntityCacheCollection {
        private static Dictionary<short, EntityCache> _collection = new Dictionary<short, EntityCache>();

        public static EntityCache GetInstance(short inTypeID){
            if (!_collection.ContainsKey(inTypeID)) {
                EntityCache _temp = new EntityCache(inTypeID);
                if (!_collection.ContainsKey(inTypeID)) { _collection.Add(inTypeID, _temp); }
                return _temp;
            }
            return _collection[inTypeID];
        }

        public static void Clear() {
            _collection = new Dictionary<short, EntityCache>();
        }
    }
}
