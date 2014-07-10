using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public partial class EntityAttribute : Xy.Data.IDataModel {

        private static Dictionary<long, string> _typeNames = null;

        public string Table { get {
            if (TypeID <= 0) {
                if (ID > 0) throw new Exception("Type ID is incorrect");
                if (TypeID == -1 && ID == -1) return "EntityBase";
            }
            if (_typeNames == null) _typeNames = new Dictionary<long, string>();
            if (!_typeNames.ContainsKey(ID)) {
                EntityType _type = EntityType.GetInstance(TypeID);
                if (_type == null) throw new Exception("Can not found type");
                _typeNames.Add(ID, "Entity_" + (IsMultiple ? _type.Key + "_" + Key : _type.Key));
            }
            return _typeNames[ID];
        } }

        static void RegisterEvents() {
        }
    }
}
