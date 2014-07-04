/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class PowerList : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

		public static string R(string name) {
		    return "XiaoYang_User_PowerList_" + name;
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

        static PowerList() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt32(inTempRow["ID"]); }
			if (cols["Key"] != null) { this.Key = Convert.ToString(inTempRow["Key"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Key", this.Key);
			inItem.SetItem("Description", this.Description);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Key", "Description" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Key":
					return this.Key;
				case "Description":
					return this.Description;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<PowerList>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</PowerList>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID int,
	@Key nvarchar(128),
	@Description nvarchar(128)

	[ID] , [Key] , [Description]
	@ID , @Key , @Description
	[ID] = @ID,
	[Key] = @Key,
	[Description] = @Description
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int32);
            item.AddItem("Key", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            int formID, string formKey, string formDescription
            formID, formKey, formDescription
            protected int formID;
            protected string formKey;
            protected string formDescription;

            formID = Convert.ToInt32(Request.Form["ID"]);
            formKey = Request.Form["Key"];
            formDescription = Request.Form["Description"];

            int inID, string inKey, string inDescription
            inID, inKey, inDescription
            item.SetItem("ID", inID);
            item.SetItem("Key", inKey);
            item.SetItem("Description", inDescription);

            ID, Key, Description
            ID = _item.ID;
            Key = _item.Key;
            Description = _item.Description;

*/
#endregion
