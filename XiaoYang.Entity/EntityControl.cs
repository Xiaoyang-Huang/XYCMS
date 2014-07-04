using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityControl: Xy.Web.Control.IControl {
        public void Handle(Xy.Web.ThreadEntity CurrentThreadEntity, Xy.Web.Page.PageAbstract CurrentPageClass, Xy.Web.HTMLContainer contentContainer) {
            throw new NotImplementedException();
        }

        public void Init(System.Collections.Specialized.NameValueCollection CreateTag, string map, int Index) {
            throw new NotImplementedException();
        }

        public Xy.Web.HTMLContainer InnerData {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public bool IsNeedData {
            get { throw new NotImplementedException(); }
        }

        public string Map {
            get { throw new NotImplementedException(); }
        }
    }
}
