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
            _procedures[R("Add")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_ValidateActive);
            _procedures[R("Add")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_BeforeAdd);
            _procedures[R("Add")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityAttribute_RefreshCache);

            _procedures[R("Edit")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_ValidateActive);
            _procedures[R("Edit")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_BeforeEdit);
            _procedures[R("Edit")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityAttribute_RefreshCache);

            _procedures[R("Del")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_ValidateActive);
            _procedures[R("Del")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityAttribute_BeforeDel);
            _procedures[R("Del")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityAttribute_RefreshCache);

            _procedures[R("GetByTypeID")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityAttribute_AfterGetByTypeID);
        }

        static void EntityAttribute_RefreshCache(Xy.Data.ProcedureResult result, Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            _typeNames = new Dictionary<long, string>();
        }

        private const string _commandPartern_DelConstraintDefault =
@"if exists(select c.name from sysconstraints a 
inner join syscolumns b on a.colid=b.colid 
inner join sysobjects c on a.constid=c.id 
where a.id=object_id('[{0}]') 
and b.name='{1}')
Alter table [{0}] drop constraint DF_{0}_{1}; ";
        private const string _commandPartern_UpdateDefault = @"Update [{0}] set [{1}] = '{2}' where [{1}] is null;";
        private const string _commandPartern_EditAttribute = @"Alter table [{0}] alter column [{1}] {2};";
        private const string _commandPartern_EditAttributeName = @"exec sp_rename '{0}.{1}','{2}','column';";
        private const string _commandPartern_AddConstraintDefault =
@"Alter table [{0}] add constraint DF_{0}_{1} default ('{2}') for [{1}];";
        static void EntityAttribute_BeforeEdit(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            EntityAttribute _oldAttr = EntityAttribute.GetInstance(Convert.ToInt64(procedure.GetItem("ID")));
            bool _isMultiple = Convert.ToBoolean(procedure.GetItem("IsMultiple"));
            if (_oldAttr.IsMultiple != _isMultiple) throw new Exception("can not switch attribute multiple mode");
            StringBuilder _editAttrCommand = new StringBuilder();
            string _default = Convert.ToString(procedure.GetItem("Default"));
            if (!string.IsNullOrEmpty(_oldAttr.Default) && string.IsNullOrEmpty(_default)) {
                _editAttrCommand.AppendLine(string.Format(_commandPartern_DelConstraintDefault, _oldAttr.Table, _oldAttr.Key));
            }
            if (!string.IsNullOrEmpty(_default)) {
                _editAttrCommand.AppendLine(string.Format(_commandPartern_UpdateDefault, _oldAttr.Table, _oldAttr.Key, _default));
                _editAttrCommand.AppendLine(string.Format(_commandPartern_AddConstraintDefault, _oldAttr.Table, _oldAttr.Key, _default));
            }
            string _editAttrDBType = GetTypeName(Convert.ToString(procedure.GetItem("Type")));
            if (!Convert.ToBoolean(procedure.GetItem("IsNull"))) {
                _editAttrDBType += " NOT NULL";
            }
            _editAttrCommand.AppendLine(string.Format(_commandPartern_EditAttribute, _oldAttr.Table, _oldAttr.Key, _editAttrDBType));
            string _key = Convert.ToString(procedure.GetItem("Key"));
            if (string.Compare(_key, _oldAttr.Key) != 0) {
                _editAttrCommand.AppendLine(string.Format(_commandPartern_EditAttributeName, _oldAttr.Table, _oldAttr.Key, _key));
            }
            Xy.Data.Procedure _editAttrColumn = new Xy.Data.Procedure("EditAttribute", _editAttrCommand.ToString());
            _editAttrColumn.InvokeProcedure();
        }

        private const string _commandPartern_DelAttributeTable = @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}]') AND type in (N'U')) Drop Table [{0}];";
        private const string _commandPartern_DelAttributeColumn = @"IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[{0}]') and name = N'{1}') Alter Table [{0}] Drop Column [{1}]";
        static void EntityAttribute_BeforeDel(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            long _attrID = Convert.ToInt64(procedure.GetItem("ID"));
            EntityAttribute _attr = GetInstance(_attrID);            
            StringBuilder _delAttrCommand = new StringBuilder();
            if (_attr.IsMultiple) {
                _delAttrCommand.AppendLine(string.Format(_commandPartern_DelAttributeTable, _attr.Table));
            } else {
                _delAttrCommand.AppendLine(string.Format(_commandPartern_DelAttributeColumn, _attr.Table, _attr.Key));
            }
            if (!string.IsNullOrEmpty(_attr.Default)) {
                _delAttrCommand.AppendLine(string.Format(_commandPartern_DelConstraintDefault, _attr.Table, _attr.Key));
            }
            Xy.Data.Procedure _delAttrColumn = new Xy.Data.Procedure("DelAttribute", _delAttrCommand.ToString());
            _delAttrColumn.InvokeProcedure();
        }

        private const string _commandPartern_AddMultipleAttribute =
