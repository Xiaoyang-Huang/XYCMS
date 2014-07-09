/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityAttribute : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public short TypeID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public long Display { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsNull { get; set; }
        public string Description { get; set; }
        public string Split { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityAttribute_" + name;
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

        static EntityAttribute() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["TypeID"] != null) { this.TypeID = Convert.ToInt16(inTempRow["TypeID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["Type"] != null) { this.Type = Convert.ToString(inTempRow["Type"]); }
			if (cols["Default"] != null) { this.Default = Convert.ToString(inTempRow["Default"]); }
			if (cols["Display"] != null) { this.Display = Convert.ToInt64(inTempRow["Display"]); }
			if (cols["IsMultiple"] != null) { this.IsMultiple = Convert.ToBoolean(inTempRow["IsMultiple"]); }
			if (cols["IsNull"] != null) { this.IsNull = Convert.ToBoolean(inTempRow["IsNull"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
			if (cols["Split"] != null) { this.Split = Convert.ToString(inTempRow["Split"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["TypeID"] = this.TypeID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["Type"] = this.Type;
			inTempRow["Default"] = this.Default;
			inTempRow["Display"] = this.Display;
			inTempRow["IsMultiple"] = this.IsMultiple;
			inTempRow["IsNull"] = this.IsNull;
			inTempRow["Description"] = this.Description;
			inTempRow["Split"] = this.Split;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("TypeID", this.TypeID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Type", this.Type);
			inItem.SetItem("Default", this.Default);
			inItem.SetItem("Display", this.Display);
			inItem.SetItem("IsMultiple", this.IsMultiple);
			inItem.SetItem("IsNull", this.IsNull);
			inItem.SetItem("Description", this.Description);
			inItem.SetItem("Split", this.Split);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "TypeID", "Name", "Key", "Type", "Default", "Display", "IsMultiple", "IsNull", "Description", "Split" };
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
				case "Type":
					return this.Type;
				case "Default":
					return this.Default;
				case "Display":
					return this.Display;
				case "IsMultiple":
					return this.IsMultiple;
				case "IsNull":
					return this.IsNull;
				case "Description":
					return this.Description;
				case "Split":
					return this.Split;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityAttribute>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityAttribute>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@TypeID smallint,
	@Name nvarchar(64),
	@Key nvarchar(64),
	@Type nvarchar(64),
	@Default nvarchar(256),
	@Display bigint,
	@IsMultiple bit,
	@IsNull bit,
	@Description nvarchar(256),
	@Split nvarchar(1)

	[ID] , [TypeID] , [Name] , [Key] , [Type] , [Default] , [Display] , [IsMultiple] , [IsNull] , [Description] , [Split]
	@ID , @TypeID , @Name , @Key , @Type , @Default , @Display , @IsMultiple , @IsNull , @Description , @Split
	[ID] = @ID,
	[TypeID] = @TypeID,
	[Name] = @Name,
	[Key] = @Key,
	[Type] = @Type,
	[Default] = @Default,
	[Display] = @Display,
	[IsMultiple] = @IsMultiple,
	[IsNull] = @IsNull,
	[Description] = @Description,
	[Split] = @Split
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("TypeID", System.Data.DbType.System.Data.DbType.Int16);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Type", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Default", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Display", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("IsMultiple", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("IsNull", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Split", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, short formTypeID, string formName, string formKey, string formType, string formDefault, long formDisplay, bool formIsMultiple, bool formIsNull, string formDescription, string formSplit
            formID, formTypeID, formName, formKey, formType, formDefault, formDisplay, formIsMultiple, formIsNull, formDescription, formSplit
            protected long formID;
            protected short formTypeID;
            protected string formName;
            protected string formKey;
            protected string formType;
            protected string formDefault;
            protected long formDisplay;
            protected bool formIsMultiple;
            protected bool formIsNull;
            protected string formDescription;
            protected string formSplit;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formTypeID = Convert.ToInt16(Request.Form["TypeID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formType = Request.Form["Type"];
            formDefault = Request.Form["Default"];
            formDisplay = Convert.ToInt64(Request.Form["Display"]);
            formIsMultiple = Convert.ToBoolean(Request.Form["IsMultiple"]);
            formIsNull = Convert.ToBoolean(Request.Form["IsNull"]);
            formDescription = Request.Form["Description"];
            formSplit = Request.Form["Split"];

            long inID, short inTypeID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsMultiple, bool inIsNull, string inDescription, string inSplit
            inID, inTypeID, inName, inKey, inType, inDefault, inDisplay, inIsMultiple, inIsNull, inDescription, inSplit
            item.SetItem("ID", inID);
            item.SetItem("TypeID", inTypeID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("Type", inType);
            item.SetItem("Default", inDefault);
            item.SetItem("Display", inDisplay);
            item.SetItem("IsMultiple", inIsMultiple);
            item.SetItem("IsNull", inIsNull);
            item.SetItem("Description", inDescription);
            item.SetItem("Split", inSplit);

            ID, TypeID, Name, Key, Type, Default, Display, IsMultiple, IsNull, Description, Split
            ID = _item.ID;
            TypeID = _item.TypeID;
            Name = _item.Name;
            Key = _item.Key;
            Type = _item.Type;
            Default = _item.Default;
            Display = _item.Display;
            IsMultiple = _item.IsMultiple;
            IsNull = _item.IsNull;
            Description = _item.Description;
            Split = _item.Split;

*/
#endregion
