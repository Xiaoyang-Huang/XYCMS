/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class User : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserGroup { get; set; }

		public static string R(string name) {
		    return "XiaoYang_User_User_" + name;
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

        static User() {
            RegisterProcedures();
            RegisterEvents();
        }


		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("Name", this.Name);
			inItem.SetItem("Nickname", this.Nickname);
			inItem.SetItem("Password", this.Password);
			inItem.SetItem("Email", this.Email);
			inItem.SetItem("UserGroup", this.UserGroup);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "Name", "Nickname", "Password", "Email", "UserGroup" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "Name":
					return this.Name;
				case "Nickname":
					return this.Nickname;
				case "Password":
					return this.Password;
				case "Email":
					return this.Email;
				case "UserGroup":
					return this.UserGroup;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<User>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</User>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@Name nvarchar(32),
	@Nickname nvarchar(64),
	@Password nvarchar(128),
	@Email nvarchar(128),
	@UserGroup int

	[ID] , [Name] , [Nickname] , [Password] , [Email] , [UserGroup]
	@ID , @Name , @Nickname , @Password , @Email , @UserGroup
	[ID] = @ID,
	[Name] = @Name,
	[Nickname] = @Nickname,
	[Password] = @Password,
	[Email] = @Email,
	[UserGroup] = @UserGroup
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("Name", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Nickname", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Password", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Email", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("UserGroup", System.Data.DbType.System.Data.DbType.Int32);
            AddProcedure(item);

            long formID, string formName, string formNickname, string formPassword, string formEmail, int formUserGroup
            formID, formName, formNickname, formPassword, formEmail, formUserGroup
            protected long formID;
            protected string formName;
            protected string formNickname;
            protected string formPassword;
            protected string formEmail;
            protected int formUserGroup;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formName = Request.Form["Name"];
            formNickname = Request.Form["Nickname"];
            formPassword = Request.Form["Password"];
            formEmail = Request.Form["Email"];
            formUserGroup = Convert.ToInt32(Request.Form["UserGroup"]);

            long inID, string inName, string inNickname, string inPassword, string inEmail, int inUserGroup
            inID, inName, inNickname, inPassword, inEmail, inUserGroup
            item.SetItem("ID", inID);
            item.SetItem("Name", inName);
            item.SetItem("Nickname", inNickname);
            item.SetItem("Password", inPassword);
            item.SetItem("Email", inEmail);
            item.SetItem("UserGroup", inUserGroup);

            ID, Name, Nickname, Password, Email, UserGroup
            ID = _item.ID;
            Name = _item.Name;
            Nickname = _item.Nickname;
            Password = _item.Password;
            Email = _item.Email;
            UserGroup = _item.UserGroup;

*/
#endregion
