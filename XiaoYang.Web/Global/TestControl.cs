using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Global {
    public class TestControl: Xy.Web.Control.IControl {
        private Xy.Web.HTMLContainer _innerData;
        private string _map;

        public void Handle(Xy.Web.ThreadEntity CurrentThreadEntity, Xy.Web.Page.PageAbstract CurrentPageClass, Xy.Web.HTMLContainer contentContainer) {
            contentContainer.Write("this is test control");
        }

        public void Init(System.Collections.Specialized.NameValueCollection CreateTag, string map, int Index) {
            _map = string.Concat(map, "IncludeControl", Index);
        }

        public Xy.Web.HTMLContainer InnerData {
            get {
                return _innerData;
            }
            set {
                _innerData = value;
            }
        }

        public bool IsNeedData {
            get { return false; }
        }

        public string Map {
            get { return _map; }
        }
    }
}
