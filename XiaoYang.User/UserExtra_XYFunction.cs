/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class UserExtra {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("UserID", System.Data.DbType.Int64);
			Add.AddItem("HeadImage", System.Data.DbType.String);
			Add.AddItem("Sex", System.Data.DbType.Int16);
			Add.AddItem("Birthday", System.Data.DbType.DateTime);
			Add.AddItem("Hometown", System.Data.DbType.String);
			Add.AddItem("Description", System.Data.DbType.String);
			Add.AddItem("Job", System.Data.DbType.String);
			Add.AddItem("Weibo", System.Data.DbType.String);
			Add.AddItem("QQ", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("UserID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure Edit = new Xy.Data.Procedure(R("Edit"));
			Edit.AddItem("UserID", System.Data.DbType.Int64);
			Edit.AddItem("HeadImage", System.Data.DbType.String);
			Edit.AddItem("Sex", System.Data.DbType.Int16);
			Edit.AddItem("Birthday", System.Data.DbType.DateTime);
			Edit.AddItem("Hometown", System.Data.DbType.String);
			Edit.AddItem("Description", System.Data.DbType.String);
			Edit.AddItem("Job", System.Data.DbType.String);
			Edit.AddItem("Weibo", System.Data.DbType.String);
			Edit.AddItem("QQ", System.Data.DbType.String);
			AddProcedure(Edit);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("UserID", System.Data.DbType.Int64);
			AddProcedure(Get);

		}


		#region long Add(long inUserID, string inHeadImage, short inSex, DateTime inBirthday, string inHometown, string inDescription, string inJob, string inWeibo, string inQQ, Xy.Data.DataBase DB = null)
		public static long Add(long inUserID, string inHeadImage, short inSex, DateTime inBirthday, string inHometown, string inDescription, string inJob, string inWeibo, string inQQ, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserExtra.GetProcedure(R("Add"));
			item.SetItem("UserID", inUserID);
			item.SetItem("HeadImage", inHeadImage);
			item.SetItem("Sex", inSex);
			item.SetItem("Birthday", inBirthday);
			item.SetItem("Hometown", inHometown);
			item.SetItem("Description", inDescription);
			item.SetItem("Job", inJob);
			item.SetItem("Weibo", inWeibo);
			item.SetItem("QQ", inQQ);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt64(values["UserID"]), values["HeadImage"], Convert.ToInt16(values["Sex"]), Convert.ToDateTime(values["Birthday"]), values["Hometown"], values["Description"], values["Job"], values["Weibo"], values["QQ"], DB);
		}
		#endregion

		#region int Del(long inUserID, Xy.Data.DataBase DB = null)
		public static int Del(long inUserID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserExtra.GetProcedure(R("Del"));
			item.SetItem("UserID", inUserID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["UserID"]), DB);
		}
		#endregion

		#region int Edit(long inUserID, string inHeadImage, short inSex, DateTime inBirthday, string inHometown, string inDescription, string inJob, string inWeibo, string inQQ, Xy.Data.DataBase DB = null)
		public static int Edit(long inUserID, string inHeadImage, short inSex, DateTime inBirthday, string inHometown, string inDescription, string inJob, string inWeibo, string inQQ, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserExtra.GetProcedure(R("Edit"));
			item.SetItem("UserID", inUserID);
			item.SetItem("HeadImage", inHeadImage);
			item.SetItem("Sex", inSex);
			item.SetItem("Birthday", inBirthday);
			item.SetItem("Hometown", inHometown);
			item.SetItem("Description", inDescription);
			item.SetItem("Job", inJob);
			item.SetItem("Weibo", inWeibo);
			item.SetItem("QQ", inQQ);
			return item.InvokeProcedure(DB);
		}
		public static int Edit(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Edit(Convert.ToInt64(values["UserID"]), values["HeadImage"], Convert.ToInt16(values["Sex"]), Convert.ToDateTime(values["Birthday"]), values["Hometown"], values["Description"], values["Job"], values["Weibo"], values["QQ"], DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inUserID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inUserID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserExtra.GetProcedure(R("Get"));
			item.SetItem("UserID", inUserID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["UserID"]), DB);
		}
		public static UserExtra GetInstance(long inUserID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inUserID, DB);
			if (result.Rows.Count > 0) {
				UserExtra temp = new UserExtra();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static UserExtra GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["UserID"]), DB);
		}
		#endregion


    }
}
