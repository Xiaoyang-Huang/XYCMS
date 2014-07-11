/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityAttribute {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("TypeID", System.Data.DbType.Int16);
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Key", System.Data.DbType.String);
			Add.AddItem("Type", System.Data.DbType.String);
			Add.AddItem("Default", System.Data.DbType.String);
			Add.AddItem("Display", System.Data.DbType.Int64);
			Add.AddItem("IsNull", System.Data.DbType.Boolean);
			Add.AddItem("Description", System.Data.DbType.String);
			Add.AddItem("IsPrimary", System.Data.DbType.Boolean);
			Add.AddItem("IsIncrease", System.Data.DbType.Boolean);
			Add.AddItem("FKID", System.Data.DbType.Int64);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure Edit = new Xy.Data.Procedure(R("Edit"));
			Edit.AddItem("ID", System.Data.DbType.Int64);
			Edit.AddItem("Name", System.Data.DbType.String);
			Edit.AddItem("Key", System.Data.DbType.String);
			Edit.AddItem("Type", System.Data.DbType.String);
			Edit.AddItem("Default", System.Data.DbType.String);
			Edit.AddItem("Display", System.Data.DbType.Int64);
			Edit.AddItem("IsNull", System.Data.DbType.Boolean);
			Edit.AddItem("Description", System.Data.DbType.String);
			Edit.AddItem("IsPrimary", System.Data.DbType.Boolean);
			Edit.AddItem("IsIncrease", System.Data.DbType.Boolean);
			Edit.AddItem("FKID", System.Data.DbType.Int64);
			AddProcedure(Edit);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

			Xy.Data.Procedure GetByTypeID = new Xy.Data.Procedure(R("GetByTypeID"));
			GetByTypeID.AddItem("TypeID", System.Data.DbType.Int16);
			AddProcedure(GetByTypeID);

		}


		#region long Add(short inTypeID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsNull, string inDescription, bool inIsPrimary, bool inIsIncrease, long inFKID, Xy.Data.DataBase DB = null)
		public static long Add(short inTypeID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsNull, string inDescription, bool inIsPrimary, bool inIsIncrease, long inFKID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttribute.GetProcedure(R("Add"));
			item.SetItem("TypeID", inTypeID);
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("Type", inType);
			item.SetItem("Default", inDefault);
			item.SetItem("Display", inDisplay);
			item.SetItem("IsNull", inIsNull);
			item.SetItem("Description", inDescription);
			item.SetItem("IsPrimary", inIsPrimary);
			item.SetItem("IsIncrease", inIsIncrease);
			item.SetItem("FKID", inFKID);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt16(values["TypeID"]), values["Name"], values["Key"], values["Type"], values["Default"], Convert.ToInt64(values["Display"]), Convert.ToBoolean(values["IsNull"]), values["Description"], Convert.ToBoolean(values["IsPrimary"]), Convert.ToBoolean(values["IsIncrease"]), Convert.ToInt64(values["FKID"]), DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttribute.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region int Edit(long inID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsNull, string inDescription, bool inIsPrimary, bool inIsIncrease, long inFKID, Xy.Data.DataBase DB = null)
		public static int Edit(long inID, string inName, string inKey, string inType, string inDefault, long inDisplay, bool inIsNull, string inDescription, bool inIsPrimary, bool inIsIncrease, long inFKID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttribute.GetProcedure(R("Edit"));
			item.SetItem("ID", inID);
			item.SetItem("Name", inName);
			item.SetItem("Key", inKey);
			item.SetItem("Type", inType);
			item.SetItem("Default", inDefault);
			item.SetItem("Display", inDisplay);
			item.SetItem("IsNull", inIsNull);
			item.SetItem("Description", inDescription);
			item.SetItem("IsPrimary", inIsPrimary);
			item.SetItem("IsIncrease", inIsIncrease);
			item.SetItem("FKID", inFKID);
			return item.InvokeProcedure(DB);
		}
		public static int Edit(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Edit(Convert.ToInt64(values["ID"]), values["Name"], values["Key"], values["Type"], values["Default"], Convert.ToInt64(values["Display"]), Convert.ToBoolean(values["IsNull"]), values["Description"], Convert.ToBoolean(values["IsPrimary"]), Convert.ToBoolean(values["IsIncrease"]), Convert.ToInt64(values["FKID"]), DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttribute.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityAttribute GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityAttribute temp = new EntityAttribute();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityAttribute GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetByTypeID(short inTypeID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetByTypeID(short inTypeID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttribute.GetProcedure(R("GetByTypeID"));
			item.SetItem("TypeID", inTypeID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetByTypeID(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetByTypeID(Convert.ToInt16(values["TypeID"]), DB);
		}
		#endregion


    }
}
