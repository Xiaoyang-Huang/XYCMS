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
			Add.AddItem("ParentTypeID", System.Data.DbType.Int16);
			Add.AddItem("Description", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int16);
			AddProcedure(Del);

			Xy.Data.Procedure EditActive = new Xy.Data.Procedure(R("EditActive"));
			EditActive.AddItem("ID", System.Data.DbType.Int16);
			EditActive.AddItem("IsActive", System.Data.DbType.Boolean);
			AddProcedure(EditActive);

			Xy.Data.Procedure EditBaseKey = new Xy.Data.Procedure(R("EditBaseKey"));
			EditBaseKey.AddItem("ID", System.Data.DbType.Int16);
			EditBaseKey.AddItem("BaseKey", System.Data.DbType.String);
			AddProcedure(EditBaseKey);

			Xy.Data.Procedure EditDisplay = new Xy.Data.Procedure(R("EditDisplay"));
			EditDisplay.AddItem("ID", System.Data.DbType.Int16);
			EditDisplay.AddItem("IsDisplay", System.Data.DbType.Boolean);
			AddProcedure(EditDisplay);

			Xy.Data.Procedure EditUpdateTime = new Xy.Data.Procedure(R("EditUpdateTime"));
			EditUpdateTime.AddItem("ID", System.Data.DbType.Int16);
			AddProcedure(EditUpdateTime);

			Xy.Data.Procedure EditWebRelated = new Xy.Data.Procedure(R("EditWebRelated"));
			EditWebRelated.AddItem("ID", System.Data.DbType.Int16);
			EditWebRelated.AddItem("IsWebRelated", System.Data.DbType.Boolean);
			AddProcedure(EditWebRelated);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int16);
			AddProcedure(Get);

			Xy.Data.Procedure GetChildType = new Xy.Data.Procedure(R("GetChildType"));
			GetChildType.AddItem("ID", System.Data.DbType.Int16);
			AddProcedure(GetChildType);

		}


		#region short Add(string inName, string inKey, short inParentTypeID, string inDescription, Xy.Data.DataBase DB = null)
		public static short Add(string inName, string inKey, short inParentTypeID, string inDescription, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("ParentTypeID", inParentTypeID);
			item.SetItem("Description", inDescription);
			return Convert.ToInt16(item.InvokeProcedureResult(DB));
		}
		public static short Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Key"], Convert.ToInt16(values["ParentTypeID"]), values["Description"], DB);
		}
		#endregion

		#region int Del(short inID, Xy.Data.DataBase DB = null)
		public static int Del(short inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt16(values["ID"]), DB);
		}
		#endregion

		#region int EditActive(short inID, bool inIsActive, Xy.Data.DataBase DB = null)
		public static int EditActive(short inID, bool inIsActive, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditActive"));
			item.SetItem("ID", inID);
			item.SetItem("IsActive", inIsActive);
			return item.InvokeProcedure(DB);
		}
		public static int EditActive(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditActive(Convert.ToInt16(values["ID"]), Convert.ToBoolean(values["IsActive"]), DB);
		}
		#endregion

		#region int EditBaseKey(short inID, string inBaseKey, Xy.Data.DataBase DB = null)
		public static int EditBaseKey(short inID, string inBaseKey, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditBaseKey"));
			item.SetItem("ID", inID);
			item.SetItem("BaseKey", inBaseKey);
			return item.InvokeProcedure(DB);
		}
		public static int EditBaseKey(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditBaseKey(Convert.ToInt16(values["ID"]), values["BaseKey"], DB);
		}
		#endregion

		#region int EditDisplay(short inID, bool inIsDisplay, Xy.Data.DataBase DB = null)
		public static int EditDisplay(short inID, bool inIsDisplay, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditDisplay"));
			item.SetItem("ID", inID);
			item.SetItem("IsDisplay", inIsDisplay);
			return item.InvokeProcedure(DB);
		}
		public static int EditDisplay(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditDisplay(Convert.ToInt16(values["ID"]), Convert.ToBoolean(values["IsDisplay"]), DB);
		}
		#endregion

		#region int EditUpdateTime(short inID, Xy.Data.DataBase DB = null)
		public static int EditUpdateTime(short inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditUpdateTime"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int EditUpdateTime(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditUpdateTime(Convert.ToInt16(values["ID"]), DB);
		}
		#endregion

		#region int EditWebRelated(short inID, bool inIsWebRelated, Xy.Data.DataBase DB = null)
		public static int EditWebRelated(short inID, bool inIsWebRelated, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("EditWebRelated"));
			item.SetItem("ID", inID);
			item.SetItem("IsWebRelated", inIsWebRelated);
			return item.InvokeProcedure(DB);
		}
		public static int EditWebRelated(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return EditWebRelated(Convert.ToInt16(values["ID"]), Convert.ToBoolean(values["IsWebRelated"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(short inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(short inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt16(values["ID"]), DB);
		}
		public static EntityType GetInstance(short inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityType temp = new EntityType();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityType GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt16(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetChildType(short inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetChildType(short inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityType.GetProcedure(R("GetChildType"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetChildType(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetChildType(Convert.ToInt16(values["ID"]), DB);
		}
		#endregion


    }
}
