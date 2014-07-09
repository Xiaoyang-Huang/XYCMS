using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public partial class EntityType : Xy.Data.IDataModel {

        static void RegisterEvents() {
            _procedures[R("Add")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityType_AfterAdd);
            _procedures[R("Del")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EntityType_BeforeDel);
            _procedures[R("EditWebRelated")].AfterInvoke += new Xy.Data.afterInvokeHandler(EntityType_AfterEditWebRelated);
        }

        private const string _commandPartern_Del = @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}]') AND type in (N'U')) Drop Table [{0}];";
        static void EntityType_BeforeDel(Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short inID = Convert.ToInt16(procedure.GetItem("ID"));
            EntityType _type = GetInstance(inID);
            System.Data.DataTable _childTypies = GetChildType(inID);
            foreach (System.Data.DataRow _childTypeRow in _childTypies.Rows) {
                EntityType _childType = new EntityType();
                _childType.Fill(_childTypeRow);
                Del(_childType.ID);
            }
            StringBuilder _dropTableProcedure = new StringBuilder();
            foreach (System.Data.DataRow _multiAttrsRow in EntityAttribute.GetByTypeID(inID).Rows) {
                EntityAttribute _attr = new EntityAttribute();
                _attr.Fill(_multiAttrsRow);
                if (_attr.IsMultiple) {
                    _dropTableProcedure.AppendLine(string.Format(_commandPartern_Del, "Entity_" + _type.Key + "_" + _attr.Key));
                }
            }
            _dropTableProcedure.AppendLine(string.Format(_commandPartern_Del, "Entity_" + _type.Key));
            Xy.Data.Procedure _deleteRelativeTable = new Xy.Data.Procedure("DeleteRelativeTable", _dropTableProcedure.ToString());
            Xy.Data.Procedure _deleteRelativeEntityBase = new Xy.Data.Procedure("DeleteRelativeEntityBase", "Delete from [EntityBase] where [TypeID] = @ID", new Xy.Data.ProcedureParameter[] { new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64) });
            _deleteRelativeEntityBase.SetItem("ID", inID);
            _deleteRelativeTable.InvokeProcedure(DB);
            _deleteRelativeEntityBase.InvokeProcedure(DB);
        }

        private const string _commandPartern_Add =
@"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}]') AND type in (N'U')) 
BEGIN
CREATE TABLE [{0}](
    [ID] [bigint] IDENTITY(1,1) NOT NULL,
    [EntityBase_ID] [bigint] NOT NULL
CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
    ([ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
ALTER TABLE [{0}]  WITH CHECK ADD  CONSTRAINT [FK_{0}] FOREIGN KEY([EntityBase_ID]) REFERENCES [EntityBase]([ID])
END
ELSE
Delete FROM {0}";
        static void EntityType_AfterAdd(Xy.Data.ProcedureResult result, Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            string _tableName = Convert.ToString(procedure.GetItem("Key"));
            short _parentTypeID = Convert.ToInt16(procedure.GetItem("ParentTypeID"));
            if (string.IsNullOrEmpty(_tableName)) throw new Exception("Empty type name");
            StringBuilder _addTableProcedure = new StringBuilder();
            _addTableProcedure.AppendLine(string.Format(_commandPartern_Add, "Entity_" + _tableName));
            Xy.Data.Procedure _addBaseTable = new Xy.Data.Procedure("AddBaseTable", _addTableProcedure.ToString());
            _addBaseTable.InvokeProcedure(DB);
        }

        private const string _commandPartern_DelWebRelated = @"IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[{0}]') and name = N'WebConfig') ALTER TABLE [{0}] DROP COLUMN [WebConfig]";
        private const string _commandPartern_AddWebRelated = @"IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[{0}]') and name = N'WebConfig') ALTER TABLE [{0}] ADD [WebConfig] NVARCHAR(32) ";
        static void EntityType_AfterEditWebRelated(Xy.Data.ProcedureResult result, Xy.Data.Procedure procedure, Xy.Data.DataBase DB) {
            short _ID = Convert.ToInt16(procedure.GetItem("ID"));
            bool _isRelated = Convert.ToBoolean(procedure.GetItem("IsWebRelated"));
            EntityTypeCacheItem _cache = EntityTypeCache.GetInstance(_ID, DB);
            Xy.Data.Procedure _webRelatedProcedure;
            if (_isRelated) {
                _webRelatedProcedure = new Xy.Data.Procedure("AddWebRelated", string.Format(_commandPartern_AddWebRelated, "Entity_" + _cache.TypeInstance.Key));
            } else {
                _webRelatedProcedure = new Xy.Data.Procedure("DelWebRelated", string.Format(_commandPartern_DelWebRelated, "Entity_" + _cache.TypeInstance.Key));
            }
            _webRelatedProcedure.InvokeProcedure(DB);
        }
    }
}
