/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityFieldForm : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Resource { get; set; }
        public string Template { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityFieldForm_" + name;
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

        static EntityFieldForm() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Resource"] != null) { this.Resource = Convert.ToString(inTempRow["Resource"]); }
			if (cols["Template"] != null) { this.Template = Convert.ToString(inTempRow["Template"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["Name"] = this.Name;
			inTempRow["Resource"] = this.Resource;
			inTempRow["Template"] = this.Template;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Resource", this.Resource);
			inItem.SetItem("Template", this.Template);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Resource", "Template" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Resource":
					return this.Resource;
				case "Template":
					return this.Template;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityFieldForm>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityFieldForm>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

		public static System.Data.DataTable CreateEmptyTable() {
			System.Data.DataTable _table = new System.Data.DataTable();
			_table.Columns.Add("ID", typeof(Int64));
			_table.Columns.Add("Name", typeof(String));
			_table.Columns.Add("Resource", typeof(String));
			_table.Columns.Add("Template", typeof(String));
			return _table;
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@Name nvarchar(32),
	@Resource text,
	@Template text

	[ID] , [Name] , [Resource] , [Template]
	@ID , @Name , @Resource , @Template
	[ID] = @ID,
	[Name] = @Name,
	[Resource] = @Resource,
	[Template] = @Template
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Resource", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Template", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, string formName, string formResource, string formTemplate
            formID, formName, formResource, formTemplate
            protected long formID;
            protected string formName;
            protected string formResource;
            protected string formTemplate;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formResource = Request.Form["Resource"];
            formTemplate = Request.Form["Template"];

            long inID, string inName, string inResource, string inTemplate
            inID, inName, inResource, inTemplate
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Resource", inResource);
            item.SetItem("Template", inTemplate);

            ID, Name, Resource, Template
            ID = _item.ID;
            Name = _item.Name;
            Resource = _item.Resource;
            Template = _item.Template;

*/
#endregion
