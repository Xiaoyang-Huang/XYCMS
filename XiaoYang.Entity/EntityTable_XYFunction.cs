/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityTable {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("TypeID", System.Data.DbType.Int64);
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Key", System.Data.DbType.String);
			Add.AddItem("Main", System.Data.DbType.Boolean);
			Add.AddItem("Multiply", System.Data.DbType.Boolean);
			Add.AddItem("Description", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetListByTypeID = new Xy.Data.Procedure(R("GetListByTypeID"));
			GetListByTypeID.AddItem("TypeID", System.Data.DbType.Int64);
			AddProcedure(GetListByTypeID);

		}


		#region long Add(long inTypeID, string inName, string inKey, bool inMain, bool inMultiply, string inDescription, Xy.Data.DataBase DB = null)
		public static long Add(long inTypeID, string inName, string inKey, bool inMain, bool inMultiply, string inDescription, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityTable.GetProcedure(R("Add"));
			item.SetItem("TypeID", inTypeID);
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("Main", inMain);
			item.SetItem("Multiply", inMultiply);
			item.SetItem("Description", inDescription);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt64(values["TypeID"]), values["Name"], values["Key"], Convert.ToBoolean(values["Main"]), Convert.ToBoolean(values["Multiply"]), values["Description"], DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityTable.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityTable.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityTable GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityTable temp = new EntityTable();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityTable GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetListByTypeID(long inTypeID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetListByTypeID(long inTypeID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityTable.GetProcedure(R("GetListByTypeID"));
			item.SetItem("TypeID", inTypeID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetListByTypeID(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetListByTypeID(Convert.ToInt64(values["TypeID"]), DB);
		}
		#endregion


    }
}
