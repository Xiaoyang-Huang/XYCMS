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
        public long ID { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Handle { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }

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
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["Handle"] != null) { this.Handle = Convert.ToString(inTempRow["Handle"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
			if (cols["IsActive"] != null) { this.IsActive = Convert.ToBoolean(inTempRow["IsActive"]); }
			if (cols["IsAvailable"] != null) { this.IsAvailable = Convert.ToBoolean(inTempRow["IsAvailable"]); }
		}

		public void FillRow(System.Data.DataRow inTempRow) {
			inTempRow["ID"] = this.ID;
			inTempRow["Name"] = this.Name;
			inTempRow["Key"] = this.Key;
			inTempRow["Handle"] = this.Handle;
			inTempRow["Description"] = this.Description;
			inTempRow["IsActive"] = this.IsActive;
			inTempRow["IsAvailable"] = this.IsAvailable;
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Handle", this.Handle);
			inItem.SetItem("Description", this.Description);
			inItem.SetItem("IsActive", this.IsActive);
			inItem.SetItem("IsAvailable", this.IsAvailable);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Key", "Handle", "Description", "IsActive", "IsAvailable" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Key":
					return this.Key;
				case "Handle":
					return this.Handle;
				case "Description":
					return this.Description;
				case "IsActive":
					return this.IsActive;
				case "IsAvailable":
					return this.IsAvailable;
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

		public static System.Data.DataTable CreateEmptyTable() {
			System.Data.DataTable _table = new System.Data.DataTable();
			_table.Columns.Add("ID", typeof(Int64));
			_table.Columns.Add("Name", typeof(String));
			_table.Columns.Add("Key", typeof(String));
			_table.Columns.Add("Handle", typeof(String));
			_table.Columns.Add("Description", typeof(String));
			_table.Columns.Add("IsActive", typeof(Boolean));
			_table.Columns.Add("IsAvailable", typeof(Boolean));
			return _table;
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@Name nvarchar(32),
	@Key nvarchar(32),
	@Handle nvarchar(32),
	@Description nvarchar(256),
	@IsActive bit,
	@IsAvailable bit

	[ID] , [Name] , [Key] , [Handle] , [Description] , [IsActive] , [IsAvailable]
	@ID , @Name , @Key , @Handle , @Description , @IsActive , @IsAvailable
	[ID] = @ID,
	[Name] = @Name,
	[Key] = @Key,
	[Handle] = @Handle,
	[Description] = @Description,
	[IsActive] = @IsActive,
	[IsAvailable] = @IsAvailable
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Handle", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("IsActive", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("IsAvailable", System.Data.DbType.System.Data.DbType.Boolean);
            AddProcedure(item);

            long formID, string formName, string formKey, string formHandle, string formDescription, bool formIsActive, bool formIsAvailable
            formID, formName, formKey, formHandle, formDescription, formIsActive, formIsAvailable
            protected long formID;
            protected string formName;
            protected string formKey;
            protected string formHandle;
            protected string formDescription;
            protected bool formIsActive;
            protected bool formIsAvailable;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formKey = Request.Form["Key"];
            formHandle = Request.Form["Handle"];
            formDescription = Request.Form["Description"];
            formIsActive = Convert.ToBoolean(Request.Form["IsActive"]);
            formIsAvailable = Convert.ToBoolean(Request.Form["IsAvailable"]);

            long inID, string inName, string inKey, string inHandle, string inDescription, bool inIsActive, bool inIsAvailable
            inID, inName, inKey, inHandle, inDescription, inIsActive, inIsAvailable
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Key", inKey);
            item.SetItem("Handle", inHandle);
            item.SetItem("Description", inDescription);
            item.SetItem("IsActive", inIsActive);
            item.SetItem("IsAvailable", inIsAvailable);

            ID, Name, Key, Handle, Description, IsActive, IsAvailable
            ID = _item.ID;
            Name = _item.Name;
            Key = _item.Key;
            Handle = _item.Handle;
            Description = _item.Description;
            IsActive = _item.IsActive;
            IsAvailable = _item.IsAvailable;

*/
#endregion
