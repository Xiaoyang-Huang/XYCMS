/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityType : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public short ID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string BaseKey { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Description { get; set; }
        public short ParentTypeID { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityType_" + name;
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

        static EntityType() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt16(inTempRow["ID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["BaseKey"] != null) { this.BaseKey = Convert.ToString(inTempRow["BaseKey"]); }
			if (cols["IsDisplay"] != null) { this.IsDisplay = Convert.ToBoolean(inTempRow["IsDisplay"]); }
			if (cols["IsActive"] != null) { this.IsActive = Convert.ToBoolean(inTempRow["IsActive"]); }
			if (cols["UpdateTime"] != null) { this.UpdateTime = Convert.ToDateTime(inTempRow["UpdateTime"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
			if (cols["ParentTypeID"] != null) { this.ParentTypeID = Convert.ToInt16(inTempRow["ParentTypeID"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["BaseKey"] = this.BaseKey;
			inTempRow["IsDisplay"] = this.IsDisplay;
			inTempRow["IsActive"] = this.IsActive;
			inTempRow["UpdateTime"] = this.UpdateTime;
			inTempRow["Description"] = this.Description;
			inTempRow["ParentTypeID"] = this.ParentTypeID;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("BaseKey", this.BaseKey);
			inItem.SetItem("IsDisplay", this.IsDisplay);
			inItem.SetItem("IsActive", this.IsActive);
			inItem.SetItem("UpdateTime", this.UpdateTime);
			inItem.SetItem("Description", this.Description);
			inItem.SetItem("ParentTypeID", this.ParentTypeID);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Key", "BaseKey", "IsDisplay", "IsActive", "UpdateTime", "Description", "ParentTypeID" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Key":
					return this.Key;
				case "BaseKey":
					return this.BaseKey;
				case "IsDisplay":
					return this.IsDisplay;
				case "IsActive":
					return this.IsActive;
				case "UpdateTime":
					return this.UpdateTime;
				case "Description":
					return this.Description;
				case "ParentTypeID":
					return this.ParentTypeID;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityType>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityType>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID smallint,
	@Name nvarchar(64),
	@Key nvarchar(64),
	@BaseKey nvarchar(64),
	@IsDisplay bit,
	@IsActive bit,
	@UpdateTime datetime,
	@Description nvarchar(256),
	@ParentTypeID smallint

	[ID] , [Name] , [Key] , [BaseKey] , [IsDisplay] , [IsActive] , [UpdateTime] , [Description] , [ParentTypeID]
	@ID , @Name , @Key , @BaseKey , @IsDisplay , @IsActive , @UpdateTime , @Description , @ParentTypeID
	[ID] = @ID,
	[Name] = @Name,
	[Key] = @Key,
	[BaseKey] = @BaseKey,
	[IsDisplay] = @IsDisplay,
	[IsActive] = @IsActive,
	[UpdateTime] = @UpdateTime,
	[Description] = @Description,
	[ParentTypeID] = @ParentTypeID
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int16);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("BaseKey", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("IsDisplay", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("IsActive", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("UpdateTime", System.Data.DbType.System.Data.DbType.DateTime);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("ParentTypeID", System.Data.DbType.System.Data.DbType.Int16);
            AddProcedure(item);

            short formID, string formName, string formKey, string formBaseKey, bool formIsDisplay, bool formIsActive, DateTime formUpdateTime, string formDescription, short formParentTypeID
            formID, formName, formKey, formBaseKey, formIsDisplay, formIsActive, formUpdateTime, formDescription, formParentTypeID
            protected short formID;
            protected string formName;
            protected string formKey;
            protected string formBaseKey;
            protected bool formIsDisplay;
            protected bool formIsActive;
            protected DateTime formUpdateTime;
            protected string formDescription;
            protected short formParentTypeID;

            formID = Convert.ToInt16(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formBaseKey = Request.Form["BaseKey"];
            formIsDisplay = Convert.ToBoolean(Request.Form["IsDisplay"]);
            formIsActive = Convert.ToBoolean(Request.Form["IsActive"]);
            formUpdateTime = Convert.ToDateTime(Request.Form["UpdateTime"]);
            formDescription = Request.Form["Description"];
            formParentTypeID = Convert.ToInt16(Request.Form["ParentTypeID"]);

            short inID, string inName, string inKey, string inBaseKey, bool inIsDisplay, bool inIsActive, DateTime inUpdateTime, string inDescription, short inParentTypeID
            inID, inName, inKey, inBaseKey, inIsDisplay, inIsActive, inUpdateTime, inDescription, inParentTypeID
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("BaseKey", inBaseKey);
            item.SetItem("IsDisplay", inIsDisplay);
            item.SetItem("IsActive", inIsActive);
            item.SetItem("UpdateTime", inUpdateTime);
            item.SetItem("Description", inDescription);
            item.SetItem("ParentTypeID", inParentTypeID);

            ID, Name, Key, BaseKey, IsDisplay, IsActive, UpdateTime, Description, ParentTypeID
            ID = _item.ID;
            Name = _item.Name;
            Key = _item.Key;
            BaseKey = _item.BaseKey;
            IsDisplay = _item.IsDisplay;
            IsActive = _item.IsActive;
            UpdateTime = _item.UpdateTime;
            Description = _item.Description;
            ParentTypeID = _item.ParentTypeID;

*/
#endregion
