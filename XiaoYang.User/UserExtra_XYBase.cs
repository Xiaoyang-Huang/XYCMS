/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class UserExtra : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long UserID { get; set; }
        public string HeadImage { get; set; }
        public short Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Hometown { get; set; }
        public string Job { get; set; }
        public string Weibo { get; set; }
        public string QQ { get; set; }
        public string Description { get; set; }

		public static string R(string name) {
		    return "XiaoYang_User_UserExtra_" + name;
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

        static UserExtra() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["UserID"] != null) { this.UserID = Convert.ToInt64(inTempRow["UserID"]); }
			if (cols["HeadImage"] != null) { this.HeadImage = Convert.ToString(inTempRow["HeadImage"]); }
			if (cols["Sex"] != null) { this.Sex = Convert.ToInt16(inTempRow["Sex"]); }
			if (cols["Birthday"] != null) { this.Birthday = Convert.ToDateTime(inTempRow["Birthday"]); }
			if (cols["Hometown"] != null) { this.Hometown = Convert.ToString(inTempRow["Hometown"]); }
			if (cols["Job"] != null) { this.Job = Convert.ToString(inTempRow["Job"]); }
			if (cols["Weibo"] != null) { this.Weibo = Convert.ToString(inTempRow["Weibo"]); }
			if (cols["QQ"] != null) { this.QQ = Convert.ToString(inTempRow["QQ"]); }
			if (cols["Description"] != null) { this.Description = Convert.ToString(inTempRow["Description"]); }
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("UserID", this.UserID);
			inItem.SetItem("HeadImage", this.HeadImage);
			inItem.SetItem("Sex", this.Sex);
			inItem.SetItem("Birthday", this.Birthday);
			inItem.SetItem("Hometown", this.Hometown);
			inItem.SetItem("Job", this.Job);
			inItem.SetItem("Weibo", this.Weibo);
			inItem.SetItem("QQ", this.QQ);
			inItem.SetItem("Description", this.Description);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "UserID", "HeadImage", "Sex", "Birthday", "Hometown", "Job", "Weibo", "QQ", "Description" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "UserID":
					return this.UserID;
				case "HeadImage":
					return this.HeadImage;
				case "Sex":
					return this.Sex;
				case "Birthday":
					return this.Birthday;
				case "Hometown":
					return this.Hometown;
				case "Job":
					return this.Job;
				case "Weibo":
					return this.Weibo;
				case "QQ":
					return this.QQ;
				case "Description":
					return this.Description;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<UserExtra>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</UserExtra>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@UserID bigint,
	@HeadImage nvarchar(512),
	@Sex smallint,
	@Birthday date,
	@Hometown nvarchar(128),
	@Job nvarchar(128),
	@Weibo nvarchar(256),
	@QQ nvarchar(32),
	@Description nvarchar(512)

	[UserID] , [HeadImage] , [Sex] , [Birthday] , [Hometown] , [Job] , [Weibo] , [QQ] , [Description]
	@UserID , @HeadImage , @Sex , @Birthday , @Hometown , @Job , @Weibo , @QQ , @Description
	[UserID] = @UserID,
	[HeadImage] = @HeadImage,
	[Sex] = @Sex,
	[Birthday] = @Birthday,
	[Hometown] = @Hometown,
	[Job] = @Job,
	[Weibo] = @Weibo,
	[QQ] = @QQ,
	[Description] = @Description
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("UserID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("HeadImage", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Sex", System.Data.DbType.System.Data.DbType.Int16);
            item.AddItem("Birthday", System.Data.DbType.System.Data.DbType.DateTime);
            item.AddItem("Hometown", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Job", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Weibo", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("QQ", System.Data.DbType.System.Data.DbType.String);
            item.AddItem("Description", System.Data.DbType.System.Data.DbType.String);
            AddProcedure(item);

            long formUserID, string formHeadImage, short formSex, DateTime formBirthday, string formHometown, string formJob, string formWeibo, string formQQ, string formDescription
            formUserID, formHeadImage, formSex, formBirthday, formHometown, formJob, formWeibo, formQQ, formDescription
            protected long formUserID;
            protected string formHeadImage;
            protected short formSex;
            protected DateTime formBirthday;
            protected string formHometown;
            protected string formJob;
            protected string formWeibo;
            protected string formQQ;
            protected string formDescription;

            formUserID = Convert.ToInt64(Request.Form["UserID"]);
            formHeadImage = Request.Form["HeadImage"];
            formSex = Convert.ToInt16(Request.Form["Sex"]);
            formBirthday = Convert.ToDateTime(Request.Form["Birthday"]);
            formHometown = Request.Form["Hometown"];
            formJob = Request.Form["Job"];
            formWeibo = Request.Form["Weibo"];
            formQQ = Request.Form["QQ"];
            formDescription = Request.Form["Description"];

            long inUserID, string inHeadImage, short inSex, DateTime inBirthday, string inHometown, string inJob, string inWeibo, string inQQ, string inDescription
            inUserID, inHeadImage, inSex, inBirthday, inHometown, inJob, inWeibo, inQQ, inDescription
            item.SetItem("UserID", inUserID);
            item.SetItem("HeadImage", inHeadImage);
            item.SetItem("Sex", inSex);
            item.SetItem("Birthday", inBirthday);
            item.SetItem("Hometown", inHometown);
            item.SetItem("Job", inJob);
            item.SetItem("Weibo", inWeibo);
            item.SetItem("QQ", inQQ);
            item.SetItem("Description", inDescription);

            UserID, HeadImage, Sex, Birthday, Hometown, Job, Weibo, QQ, Description
            UserID = _item.UserID;
            HeadImage = _item.HeadImage;
            Sex = _item.Sex;
            Birthday = _item.Birthday;
            Hometown = _item.Hometown;
            Job = _item.Job;
            Weibo = _item.Weibo;
            QQ = _item.QQ;
            Description = _item.Description;

*/
#endregion
