/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityType {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Key", System.Data.DbType.String);
			Add.AddItem("Handle", System.Data.DbType.String);
			Add.AddItem("Description", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure EditActive = new Xy.Data.Procedure(R("EditActive"));
			EditActive.AddItem("ID", System.Data.DbType.Int64);
			EditActive.AddItem("IsActive", System.Data.DbType.Boolean);
			AddProcedure(EditActive);

			Xy.Data.Procedure EditAvailable = new Xy.Data.Procedure(R("EditAvailable"));
			EditAvailable.AddItem("ID", System.Data.DbType.Int64);
			EditAvailable.AddItem("IsAvailable", System.Data.DbType.Boolean);
			AddProcedure(EditAvailable);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetAll = new Xy.Data.Procedure(R("GetAll"));
			AddProcedure(GetAll);

		}


		#region long Add(string inName, string inKey, string inHandle, string inDescription, Xy.Data.DataBase DB = null)
		public static long Add(string inName, string inKey, string inHandle, string inDescription, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("Handle", inHandle);
			item.SetItem("Description", inDescription);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Key"], values["Handle"], values["Description"], DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region int EditActive(long inID, bool inIsActive, Xy.Data.DataBase DB = null)
		public static int EditActive(long inID, bool inIsActive, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditActive"));
			item.SetItem("ID", inID);
			item.SetItem("IsActive", inIsActive);
			return item.InvokeProcedure(DB);
		}
		public static int EditActive(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditActive(Convert.ToInt64(values["ID"]), Convert.ToBoolean(values["IsActive"]), DB);
		}
		#endregion

		#region int EditAvailable(long inID, bool inIsAvailable, Xy.Data.DataBase DB = null)
		public static int EditAvailable(long inID, bool inIsAvailable, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditAvailable"));
			item.SetItem("ID", inID);
			item.SetItem("IsAvailable", inIsAvailable);
			return item.InvokeProcedure(DB);
		}
		public static int EditAvailable(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditAvailable(Convert.ToInt64(values["ID"]), Convert.ToBoolean(values["IsAvailable"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityType GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityType temp = new EntityType();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityType GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetAll(Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetAll(Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("GetAll"));
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetAll(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetAll(DB);
		}
		#endregion


    }
}
