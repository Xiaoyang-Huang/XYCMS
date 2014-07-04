using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityCollectionControl: Xy.Web.Control.IControl {

        private string _map;
        public string Map { get { return _map; } set { _map = value; } }
        private bool _isNeedData;
        public bool IsNeedData { get { return _isNeedData; } }
        private Xy.Web.HTMLContainer _innerData;
        public Xy.Web.HTMLContainer InnerData { get { return _innerData; } set { _innerData = value; } }
        
        private short _typeID = 0;
        private string _xsltPath = string.Empty;
        private string _xsltParameter = string.Empty;
        private bool _enableScript = false;
        private bool _enableCode = false;
        private bool _enableCache = false;
        private string _root = string.Empty;
        private string _where = string.Empty;
        private string _order = string.Empty;
        private int _pageIndex = 0;
        private int _pageSize = int.MaxValue;

        public void Init(System.Collections.Specialized.NameValueCollection CreateTag, string map, int Index) {
            for (int i = 0; i < CreateTag.Count; i++) {
                switch (CreateTag.Keys[i]) {
                    case "TypeID":
                        _typeID = Convert.ToInt16(CreateTag[i]);
                        break;
                    case "Where":
                        _where = CreateTag[i];
                        break;
                    case "Order":
                        _order = CreateTag[i];
                        break;
                    case "PageIndex":
                        _pageIndex = Convert.ToInt32(CreateTag[i]);
                        break;
                    case "PageSize":
                        _pageSize = Convert.ToInt32(CreateTag[i]);
                        break;
                    case "Xslt":
                        _xsltPath = CreateTag[i];
                        break;
                    case "XsltParameter":
                        _xsltParameter = CreateTag[i];
                        break;
                    case "EnableScript":
                        _enableScript = string.Compare(CreateTag[i], "true", true) == 0;
                        break;
                    case "EnableCode":
                        _enableCode = string.Compare(CreateTag[i], "true", true) == 0;
                        break;
                    case "EnableCache":
                        _enableCache = string.Compare(CreateTag[i], "true", true) == 0;
                        break;
                    case "Root":
                        _root = CreateTag[i];
                        break;
                }
            }
            _map = string.Concat(map, "EntityControl", Index);
            _isNeedData = string.IsNullOrEmpty(_xsltPath);
        }

        public void Handle(Xy.Web.ThreadEntity CurrentThreadEntity, Xy.Web.Page.PageAbstract CurrentPageClass, Xy.Web.HTMLContainer contentContainer) {
            if (_typeID == 0) return;
            EntityHelper _help = new EntityHelper(_typeID);
            
        }
    }
}
