/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Installation {
    public partial class Installation {

		protected static void RegisterProcedures() {
			Xy.Data.Procedure Add = new Xy.Data.Procedure(R("Add"));
			Add.AddItem("Name", System.Data.DbType.String);
			Add.AddItem("Version", System.Data.DbType.String);
			Add.AddItem("Depend", System.Data.DbType.String);
			Add.AddItem("SQL", System.Data.DbType.String);
			Add.AddItem("Code", System.Data.DbType.String);
			Add.AddItem("Message", System.Data.DbType.String);
			AddProcedure(Add);

			Xy.Data.Procedure GetAll = new Xy.Data.Procedure(R("GetAll"));
			AddProcedure(GetAll);

		}


		#region long Add(string inName, string inVersion, string inDepend, string inSQL, string inCode, string inMessage, Xy.Data.DataBase DB = null)
		public static long Add(string inName, string inVersion, string inDepend, string inSQL, string inCode, string inMessage, Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Installation.Installation.GetProcedure(R("Add"));
			item.SetItem("Name", inName);
			item.SetItem("Version", inVersion);
			item.SetItem("Depend", inDepend);
			item.SetItem("SQL", inSQL);
			item.SetItem("Code", inCode);
			item.SetItem("Message", inMessage);
			return Convert.ToInt64(item.InvokeProcedureResult(DB));
		}
		public static long Add(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return Add(values["Name"], values["Version"], values["Depend"], values["SQL"], values["Code"], values["Message"], DB);
		}
		#endregion

		#region System.Data.DataTable GetAll(Xy.Data.DataBase DB = null)
		public static System.Data.DataTable GetAll(Xy.Data.DataBase DB = null) {
			Xy.Data.Procedure item = XiaoYang.Installation.Installation.GetProcedure(R("GetAll"));
			return item.InvokeProcedureFill(DB);
		}
		public static System.Data.DataTable GetAll(System.Collections.Specialized.NameValueCollection values, Xy.Data.DataBase DB = null) {
			return GetAll(DB);
		}
		#endregion


    }
}
