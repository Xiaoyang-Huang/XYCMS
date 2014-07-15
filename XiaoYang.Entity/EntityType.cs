using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public partial class EntityType : Xy.Data.IDataModel {
        static void RegisterEvents() {
            _procedures[R("EditActive")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityType_ValidateEditActive);
            _procedures[R("EditActive")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityType_BeforeEditActive);
        }

        static void EntityType_ValidateEditActive(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short _typeID = Convert.ToInt16(procedure.GetItem("ID"));
            bool _isActive = Convert.ToBoolean(procedure.GetItem("IsActive"));
            EntityType _type = EntityType.GetInstance(_typeID, DB);
            if (_isActive) {
                if (_type.ParentTypeID > 0) {
                    if (!EntityType.GetInstance(_type.ParentTypeID).IsActive) throw new Exception("Superior type not actived");
                }
            } else {
                System.Data.DataTable _dt = EntityType.GetChildType(_typeID);
                if (_dt.Rows.Count > 0) throw new Exception("Child type is actived");
            }
        }

        static void EntityType_BeforeEditActive(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short _typeID = Convert.ToInt16(procedure.GetItem("ID"));
            bool _isActive = Convert.ToBoolean(procedure.GetItem("IsActive"));
            EntityType _type = EntityType.GetInstance(_typeID, DB);
            System.Data.DataTable _attrs = EntityAttribute.GetByTypeID(_typeID);
            StringBuilder _command = new StringBuilder();
            StringBuilder _commandAfter = new StringBuilder();
            bool _hasPrimaryKey = false;
            if (_isActive) {
                _command.AppendFormat("CREATE TABLE [Entity_{0}](", _type.Key).AppendLine();
                for (int i = 0; i < _attrs.Rows.Count; i++) {
                    EntityAttribute _attr = new EntityAttribute();
                    _attr.Fill(_attrs.Rows[i]);
                    _command.AppendFormat("[{0}] {1} ", _attr.Key, Tools.Entity.GetTypeName(_attr.Type));
                    _command.Append(_attr.IsIncrease ? "IDENTITY(1,1) " : string.Empty);
                    _command.Append(_attr.IsNull ? "NULL" : "NOT NULL");
                    _command.AppendLine(",");
                    if (_attr.IsPrimary) {
                        _commandAfter.AppendFormat("ALTER TABLE [Entity_{0}] ADD CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([{1}] ASC)", _type.Key, _attr.Key).AppendLine();
                        if (_hasPrimaryKey) throw new Exception("primary key existed");
                        _hasPrimaryKey = true;
                    }
                    if (_attr.FKID > 0) {
                        EntityAttribute _FKattr = EntityAttribute.GetInstance(_attr.FKID);
                        EntityType _FKtype = EntityType.GetInstance(_FKattr.TypeID);
                        _commandAfter.AppendFormat("ALTER TABLE [Entity_{0}] ADD CONSTRAINT [FK_{0}_{1}] FOREIGN KEY([{1}]) REFERENCES [Entity_{2}]([{3}])", _type.Key, _attr.Key, _FKtype.Key, _FKattr.Key).AppendLine();
                    }
                    if (!string.IsNullOrEmpty(_attr.Default)) {
                        _commandAfter.AppendFormat("ALTER TABLE [Entity_{0}] ADD CONSTRAINT [DF_{0}_{1}] DEFAULT ('{2}') for [{1}]", _type.Key, _attr.Key, _attr.Default).AppendLine();
                    }
                }
                _command.AppendLine(")");
                _command.Append(_commandAfter);
                if (!_hasPrimaryKey) throw new Exception("missing primary key");
            } else {
                _command.AppendFormat("DROP TABLE [Entity_{0}]", _type.Key);
            }
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("EditActive", _command.ToString());
            _procedure.InvokeProcedure(DB);
        }
    }
}
