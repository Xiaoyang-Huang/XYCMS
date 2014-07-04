/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class PowerShip : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public int ID { get; set; }
        public int PowerList { get; set; }
        public int UserGroup { get; set; }

		public static string R(string name) {
		    return "XiaoYang_User_PowerShip_" + name;
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

        static PowerShip() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt32(inTempRow["ID"]); }
			if (cols["PowerList"] != null) { this.PowerList = Convert.ToInt32(inTempRow["PowerList"]); }
			if (cols["UserGroup"] != null) { this.UserGroup = Convert.ToInt32(inTempRow["UserGroup"]); }
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("PowerList", this.PowerList);
			inItem.SetItem("UserGroup", this.UserGroup);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "PowerList", "UserGroup" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "PowerList":
					return this.PowerList;
				case "UserGroup":
					return this.UserGroup;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<PowerShip>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</PowerShip>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID int,
	@PowerList int,
	@UserGroup int

	[ID] , [PowerList] , [UserGroup]
	@ID , @PowerList , @UserGroup
	[ID] = @ID,
	[PowerList] = @PowerList,
	[UserGroup] = @UserGroup
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int32);
            item.AddItem("PowerList", System.Data.DbType.System.Data.DbType.Int32);
            item.AddItem("UserGroup", System.Data.DbType.System.Data.DbType.Int32);
            AddProcedure(item);

            int formID, int formPowerList, int formUserGroup
            formID, formPowerList, formUserGroup
            protected int formID;
            protected int formPowerList;
            protected int formUserGroup;

            formID = Convert.ToInt32(Request.Form["ID"]);
            formPowerList = Convert.ToInt32(Request.Form["PowerList"]);
            formUserGroup = Convert.ToInt32(Request.Form["UserGroup"]);

            int inID, int inPowerList, int inUserGroup
            inID, inPowerList, inUserGroup
            item.SetItem("ID", inID);
            item.SetItem("PowerList", inPowerList);
            item.SetItem("UserGroup", inUserGroup);

            ID, PowerList, UserGroup
            ID = _item.ID;
            PowerList = _item.PowerList;
            UserGroup = _item.UserGroup;

*/
#endregion
