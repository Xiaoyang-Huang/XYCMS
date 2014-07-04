/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityBase {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("TypeID", System.Data.DbType.Int16);
			Add.AddItem("IsActive", System.Data.DbType.Boolean);
			AddProcedure(Add);

			Xy.Data.Procedure EditActive = new Xy.Data.Procedure(R("EditActive"));
			EditActive.AddItem("ID", System.Data.DbType.Int64);
			EditActive.AddItem("IsActive", System.Data.DbType.Boolean);
			AddProcedure(EditActive);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetList = new Xy.Data.Procedure(R("GetList"));
			GetList.AddItem("PageIndex", System.Data.DbType.Int32);
			GetList.AddItem("PageSize", System.Data.DbType.Int32);
			GetList.AddItem("TypeID", System.Data.DbType.Int16);
			GetList.AddItem("OutWhere", System.Data.DbType.String);
			GetList.AddItem("TotalCount", System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
			AddProcedure(GetList);

		}


		#region long Add(short inTypeID, bool inIsActive, Xy.Data.DataBase DB = null)
		public static long Add(short inTypeID, bool inIsActive, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityBase.GetProcedure(R("Add"));
			item.SetItem("TypeID", inTypeID);
			item.SetItem("IsActive", inIsActive);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt16(values["TypeID"]), Convert.ToBoolean(values["IsActive"]), DB);
		}
		#endregion

		#region int EditActive(long inID, bool inIsActive, Xy.Data.DataBase DB = null)
		public static int EditActive(long inID, bool inIsActive, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityBase.GetProcedure(R("EditActive"));
			item.SetItem("ID", inID);
			item.SetItem("IsActive", inIsActive);
			return item.InvokeProcedure(DB);
		}
		public static int EditActive(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditActive(Convert.ToInt64(values["ID"]), Convert.ToBoolean(values["IsActive"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityBase.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityBase GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityBase temp = new EntityBase();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityBase GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetList(int inPageIndex, int inPageSize, short inTypeID, string inOutWhere, ref int inTotalCount, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetList(int inPageIndex, int inPageSize, short inTypeID, string inOutWhere, ref int inTotalCount, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityBase.GetProcedure(R("GetList"));
			item.SetItem("PageIndex", inPageIndex);
			item.SetItem("PageSize", inPageSize);
			item.SetItem("TypeID", inTypeID);
			item.SetItem("OutWhere", inOutWhere);
			item.SetItem("TotalCount", inTotalCount);
			System.Data.DataTable result = item.InvokeProcedureFill(DB);
			inTotalCount = Convert.ToInt32(item.GetItem("TotalCount"));
			return result;
		}
		public static System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection values, ref int inTotalCount, Xy.Data.DataBase DB = null) {
			return GetList(Convert.ToInt32(values["PageIndex"]), Convert.ToInt32(values["PageSize"]), Convert.ToInt16(values["TypeID"]), values["OutWhere"], ref inTotalCount, DB);
		}
		#endregion


    }
}
