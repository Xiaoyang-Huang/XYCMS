/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Installation {
    public partial class Installation : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Depend { get; set; }
        public string SQL { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Installation_Installation_" + name;
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

        static Installation() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["Name"] != null) { this.Name = Convert.ToString(inTempRow["Name"]); }
			if (cols["Version"] != null) { this.Version = Convert.ToString(inTempRow["Version"]); }
			if (cols["Depend"] != null) { this.Depend = Convert.ToString(inTempRow["Depend"]); }
			if (cols["SQL"] != null) { this.SQL = Convert.ToString(inTempRow["SQL"]); }
			if (cols["Code"] != null) { this.Code = Convert.ToString(inTempRow["Code"]); }
			if (cols["Message"] != null) { this.Message = Convert.ToString(inTempRow["Message"]); }
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Version", this.Version);
			inItem.SetItem("Depend", this.Depend);
			inItem.SetItem("SQL", this.SQL);
			inItem.SetItem("Code", this.Code);
			inItem.SetItem("Message", this.Message);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Version", "Depend", "SQL", "Code", "Message" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Version":
					return this.Version;
				case "Depend":
					return this.Depend;
				case "SQL":
					return this.SQL;
				case "Code":
					return this.Code;
				case "Message":
					return this.Message;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<Installation>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</Installation>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@Name nvarchar(64),
	@Version nvarchar(64),
	@Depend nvarchar(64),
	@SQL ntext,
	@Code ntext,
	@Message ntext

	[ID] , [Name] , [Version] , [Depend] , [SQL] , [Code] , [Message]
	@ID , @Name , @Version , @Depend , @SQL , @Code , @Message
	[ID] = @ID,
	[Name] = @Name,
	[Version] = @Version,
	[Depend] = @Depend,
	[SQL] = @SQL,
	[Code] = @Code,
	[Message] = @Message
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Version", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Depend", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("SQL", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Code", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Message", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formID, string formName, string formVersion, string formDepend, string formSQL, string formCode, string formMessage
            formID, formName, formVersion, formDepend, formSQL, formCode, formMessage
            protected long formID;
            protected string formName;
            protected string formVersion;
            protected string formDepend;
            protected string formSQL;
            protected string formCode;
            protected string formMessage;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formVersion = Request.Form["Version"];
            formDepend = Request.Form["Depend"];
            formSQL = Request.Form["SQL"];
            formCode = Request.Form["Code"];
            formMessage = Request.Form["Message"];

            long inID, string inName, string inVersion, string inDepend, string inSQL, string inCode, string inMessage
            inID, inName, inVersion, inDepend, inSQL, inCode, inMessage
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Version", inVersion);
            item.SetItem("Depend", inDepend);
            item.SetItem("SQL", inSQL);
            item.SetItem("Code", inCode);
            item.SetItem("Message", inMessage);

            ID, Name, Version, Depend, SQL, Code, Message
            ID = _item.ID;
            Name = _item.Name;
            Version = _item.Version;
            Depend = _item.Depend;
            SQL = _item.SQL;
            Code = _item.Code;
            Message = _item.Message;

*/
#endregion
