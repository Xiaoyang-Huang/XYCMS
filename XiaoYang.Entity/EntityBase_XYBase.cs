/****************************************************************************
 * 
 *  all code on the blow is builded by XyFrameDataModuleBuilder 1.0.0.0 version
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


namespace XiaoYang.Entity {
    public partial class EntityBase : Xy.Data.IDataModel, Xy.Data.IDataModelDisplay {
        public long ID { get; set; }
        public short TypeID { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdateTime { get; set; }

		public static string R(string name) {
		    return "XiaoYang_Entity_EntityBase_" + name;
		}

        private static Dictionary<string, Xy.Data.Procedure> _procedures;
		private static void AddProcedure(Xy.Data.Procedure _inValue) {
			if (_procedures == null) _procedures = new Dictionary<string, Xy.Data.Procedure>();
			_procedures.Add(_inValue.Name, _inValue);
		}

		private static Xy.Data.Procedure GetProcedure(string name) {
			if (_procedures == null) _procedures = new Dictionary<string,Xy.Data.Procedure>();
			if (_procedures.ContainsKey(name)) {
				return _procedures[name].Clone();
			} else {
				throw new Exception(string.Format("can not found \"{0}\" in procedure list", name));
			}
		}

        static EntityBase() {
            RegisterProcedures();
            RegisterEvents();
        }

		public void Fill(System.Data.DataRow inTempRow) {
			System.Data.DataColumnCollection cols = inTempRow.Table.Columns;
			if (cols["ID"] != null) { this.ID = Convert.ToInt64(inTempRow["ID"]); }
			if (cols["TypeID"] != null) { this.TypeID = Convert.ToInt16(inTempRow["TypeID"]); }
			if (cols["IsActive"] != null) { this.IsActive = Convert.ToBoolean(inTempRow["IsActive"]); }
			if (cols["UpdateTime"] != null) { this.UpdateTime = Convert.ToDateTime(inTempRow["UpdateTime"]); }
		}

		public Xy.Data.Procedure FillProcedure(Xy.Data.Procedure inItem) {
			inItem.SetItem("ID", this.ID);
			inItem.SetItem("TypeID", this.TypeID);
			inItem.SetItem("IsActive", this.IsActive);
			inItem.SetItem("UpdateTime", this.UpdateTime);
			return inItem;
		}

		public string[] GetAttributesName() {
			return new string[]{ "ID", "TypeID", "IsActive", "UpdateTime" };
		}

		public object GetAttributesValue(string inName) {
			switch (inName) {
				case "ID":
					return this.ID;
				case "TypeID":
					return this.TypeID;
				case "IsActive":
					return this.IsActive;
				case "UpdateTime":
					return this.UpdateTime;
				default:
					return null;
			}
		}

		public System.Xml.XPath.XPathDocument GetXml() {
			StringBuilder _xmlBuilder = new StringBuilder();
			string[] _attrs = GetAttributesName();
			_xmlBuilder.Append("<EntityBase>");
			foreach (string _attr in _attrs) {
				_xmlBuilder.AppendFormat("<{0}>{1}</{0}>", _attr, GetAttributesValue(_attr));
			}
			_xmlBuilder.Append("</EntityBase>");
			return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
		}

    }
}


#region SQL Help Code
/*  --you can use below code in your sql procedures
	@ID bigint,
	@TypeID smallint,
	@IsActive bit,
	@UpdateTime datetime

	[ID] , [TypeID] , [IsActive] , [UpdateTime]
	@ID , @TypeID , @IsActive , @UpdateTime
	[ID] = @ID,
	[TypeID] = @TypeID,
	[IsActive] = @IsActive,
	[UpdateTime] = @UpdateTime
*/
#endregion

#region C# Help Code
/*  --you can use below code in your sql procedures
            Xy.Data.Procedure item = new Xy.Data.Procedure(R("itemname"));
            item.AddItem("ID", System.Data.DbType.System.Data.DbType.Int64);
            item.AddItem("TypeID", System.Data.DbType.System.Data.DbType.Int16);
            item.AddItem("IsActive", System.Data.DbType.System.Data.DbType.Boolean);
            item.AddItem("UpdateTime", System.Data.DbType.System.Data.DbType.DateTime);
            AddProcedure(item);

            long formID, short formTypeID, bool formIsActive, DateTime formUpdateTime
            formID, formTypeID, formIsActive, formUpdateTime
            protected long formID;
            protected short formTypeID;
            protected bool formIsActive;
            protected DateTime formUpdateTime;

            formID = Convert.ToInt64(Request.Form["ID"]);
            formTypeID = Convert.ToInt16(Request.Form["TypeID"]);
            formIsActive = Convert.ToBoolean(Request.Form["IsActive"]);
            formUpdateTime = Convert.ToDateTime(Request.Form["UpdateTime"]);

            long inID, short inTypeID, bool inIsActive, DateTime inUpdateTime
            inID, inTypeID, inIsActive, inUpdateTime
            item.SetItem("ID", inID);
            item.SetItem("TypeID", inTypeID);
            item.SetItem("IsActive", inIsActive);
            item.SetItem("UpdateTime", inUpdateTime);

            ID, TypeID, IsActive, UpdateTime
            ID = _item.ID;
            TypeID = _item.TypeID;
            IsActive = _item.IsActive;
            UpdateTime = _item.UpdateTime;

*/
#endregion
