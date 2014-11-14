/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityField {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("TableID", System.Data.DbType.Int64);
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Key", System.Data.DbType.String);
			Add.AddItem("Type", System.Data.DbType.String);
			Add.AddItem("Default", System.Data.DbType.String);
			Add.AddItem("Template", System.Data.DbType.Int64);
			Add.AddItem("Primary", System.Data.DbType.Boolean);
			Add.AddItem("Increase", System.Data.DbType.Boolean);
			Add.AddItem("Null", System.Data.DbType.Boolean);
			Add.AddItem("Foreign", System.Data.DbType.Boolean);
			Add.AddItem("Description", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetListByTabldID = new Xy.Data.Procedure(R("GetListByTabldID"));
			GetListByTabldID.AddItem("TableID", System.Data.DbType.Int64);
			AddProcedure(GetListByTabldID);

		}


		#region long Add(long inTableID, string inName, string inKey, string inType, string inDefault, long inTemplate, bool inPrimary, bool inIncrease, bool inNull, bool inForeign, string inDescription, Xy.Data.DataBase DB = null)
		public static long Add(long inTableID, string inName, string inKey, string inType, string inDefault, long inTemplate, bool inPrimary, bool inIncrease, bool inNull, bool inForeign, string inDescription, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityField.GetProcedure(R("Add"));
			item.SetItem("TableID", inTableID);
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("Type", inType);
			item.SetItem("Default", inDefault);
			item.SetItem("Template", inTemplate);
			item.SetItem("Primary", inPrimary);
			item.SetItem("Increase", inIncrease);
			item.SetItem("Null", inNull);
			item.SetItem("Foreign", inForeign);
			item.SetItem("Description", inDescription);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt64(values["TableID"]), values["Name"], values["Key"], values["Type"], values["Default"], Convert.ToInt64(values["Template"]), Convert.ToBoolean(values["Primary"]), Convert.ToBoolean(values["Increase"]), Convert.ToBoolean(values["Null"]), Convert.ToBoolean(values["Foreign"]), values["Description"], DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityField.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityField.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityField GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityField temp = new EntityField();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityField GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetListByTabldID(long inTableID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetListByTabldID(long inTableID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityField.GetProcedure(R("GetListByTabldID"));
			item.SetItem("TableID", inTableID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetListByTabldID(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetListByTabldID(Convert.ToInt64(values["TableID"]), DB);
		}
		#endregion


    }
}
