/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.User {
    public partial class PowerShip {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("PowerList", System.Data.DbType.Int32);
			Add.AddItem("UserGroup", System.Data.DbType.Int32);
			AddProcedure(Add);

			Xy.Data.Procedure Del = new Xy.Data.Procedure(R("Del"));
			Del.AddItem("PowerList", System.Data.DbType.Int32);
			Del.AddItem("UserGroup", System.Data.DbType.Int32);
			AddProcedure(Del);

			Xy.Data.Procedure DelByGroup = new Xy.Data.Procedure(R("DelByGroup"));
			DelByGroup.AddItem("UserGroup", System.Data.DbType.Int32);
			AddProcedure(DelByGroup);

			Xy.Data.Procedure GetAll = new Xy.Data.Procedure(R("GetAll"));
			AddProcedure(GetAll);

		}


		#region int Add(int inPowerList, int inUserGroup, Xy.Data.DataBase DB = null)
		public static int Add(int inPowerList, int inUserGroup, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.PowerShip.GetProcedure(R("Add"));
			item.SetItem("PowerList", inPowerList);
			item.SetItem("UserGroup", inUserGroup);
			return Convert.ToInt32(item.InvokeProcedureResult(DB));
		}
		public static int Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(Convert.ToInt32(values["PowerList"]), Convert.ToInt32(values["UserGroup"]), DB);
		}
		#endregion

		#region int Del(int inPowerList, int inUserGroup, Xy.Data.DataBase DB = null)
		public static int Del(int inPowerList, int inUserGroup, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.PowerShip.GetProcedure(R("Del"));
			item.SetItem("PowerList", inPowerList);
			item.SetItem("UserGroup", inUserGroup);
			return item.InvokeProcedure(DB);
		}
		public static int Del(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Del(Convert.ToInt32(values["PowerList"]), Convert.ToInt32(values["UserGroup"]), DB);
		}
		#endregion

		#region int DelByGroup(int inUserGroup, Xy.Data.DataBase DB = null)
		public static int DelByGroup(int inUserGroup, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.PowerShip.GetProcedure(R("DelByGroup"));
			item.SetItem("UserGroup", inUserGroup);
			return item.InvokeProcedure(DB);
		}
		public static int DelByGroup(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return DelByGroup(Convert.ToInt32(values["UserGroup"]), DB);
		}
		#endregion

		#region System.Data.DataTable GetAll(Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetAll(Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.User.PowerShip.GetProcedure(R("GetAll"));
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetAll(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetAll(DB);
		}
		#endregion


    }
}
