/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityAttributeDisplay {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Resource", System.Data.DbType.String);
			Add.AddItem("Template", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Del);

			Xy.Data.Procedure Edit = new Xy.Data.Procedure(R("Edit"));
			Edit.AddItem("ID", System.Data.DbType.Int64);
			Edit.AddItem("Name", System.Data.DbType.String);
			Edit.AddItem("Resource", System.Data.DbType.String);
			Edit.AddItem("Template", System.Data.DbType.String);
			AddProcedure(Edit);

			Xy.Data.Procedure Get = new Xy.Data.Procedure(R("Get"));
			Get.AddItem("ID", System.Data.DbType.Int64);
			AddProcedure(Get);

		}


		#region long Add(string inName, string inResource, string inTemplate, Xy.Data.DataBase DB = null)
		public static long Add(string inName, string inResource, string inTemplate, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttributeDisplay.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Resource", inResource);
			item.SetItem("Template", inTemplate);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Resource"], values["Template"], DB);
		}
		#endregion

		#region int Del(long inID, Xy.Data.DataBase DB = null)
		public static int Del(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttributeDisplay.GetProcedure(R("Del"));
			item.SetItem("ID", inID);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion

		#region int Edit(long inID, string inName, string inResource, string inTemplate, Xy.Data.DataBase DB = null)
		public static int Edit(long inID, string inName, string inResource, string inTemplate, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttributeDisplay.GetProcedure(R("Edit"));
			item.SetItem("ID", inID);
			item.SetItem("Name", inName);
			item.SetItem("Resource", inResource);
			item.SetItem("Template", inTemplate);
			return item.InvokeProcedure(DB);
		}
		public static int Edit(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Edit(Convert.ToInt64(values["ID"]), values["Name"], values["Resource"], values["Template"], DB);
		}
		#endregion

		#region System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null)
		public static System.Data.DataTable Get(long inID, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Entity.EntityAttributeDisplay.GetProcedure(R("Get"));
			item.SetItem("ID", inID);
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Get(Convert.ToInt64(values["ID"]), DB);
		}
		public static EntityAttributeDisplay GetInstance(long inID, Xy.Data.DataBase DB = null) {
			System.Data.DataTable result = Get(inID, DB);
			if (result.Rows.Count > 0) {
				EntityAttributeDisplay temp = new EntityAttributeDisplay();
				temp.Fill(result.Rows[0]);
				return temp;
			}
			return null;
		}
		public static EntityAttributeDisplay GetInstance(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetInstance(Convert.ToInt64(values["ID"]), DB);
		}
		#endregion


    }
}
