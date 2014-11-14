/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityTable : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public long TypeID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public bool Main { get; set; }
        public bool Multiply { get; set; }
        public string Description { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityTable_" + name;
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

        static EntityTable() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["TypeID"] != null) { this.TypeID = Convert.ToInt64(inTempRow["TypeID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["Main"] != null) { this.Main = Convert.ToBoolean(inTempRow["Main"]); }
			if (cols["Multiply"] != null) { this.Multiply = Convert.ToBoolean(inTempRow["Multiply"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["TypeID"] = this.TypeID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["Main"] = this.Main;
			inTempRow["Multiply"] = this.Multiply;
			inTempRow["Description"] = this.Description;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("TypeID", this.TypeID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Main", this.Main);
			inItem.SetItem("Multiply", this.Multiply);
			inItem.SetItem("Description", this.Description);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "TypeID", "Name", "Key", "Main", "Multiply", "Description" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "TypeID":
					return this.TypeID;
				case "Name":
					return this.Name;
				case "Key":
					return this.Key;
				case "Main":
					return this.Main;
				case "Multiply":
					return this.Multiply;
				case "Description":
					return this.Description;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityTable>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityTable>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

		public static System.Data.DataTable CreateEmptyTable() {
			System.Data.DataTable _table = new System.Data.DataTable();
			_table.Columns.Add("ID", typeof(Int64));
			_table.Columns.Add("TypeID", typeof(Int64));
			_table.Columns.Add("Name", typeof(String));
			_table.Columns.Add("Key", typeof(String));
			_table.Columns.Add("Main", typeof(Boolean));
			_table.Columns.Add("Multiply", typeof(Boolean));
			_table.Columns.Add("Description", typeof(String));
			return _table;
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@TypeID bigint,
	@Name nvarchar(32),
	@Key nvarchar(32),
	@Main bit,
	@Multiply bit,
	@Description nvarchar(256)

	[ID] , [TypeID] , [Name] , [Key] , [Main] , [Multiply] , [Description]
	@ID , @TypeID , @Name , @Key , @Main , @Multiply , @Description
	[ID] = @ID,
	[TypeID] = @TypeID,
	[Name] = @Name,
	[Key] = @Key,
	[Main] = @Main,
	[Multiply] = @Multiply,
	[Description] = @Description
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("TypeID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Main", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Multiply", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, long formTypeID, string formName, string formKey, bool formMain, bool formMultiply, string formDescription
            formID, formTypeID, formName, formKey, formMain, formMultiply, formDescription
            protected long formID;
            protected long formTypeID;
            protected string formName;
            protected string formKey;
            protected bool formMain;
            protected bool formMultiply;
            protected string formDescription;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formTypeID = Convert.ToInt64(Request.Form["TypeID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formMain = Convert.ToBoolean(Request.Form["Main"]);
            formMultiply = Convert.ToBoolean(Request.Form["Multiply"]);
            formDescription = Request.Form["Description"];

            long inID, long inTypeID, string inName, string inKey, bool inMain, bool inMultiply, string inDescription
            inID, inTypeID, inName, inKey, inMain, inMultiply, inDescription
            item.SetItem("ID", inID);
            item.SetItem("TypeID", inTypeID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("Main", inMain);
            item.SetItem("Multiply", inMultiply);
            item.SetItem("Description", inDescription);

            ID, TypeID, Name, Key, Main, Multiply, Description
            ID = _item.ID;
            TypeID = _item.TypeID;
            Name = _item.Name;
            Key = _item.Key;
            Main = _item.Main;
            Multiply = _item.Multiply;
            Description = _item.Description;

*/
#endregion
