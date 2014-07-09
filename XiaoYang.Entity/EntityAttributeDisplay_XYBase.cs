/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityAttributeDisplay : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public string Resource { get; set; }
        public string TypeClass { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityAttributeDisplay_" + name;
		}

        private static Dictionary<string, Xy.Data.Procedure> _procedures;
		private static void AddProcedure(Xy.Data.Procedure _inValue) {
			if (_procedures == null) _procedures = new Dictionary<string, Xy.Data.Procedure>();
			_procedures.Add(_inValue.Name, _inValue);
		}

		private static Xy.Data.Procedure GetProcedure(string name) {
			if (_procedures == null) _procedures = new Dictionary<string,Xy.Data.Procedure>();
			if (_procedures.ContainsKey(name)) {
				return _procedures[name].Clone();
			} else {
				throw new Exception(string.Format("can not found \"{0}\" in procedure list", name));
			}
		}

        static EntityAttributeDisplay() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Template"] != null) { this.Template = Convert.ToString(inTempRow["Template"]); }
			if (cols["Resource"] != null) { this.Resource = Convert.ToString(inTempRow["Resource"]); }
			if (cols["TypeClass"] != null) { this.TypeClass = Convert.ToString(inTempRow["TypeClass"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["Name"] = this.Name;
			inTempRow["Template"] = this.Template;
			inTempRow["Resource"] = this.Resource;
			inTempRow["TypeClass"] = this.TypeClass;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Template", this.Template);
			inItem.SetItem("Resource", this.Resource);
			inItem.SetItem("TypeClass", this.TypeClass);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Template", "Resource", "TypeClass" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Template":
					return this.Template;
				case "Resource":
					return this.Resource;
				case "TypeClass":
					return this.TypeClass;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityAttributeDisplay>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityAttributeDisplay>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@Name nvarchar(64),
	@Template ntext,
	@Resource ntext,
	@TypeClass nvarchar(32)

	[ID] , [Name] , [Template] , [Resource] , [TypeClass]
	@ID , @Name , @Template , @Resource , @TypeClass
	[ID] = @ID,
	[Name] = @Name,
	[Template] = @Template,
	[Resource] = @Resource,
	[TypeClass] = @TypeClass
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Template", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Resource", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("TypeClass", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, string formName, string formTemplate, string formResource, string formTypeClass
            formID, formName, formTemplate, formResource, formTypeClass
            protected long formID;
            protected string formName;
            protected string formTemplate;
            protected string formResource;
            protected string formTypeClass;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formTemplate = Request.Form["Template"];
            formResource = Request.Form["Resource"];
            formTypeClass = Request.Form["TypeClass"];

            long inID, string inName, string inTemplate, string inResource, string inTypeClass
            inID, inName, inTemplate, inResource, inTypeClass
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Template", inTemplate);
            item.SetItem("Resource", inResource);
            item.SetItem("TypeClass", inTypeClass);

            ID, Name, Template, Resource, TypeClass
            ID = _item.ID;
            Name = _item.Name;
            Template = _item.Template;
            Resource = _item.Resource;
            TypeClass = _item.TypeClass;

*/
#endregion
