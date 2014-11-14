/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityField : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public long TableID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public long Template { get; set; }
        public bool Primary { get; set; }
        public bool Increase { get; set; }
        public bool Null { get; set; }
        public bool Foreign { get; set; }
        public string Description { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityField_" + name;
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

        static EntityField() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["TableID"] != null) { this.TableID = Convert.ToInt64(inTempRow["TableID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["Type"] != null) { this.Type = Convert.ToString(inTempRow["Type"]); }
			if (cols["Default"] != null) { this.Default = Convert.ToString(inTempRow["Default"]); }
			if (cols["Template"] != null) { this.Template = Convert.ToInt64(inTempRow["Template"]); }
			if (cols["Primary"] != null) { this.Primary = Convert.ToBoolean(inTempRow["Primary"]); }
			if (cols["Increase"] != null) { this.Increase = Convert.ToBoolean(inTempRow["Increase"]); }
			if (cols["Null"] != null) { this.Null = Convert.ToBoolean(inTempRow["Null"]); }
			if (cols["Foreign"] != null) { this.Foreign = Convert.ToBoolean(inTempRow["Foreign"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["TableID"] = this.TableID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["Type"] = this.Type;
			inTempRow["Default"] = this.Default;
			inTempRow["Template"] = this.Template;
			inTempRow["Primary"] = this.Primary;
			inTempRow["Increase"] = this.Increase;
			inTempRow["Null"] = this.Null;
			inTempRow["Foreign"] = this.Foreign;
			inTempRow["Description"] = this.Description;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("TableID", this.TableID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Type", this.Type);
			inItem.SetItem("Default", this.Default);
			inItem.SetItem("Template", this.Template);
			inItem.SetItem("Primary", this.Primary);
			inItem.SetItem("Increase", this.Increase);
			inItem.SetItem("Null", this.Null);
			inItem.SetItem("Foreign", this.Foreign);
			inItem.SetItem("Description", this.Description);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "TableID", "Name", "Key", "Type", "Default", "Template", "Primary", "Increase", "Null", "Foreign", "Description" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "TableID":
					return this.TableID;
				case "Name":
					return this.Name;
				case "Key":
					return this.Key;
				case "Type":
					return this.Type;
				case "Default":
					return this.Default;
				case "Template":
					return this.Template;
				case "Primary":
					return this.Primary;
				case "Increase":
					return this.Increase;
				case "Null":
					return this.Null;
				case "Foreign":
					return this.Foreign;
				case "Description":
					return this.Description;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityField>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityField>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

		public static System.Data.DataTable CreateEmptyTable() {
			System.Data.DataTable _table = new System.Data.DataTable();
			_table.Columns.Add("ID", typeof(Int64));
			_table.Columns.Add("TableID", typeof(Int64));
			_table.Columns.Add("Name", typeof(String));
			_table.Columns.Add("Key", typeof(String));
			_table.Columns.Add("Type", typeof(String));
			_table.Columns.Add("Default", typeof(String));
			_table.Columns.Add("Template", typeof(Int64));
			_table.Columns.Add("Primary", typeof(Boolean));
			_table.Columns.Add("Increase", typeof(Boolean));
			_table.Columns.Add("Null", typeof(Boolean));
			_table.Columns.Add("Foreign", typeof(Boolean));
			_table.Columns.Add("Description", typeof(String));
			return _table;
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@TableID bigint,
	@Name nvarchar(32),
	@Key nvarchar(32),
	@Type nvarchar(32),
	@Default nvarchar(128),
	@Template bigint,
	@Primary bit,
	@Increase bit,
	@Null bit,
	@Foreign bit,
	@Description nvarchar(256)

	[ID] , [TableID] , [Name] , [Key] , [Type] , [Default] , [Template] , [Primary] , [Increase] , [Null] , [Foreign] , [Description]
	@ID , @TableID , @Name , @Key , @Type , @Default , @Template , @Primary , @Increase , @Null , @Foreign , @Description
	[ID] = @ID,
	[TableID] = @TableID,
	[Name] = @Name,
	[Key] = @Key,
	[Type] = @Type,
	[Default] = @Default,
	[Template] = @Template,
	[Primary] = @Primary,
	[Increase] = @Increase,
	[Null] = @Null,
	[Foreign] = @Foreign,
	[Description] = @Description
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("TableID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Type", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Default", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Template", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Primary", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Increase", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Null", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Foreign", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, long formTableID, string formName, string formKey, string formType, string formDefault, long formTemplate, bool formPrimary, bool formIncrease, bool formNull, bool formForeign, string formDescription
            formID, formTableID, formName, formKey, formType, formDefault, formTemplate, formPrimary, formIncrease, formNull, formForeign, formDescription
            protected long formID;
            protected long formTableID;
            protected string formName;
            protected string formKey;
            protected string formType;
            protected string formDefault;
            protected long formTemplate;
            protected bool formPrimary;
            protected bool formIncrease;
            protected bool formNull;
            protected bool formForeign;
            protected string formDescription;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formTableID = Convert.ToInt64(Request.Form["TableID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formType = Request.Form["Type"];
            formDefault = Request.Form["Default"];
            formTemplate = Convert.ToInt64(Request.Form["Template"]);
            formPrimary = Convert.ToBoolean(Request.Form["Primary"]);
            formIncrease = Convert.ToBoolean(Request.Form["Increase"]);
            formNull = Convert.ToBoolean(Request.Form["Null"]);
            formForeign = Convert.ToBoolean(Request.Form["Foreign"]);
            formDescription = Request.Form["Description"];

            long inID, long inTableID, string inName, string inKey, string inType, string inDefault, long inTemplate, bool inPrimary, bool inIncrease, bool inNull, bool inForeign, string inDescription
            inID, inTableID, inName, inKey, inType, inDefault, inTemplate, inPrimary, inIncrease, inNull, inForeign, inDescription
            item.SetItem("ID", inID);
            item.SetItem("TableID", inTableID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("Type", inType);
            item.SetItem("Default", inDefault);
            item.SetItem("Template", inTemplate);
            item.SetItem("Primary", inPrimary);
            item.SetItem("Increase", inIncrease);
            item.SetItem("Null", inNull);
            item.SetItem("Foreign", inForeign);
            item.SetItem("Description", inDescription);

            ID, TableID, Name, Key, Type, Default, Template, Primary, Increase, Null, Foreign, Description
            ID = _item.ID;
            TableID = _item.TableID;
            Name = _item.Name;
            Key = _item.Key;
            Type = _item.Type;
            Default = _item.Default;
            Template = _item.Template;
            Primary = _item.Primary;
            Increase = _item.Increase;
            Null = _item.Null;
            Foreign = _item.Foreign;
            Description = _item.Description;

*/
#endregion
