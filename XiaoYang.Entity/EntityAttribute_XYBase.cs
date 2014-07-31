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
        public bool IsNull { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsIncrease { get; set; }
        public long FKID { get; set; }

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
			if (cols["IsNull"] != null) { this.IsNull = Convert.ToBoolean(inTempRow["IsNull"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
			if (cols["IsPrimary"] != null) { this.IsPrimary = Convert.ToBoolean(inTempRow["IsPrimary"]); }
			if (cols["IsIncrease"] != null) { this.IsIncrease = Convert.ToBoolean(inTempRow["IsIncrease"]); }
			if (cols["FKID"] != null) { this.FKID = Convert.ToInt64(inTempRow["FKID"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["TypeID"] = this.TypeID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["Type"] = this.Type;
			inTempRow["Default"] = this.Default;
			inTempRow["Display"] = this.Display;
			inTempRow["IsNull"] = this.IsNull;
			inTempRow["Description"] = this.Description;
			inTempRow["IsPrimary"] = this.IsPrimary;
			inTempRow["IsIncrease"] = this.IsIncrease;
			inTempRow["FKID"] = this.FKID;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("TypeID", this.TypeID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Type", this.Type);
			inItem.SetItem("Default", this.Default);
			inItem.SetItem("Display", this.Display);
			inItem.SetItem("IsNull", this.IsNull);
			inItem.SetItem("Description", this.Description);
			inItem.SetItem("IsPrimary", this.IsPrimary);
			inItem.SetItem("IsIncrease", this.IsIncrease);
			inItem.SetItem("FKID", this.FKID);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "TypeID", "Name", "Key", "Type", "Default", "Display", "IsNull", "Description", "IsPrimary", "IsIncrease", "FKID" };
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
				case "IsNull":
					return this.IsNull;
				case "Description":
					return this.Description;
				case "IsPrimary":
					return this.IsPrimary;
				case "IsIncrease":
					return this.IsIncrease;
				case "FKID":
					return this.FKID;
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

		public static System.Data.DataTable CreateEmptyTable() {
			System.Data.DataTable _table = new System.Data.DataTable();
			_table.Columns.Add("ID", typeof(Int64));
			_table.Columns.Add("TypeID", typeof(Int16));
			_table.Columns.Add("Name", typeof(String));
			_table.Columns.Add("Key", typeof(String));
			_table.Columns.Add("Type", typeof(String));
			_table.Columns.Add("Default", typeof(String));
			_table.Columns.Add("Display", typeof(Int64));
			_table.Columns.Add("IsNull", typeof(Boolean));
			_table.Columns.Add("Description", typeof(String));
			_table.Columns.Add("IsPrimary", typeof(Boolean));
			_table.Columns.Add("IsIncrease", typeof(Boolean));
			_table.Columns.Add("FKID", typeof(Int64));
			return _table;
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
	@IsNull bit,
	@Description nvarchar(256),
	@IsPrimary bit,
	@IsIncrease bit,
	@FKID bigint

	[ID] , [TypeID] , [Name] , [Key] , [Type] , [Default] , [Display] , [IsNull] , [Description] , [IsPrimary] , [IsIncrease] , [FKID]
	@ID , @TypeID , @Name , @Key , @Type , @Default , @Display , @IsNull , @Description , @IsPrimary , @IsIncrease , @FKID
	[ID] = @ID,
	[TypeID] = @TypeID,
	[Name] = @Name,
	[Key] = @Key,
	[Type] = @Type,
	[Default] = @Default,
	[Display] = @Display,
	[IsNull] = @IsNull,
	[Description] = @Description,
	[IsPrimary] = @IsPrimary,
	[IsIncrease] = @IsIncrease,
	[FKID] = @FKID
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
            item.AddItem("IsNull", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("IsPrimary", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("IsIncrease", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("FKID", System.Data.DbType.System.Data.DbType.Int64);
            AddProcedure(item);

            long formID, short formTypeID, string formName, string formKey, string formType, string formDefault, long formDisplay, bool formIsNull, string formDescription, bool formIsPrimary, bool formIsIncrease, long formFKID
            formID, formTypeID, formName, formKey, formType, formDefault, formDisplay, formIsNull, formDescription, formIsPrimary, formIsIncrease, formFKID
            protected long formID;
            protected short formTypeID;
            protected string formName;
            protected string formKey;
            protected string formType;
            protected string formDefault;
            protected long formDisplay;
            protected bool formIsNull;
            protected string formDescription;
            protected bool formIsPrimary;
            protected bool formIsIncrease;
            protected long formFKID;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formTypeID = Convert.ToInt16(Request.Form["TypeID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formType = Request.Form["Type"];
            formDefault = Request.Form["Default"];
            formDisplay = Convert.ToInt64(Request.Form["Display"]);
            formIsNull = Convert.ToBoolean(Request.Form["IsNull"]);
            formDescription = Request.Form["Description"];
            formIsPrimary = Convert.ToBoolean(Request.Form["IsPrimary"]);
            formIsIncrease = Convert.ToBoolean(Request.Form["IsIncrease"]);
            formFKID = Convert.ToInt64(Request.Form["FKID"]);

            long inID, short inTypeID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsNull, string inDescription, bool inIsPrimary, bool inIsIncrease, long inFKID
            inID, inTypeID, inName, inKey, inType, inDefault, inDisplay, inIsNull, inDescription, inIsPrimary, inIsIncrease, inFKID
            item.SetItem("ID", inID);
            item.SetItem("TypeID", inTypeID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("Type", inType);
            item.SetItem("Default", inDefault);
            item.SetItem("Display", inDisplay);
            item.SetItem("IsNull", inIsNull);
            item.SetItem("Description", inDescription);
            item.SetItem("IsPrimary", inIsPrimary);
            item.SetItem("IsIncrease", inIsIncrease);
            item.SetItem("FKID", inFKID);

            ID, TypeID, Name, Key, Type, Default, Display, IsNull, Description, IsPrimary, IsIncrease, FKID
            ID = _item.ID;
            TypeID = _item.TypeID;
            Name = _item.Name;
            Key = _item.Key;
            Type = _item.Type;
            Default = _item.Default;
            Display = _item.Display;
            IsNull = _item.IsNull;
            Description = _item.Description;
            IsPrimary = _item.IsPrimary;
            IsIncrease = _item.IsIncrease;
            FKID = _item.FKID;

*/
#endregion
