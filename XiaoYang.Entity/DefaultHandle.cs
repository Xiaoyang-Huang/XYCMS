using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public interface IHandler {
        void Init(long inTypeID);
        object Add(System.Collections.Specialized.NameValueCollection inForm);
        int Del(System.Collections.Specialized.NameValueCollection inForm);
        System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection inForm);
        int Update(System.Collections.Specialized.NameValueCollection inForm);
        System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection inForm);
        //System.Data.DataTable GetList(int inPageIndex, int inPageSize, string inWhere, string inOrder, out int inTotalCount);
    }

    public class DefaultHandler: IHandler {

        private Cache _cache;

        public void Init(long inTypeID) {
            _cache = CacheManager.Get(inTypeID);
        }

        public object Add(System.Collections.Specialized.NameValueCollection inForm) {
            foreach (EntityTable _table in _cache.TableList) {

            }
            throw new NotImplementedException();
        }

        public int Del(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Get(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public int Update(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetList(System.Collections.Specialized.NameValueCollection inForm) {
            throw new NotImplementedException();
        }

        
    }
}
