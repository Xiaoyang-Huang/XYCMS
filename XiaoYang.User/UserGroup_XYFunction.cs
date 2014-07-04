/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class UserGroup {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Key", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int32);
			AddProcedure(Del);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int32);
			AddProcedure(Get);

			Xy.Data.Procedure GetAll = new Xy.Data.Procedure(R("GetAll"));
			AddProcedure(GetAll);

		}


		#region int Add(string inName, string inKey, Xy.Data.DataBase DB = null)
		public static int Add(string inName, string inKey, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserGroup.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			return Convert.ToInt32(item.InvokeProcedureResult(DB));
		}
		public static int Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Key"], DB);
		}
		#endregion

		#region int Del(int inID, Xy.Data.DataBase DB = null)
		public static int Del(int inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserGroup.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt32(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(int inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(int inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserGroup.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt32(values["ID"]), DB);
		}
		public static UserGroup GetInstance(int inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				UserGroup temp = new UserGroup();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static UserGroup GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt32(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetAll(Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetAll(Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.UserGroup.GetProcedure(R("GetAll"));
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetAll(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetAll(DB);
		}
		#endregion


    }
}
