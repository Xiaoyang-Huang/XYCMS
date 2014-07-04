/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class User {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Nickname", System.Data.DbType.String);
			Add.AddItem("Password", System.Data.DbType.String);
			Add.AddItem("Email", System.Data.DbType.String);
			Add.AddItem("UserGroup", System.Data.DbType.Int32);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure EditNickname = new Xy.Data.Procedure(R("EditNickname"));
			EditNickname.AddItem("ID", System.Data.DbType.Int64);
			EditNickname.AddItem("Nickname", System.Data.DbType.String);
			AddProcedure(EditNickname);

			Xy.Data.Procedure EditPassword = new Xy.Data.Procedure(R("EditPassword"));
			EditPassword.AddItem("ID", System.Data.DbType.Int64);
			EditPassword.AddItem("Password", System.Data.DbType.String);
			AddProcedure(EditPassword);

			Xy.Data.Procedure EditUserGroup = new Xy.Data.Procedure(R("EditUserGroup"));
			EditUserGroup.AddItem("ID", System.Data.DbType.Int64);
			EditUserGroup.AddItem("UserGroup", System.Data.DbType.Int32);
			AddProcedure(EditUserGroup);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetByNickname = new Xy.Data.Procedure(R("GetByNickname"));
			GetByNickname.AddItem("Nickname", System.Data.DbType.String);
			AddProcedure(GetByNickname);

			Xy.Data.Procedure GetList = new Xy.Data.Procedure(R("GetList"));
			GetList.AddItem("PageIndex", System.Data.DbType.Int32);
			GetList.AddItem("PageSize", System.Data.DbType.Int32);
			GetList.AddItem("Nickname", System.Data.DbType.String);
			GetList.AddItem("Name", System.Data.DbType.String);
			GetList.AddItem("Email", System.Data.DbType.String);
			GetList.AddItem("UserGrouop", System.Data.DbType.Int32);
			AddProcedure(GetList);

			Xy.Data.Procedure Login = new Xy.Data.Procedure(R("Login"));
			Login.AddItem("Name", System.Data.DbType.String);
			Login.AddItem("Password", System.Data.DbType.String);
			AddProcedure(Login);

		}


		#region long Add(string inName, string inNickname, string inPassword, string inEmail, int inUserGroup, Xy.Data.DataBase DB = null)
		public static long Add(string inName, string inNickname, string inPassword, string inEmail, int inUserGroup, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Nickname", inNickname);
			item.SetItem("Password", inPassword);
			item.SetItem("Email", inEmail);
			item.SetItem("UserGroup", inUserGroup);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Nickname"], values["Password"], values["Email"], Convert.ToInt32(values["UserGroup"]), DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region int EditNickname(long inID, string inNickname, Xy.Data.DataBase DB = null)
		public static int EditNickname(long inID, string inNickname, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("EditNickname"));
			item.SetItem("ID", inID);
			item.SetItem("Nickname", inNickname);
			return item.InvokeProcedure(DB);
		}
		public static int EditNickname(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditNickname(Convert.ToInt64(values["ID"]), values["Nickname"], DB);
		}
		#endregion

		#region int EditPassword(long inID, string inPassword, Xy.Data.DataBase DB = null)
		public static int EditPassword(long inID, string inPassword, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("EditPassword"));
			item.SetItem("ID", inID);
			item.SetItem("Password", inPassword);
			return item.InvokeProcedure(DB);
		}
		public static int EditPassword(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditPassword(Convert.ToInt64(values["ID"]), values["Password"], DB);
		}
		#endregion

		#region int EditUserGroup(long inID, int inUserGroup, Xy.Data.DataBase DB = null)
		public static int EditUserGroup(long inID, int inUserGroup, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("EditUserGroup"));
			item.SetItem("ID", inID);
			item.SetItem("UserGroup", inUserGroup);
			return item.InvokeProcedure(DB);
		}
		public static int EditUserGroup(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditUserGroup(Convert.ToInt64(values["ID"]), Convert.ToInt32(values["UserGroup"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static User GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				User temp = new User();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static User GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetByNickname(string inNickname, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetByNickname(string inNickname, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("GetByNickname"));
			item.SetItem("Nickname", inNickname);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetByNickname(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetByNickname(values["Nickname"], DB);
		}
		#endregion

		#region System.Data.DataTable GetList(int inPageIndex, int inPageSize, string inNickname, string inName, string inEmail, int inUserGrouop, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetList(int inPageIndex, int inPageSize, string inNickname, string inName, string inEmail, int inUserGrouop, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.User.GetProcedure(R("GetList"));
			item.SetItem("PageIndex", inPageIndex);
			item.SetItem("PageSize", inPageSize);
			item.SetItem("Nickname", inNickname);
			item.SetItem("Name", inName);
			item.SetItem("Email", inEmail);
			item.SetItem("UserGrouop", inUserGrouop);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetList(Convert.ToInt32(values["PageIndex"]), Convert.ToInt32(values["PageSize"]), values["Nickname"], values["Name"], values["Email"], Convert.ToInt32(values["UserGrouop"]), DB);
		}
		#endregion



    }
}