@"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}_{1}]') AND type in (N'U')) 
BEGIN
CREATE TABLE [{0}_{1}](
    [ID] [bigint] IDENTITY(1,1) NOT NULL,
    [EntityID] [bigint] NOT NULL,
    [{1}] {2}
CONSTRAINT [PK_{0}_{1}] PRIMARY KEY CLUSTERED 
    ([ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
ELSE
Delete FROM {0}_{1}";
        private const string _commandPartern_AddSimpleAttribute = @"ALTER TABLE [{0}] ADD [{1}] {2} ";
        private static void EntityAttribute_BeforeAdd(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            string _attrName = Convert.ToString(produce.GetItem("Key"));
            EntityType _type = EntityType.GetInstance(Convert.ToInt16(produce.GetItem("TypeID")));
            System.Data.DataTable _attrs = GetByTypeID(_type.ID);
            foreach (System.Data.DataRow _attr in _attrs.Rows) {
                if (string.Compare(_attr["Name"].ToString(), _attrName, true) == 0) throw new Exception(string.Format("\"{0}\" already existed", _attrName));
            }
            string _addAttrCommand;
            if (Convert.ToBoolean(produce.GetItem("IsMultiple"))) {
                _addAttrCommand = _commandPartern_AddMultipleAttribute;
            } else {
                _addAttrCommand = _commandPartern_AddSimpleAttribute;
            }
            string _addAttrDBType = GetTypeName(Convert.ToString(produce.GetItem("Type")));
            if (!Convert.ToBoolean(produce.GetItem("IsNull"))) {
                _addAttrDBType += " NOT NULL";
            }
            _addAttrCommand = string.Format(_addAttrCommand, "Entity_" + _type.Key, _attrName, _addAttrDBType);

            Xy.Data.Procedure _addAttrColumn = new Xy.Data.Procedure("AddAttribute", _addAttrCommand);
            _addAttrColumn.InvokeProcedure(DB);
        }

        private static void EntityAttribute_AfterGetByTypeID(Xy.Data.ProcedureResult result, Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            short _typeID = Convert.ToInt16(produce.GetItem("TypeID"));
            EntityType _type = EntityType.GetInstance(_typeID);
            if (_type != null && _type.ParentTypeID > 0) {
                System.Data.DataTable _parentAttrs = GetByTypeID(_type.ParentTypeID);
                foreach (System.Data.DataRow _row in _parentAttrs.Rows) {
                    result.DataResult.ImportRow(_row);
                }
            }
        }

        private static void EntityAttribute_ValidateActive(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            EntityType _type;
            if (produce.GetItem("TypeID") == null) {
                long _attrID = Convert.ToInt64(produce.GetItem("ID"));
                EntityAttribute _attr = GetInstance(_attrID, DB);
                _type = EntityType.GetInstance(_attr.TypeID);
            } else {
                _type = EntityType.GetInstance(Convert.ToInt16(produce.GetItem("TypeID")));
            }
            if (_type.IsActive) throw new Exception("Cannot modify actived type");
            EntityType.EditUpdateTime(_type.ID);
        }

        public static string GetTypeName(string inTypeString) {
            System.Data.DbType _type = (System.Data.DbType)Enum.Parse(typeof(System.Data.DbType), inTypeString.Split('|')[0].Split('.')[3]);
            switch (_type) {
                case System.Data.DbType.Binary:
                case System.Data.DbType.DateTime2:
                case System.Data.DbType.DateTimeOffset:
                case System.Data.DbType.Decimal:
                case System.Data.DbType.Time:
                    return _type.ToString() + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.AnsiString:
                    return "VarChar" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.AnsiStringFixedLength:
                    return "Char" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.String:
                    return "Text";
                case System.Data.DbType.VarNumeric:
                    return "Numeric" + "(" + inTypeString.Split('|')[1] + ")";
                case System.Data.DbType.Boolean:
                    return "Bit";
                case System.Data.DbType.Byte:
                    return "TinyInt";
                case System.Data.DbType.Currency:
                    return "Money";
                case System.Data.DbType.Double:
                    return "Float";
                case System.Data.DbType.Guid:
                    return "Uniqueidentifier";
                case System.Data.DbType.Int16:
                case System.Data.DbType.UInt16:
                    return "SmallInt";
                case System.Data.DbType.Int32:
                case System.Data.DbType.UInt32:
                    return "Int";
                case System.Data.DbType.Int64:
                case System.Data.DbType.UInt64:
                    return "BigInt";
                case System.Data.DbType.Single:
                    return "Real";
                case System.Data.DbType.Object:
                case System.Data.DbType.SByte:
                case System.Data.DbType.StringFixedLength:
                    throw new ArgumentException("can not found a correspond SQL type.", "inTypeString");
                default:
                    return _type.ToString();
            }
        }

    }
}
