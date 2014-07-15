using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class DefaultEntityType: IEntityType {
        public void ClearTable() {
            throw new NotImplementedException();
        }

        public void CreateTable() {
            throw new NotImplementedException();
        }

        public void DropTable() {
            throw new NotImplementedException();
        }

        public void AddAttribute() {
            throw new NotImplementedException();
        }

        public void EditAttribute() {
            throw new NotImplementedException();
        }

        public void DelAttribute() {
            throw new NotImplementedException();
        }

        public void GetAttributes() {
            throw new NotImplementedException();
        }

        public long Add(System.Collections.Specialized.NameValueCollection values) {
            throw new NotImplementedException();
        }

        public int Edit(System.Collections.Specialized.NameValueCollection values, long[] ID) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Get(string where) {
            throw new NotImplementedException();
        }

        public int Del(string where) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetList(string where, int pageIndex, int pageSize, string order, ref int totalRowCount) {
            throw new NotImplementedException();
        }
    }
}
