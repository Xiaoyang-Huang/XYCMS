using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public interface IEntityType {
        void ClearTable();
        void CreateTable();
        void DropTable();

        void AddAttribute();
        void EditAttribute();
        void DelAttribute();
        void GetAttributes();

        long Add(System.Collections.Specialized.NameValueCollection values);
        int Edit(System.Collections.Specialized.NameValueCollection values, long[] ID);
        System.Data.DataTable Get(long[] ID);
        //Entity GetEntity(long[] ID);
        int Del(long[] ID);
        System.Data.DataTable GetList(string where, int pageIndex, int pageSize, string order, ref int totalRowCount);
        //EntityCollection GetEntityList(string where, int pageIndex, int pageSize, string order, ref int totalRowCount);
    }

    public interface IEntityAttribute {
        string Default { get; }
        string Description { get; }
        long Display { get; }
        long FKID { get; }
        long ID { get; }
        bool IsIncrease { get; }
        bool IsNull { get; }
        bool IsPrimary { get; }
        string Key { get; }
        string Name { get; }
        string Table { get; }
        string Type { get; }
        short TypeID { get; }
        IEntityAttributeHandle Handle { get; }
    }

    public interface IEntityAttributeHandle {
        object HandleValue(object value);
    }
}
