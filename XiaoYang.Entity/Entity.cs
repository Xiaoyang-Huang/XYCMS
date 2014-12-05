using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class Entity: Xy.Data.IDataModelDisplay {

        private Cache _cache = null;
        System.Xml.XPath.XPathDocument _xml = null;
        System.Xml.XPath.XPathNavigator _navgator = null;

        internal Entity(Cache inCache, string inContent) {
            _cache = inCache;
            _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(inContent));
            _navgator = _xml.CreateNavigator();
        }

        internal System.Xml.XPath.XPathDocument XML {
            get { return _xml; }
        }

        public string[] GetAttributesName() {
            return _cache.AttributeNameList;
        }

        public object GetAttributesValue(string inName) {
            if(_navgator == null) _navgator = _xml.CreateNavigator();
            string _table = string.Empty, _column = string.Empty;
            if (inName.IndexOf(':') > -1) {
                _table = inName.Split(':')[0];
                _column = inName.Split(':')[1];
            } else {
                for (int i = 0; i < _cache.FieldList.Count; i++) {
                    EntityField _field = _cache.FieldList[i];
                    if (string.Compare(_field.Key, inName) == 0) {
                        _table = _field.Table.Key;
                        _column = _field.Key;
                    }
                }
            }
            if(string.IsNullOrEmpty(_table)) throw new Exception("Can not found table");
            StringBuilder _value = new StringBuilder();
            System.Xml.XPath.XPathNodeIterator _iter = _navgator.Select(string.Format("{0}/{1}/{2}", _cache.Type.Key, _table, _column));
            if (_iter.Count > 0) {
                while (_iter.MoveNext()) {
                    _value.Append(',').Append(_iter.Current.Value);
                }
            }
            return _value.ToString();
        }

        public System.Xml.XPath.XPathDocument GetXml() {
            return _xml;
        }
    }
}
