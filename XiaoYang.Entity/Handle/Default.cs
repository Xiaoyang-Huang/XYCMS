using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity.Handle {
    public class Default: IEntityHandle {

        private Entity.EntityType _type;
        EntityAttribute[] _attrs;

        public void DropTable() {
            StringBuilder _command = new StringBuilder();
            _command.AppendFormat("DROP TABLE [Entity_{0}]", _type.Key);
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("DropTable", _command.ToString());
        }

        public void CreateTable() {
            
            StringBuilder _command = new StringBuilder();
            StringBuilder _commandAfter = new StringBuilder();
            bool _hasPrimaryKey = false;
            _command.AppendFormat("CREATE TABLE [Entity_{0}](", _type.Key).AppendLine();
            foreach(EntityAttribute _attr in _attrs){
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
            Xy.Data.Procedure _procedure = new Xy.Data.Procedure("CreateTable", _command.ToString());
        }

        public void Init(EntityType type) {
            _type = type;
            System.Data.DataTable _attrsTable = EntityAttribute.GetByTypeID(_type.ID);
            _attrs = new EntityAttribute[_attrsTable.Rows.Count];
            for (int i = 0; i < _attrsTable.Rows.Count; i++) {
                _attrs[i] = new EntityAttribute();
                _attrs[i].Fill(_attrsTable.Rows[i]);
            }
        }

        public Xy.Web.Page.PageAbstract GetEditPageClass() {
            EditPageClass _page = new EditPageClass();
            _page._type = _type;
            _page._attrs = _attrs;
            return _page;
        }

        private class EditPageClass : Xy.Web.Page.PageAbstract {

            private string _template =
@"<% @Data Provider=""Data"" Name=""AttributesTable"" EnableScript=""True"" %>
    <xsl:choose>
        <xsl:when test=""Display > 0"">
            <div class=""row-fluid"">
                <label>
                    <xsl:value-of select=""Name"" />
                    <xsl:if test=""string-length(Description) > 0"">(<xsl:value-of select=""Description"" />)</xsl:if>
                </label>
                <xsl:value-of select=""DisplayHTML"" disable-output-escaping=""yes"" />
            </div>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of select=""DisplayHTML"" disable-output-escaping=""yes"" />
        </xsl:otherwise>
    </xsl:choose>
<% @End %>";
            private string _hiddenInput = @"<input type=""hidden"" name=""{0}"" value=""{1}"" />";

            internal Entity.EntityType _type;
            internal Entity.EntityAttribute[] _attrs;

            public override void onGetRequest() {
                Xy.Web.HTMLContainer _templateContainer = new Xy.Web.HTMLContainer(WebSetting.Encoding);
                _templateContainer.Write(_template);
                SetContent(_templateContainer);

                System.Data.DataTable _attrTable = Entity.EntityAttribute.GetByTypeID(_type.ID);
                _attrTable.Columns.Add("DisplayHTML", typeof(String));

                foreach (System.Data.DataRow _row in _attrTable.Rows) {
                    long _attrID = Convert.ToInt64(_row["ID"]);
                    long _displayID = Convert.ToInt64(_row["Display"]);
                    if (_displayID > 0) {
                        EntityAttributeDisplay _display = EntityAttributeDisplay.GetInstance(_displayID);
                        string _attrTemplate = _display.Template
                            .Replace("{{AttributeName}}", _row["Name"].ToString())
                            .Replace("{{AttributeKey}}", _row["Key"].ToString())
                            .Replace("{{TypeID}}", _type.ID.ToString())
                            .Replace("{{TypeName}}", _type.Name.ToString())
                            .Replace("{{TypeKey}}", _type.Key.ToString());
                        //Value:属性值, 对于新Post此标签将用作处理默认值
                        //PostID:此模板关联的PostID
                        _row["DisplayHTML"] = _attrTemplate;
                    } else {
                        _row["DisplayHTML"] = string.Format(_hiddenInput, _row["Key"].ToString(), string.Empty);
                    }
                }

                PageData.Add("AttributesTable", _attrTable);
            }

            public override void Validate() {
                
            }
        }
    }
}
