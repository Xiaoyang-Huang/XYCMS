using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class Entity : Xy.Data.IDataModelDisplay {
        struct MultiValue {
            public long ID;
            public object Value;
        }

        private Dictionary<string, List<object>> _values;
        private Dictionary<string, List<long>> _filter;
        private EntityTypeCacheItem _cache;
        private bool _readLine;

        internal Entity(EntityTypeCacheItem cache) {
            _values = new Dictionary<string, List<object>>();
            _cache = cache;
            _readLine = false;
            if (_cache.HasMulitple) _filter = new Dictionary<string, List<long>>();
        }

        internal void AddRow(System.Data.DataRow Row) {
            foreach (EntityAttribute _attr in _cache.Attributes) {
                if (!_attr.IsMultiple) {
                    if (_readLine) continue;
                    _values.Add(_attr.Key, new List<object>() { Row[_attr.Key] });
                } else {
                    if (Row[_attr.Key + "ID"] == DBNull.Value) continue;
                    long _multiID = Convert.ToInt64(Row[_attr.Key + "ID"]);
                    if (!_filter.ContainsKey(_attr.Key)) {
                        _filter.Add(_attr.Key, new List<long>() { _multiID });
                        _values.Add(_attr.Key, new List<object>() { new MultiValue() { ID = _multiID, Value = Row[_attr.Key] } });
                    } else {
                        if (_filter[_attr.Key].Contains(_multiID)) continue;
                        _filter[_attr.Key].Add(_multiID);
                        _values[_attr.Key].Add(new MultiValue() { ID = _multiID, Value = Row[_attr.Key] });
                    }
                }
            }
            _readLine = true;
        }

        public string[] GetAttributesName() {
            return _cache.AttributeKeys;
        }

        public object GetAttributesValue(string inName) {
            EntityAttribute _attr = _cache.GetAttribute(inName);
            if (!_values.ContainsKey(inName)) return null;
            List<object> _valItem = _values[inName];
            if (_attr.IsMultiple) {
                StringBuilder _value = new StringBuilder();
                foreach (object _val in _valItem) {
                    if (_value.Length > 0) _value.Append(_attr.Split);
                    _value.Append(((MultiValue)_val).Value.ToString());
                }
                return _value.ToString();
            } else {
                return _valItem[0];
            }
        }

        private const string itemTemplte = "<{0}{2}><![CDATA[{1}]]></{0}>";
        internal string GetXmlString() {
            StringBuilder _xmlBuilder = new StringBuilder();
            _xmlBuilder.AppendFormat("<{0}>", _cache.TypeInstance.Key);
            foreach (EntityAttribute _attr in _cache.Attributes) {
                if (!_values.ContainsKey(_attr.Key)) continue;
                List<object> _cur = _values[_attr.Key];
                if (_cur != null) {
                    foreach (object _incur in _cur) {
                        if (_incur == null) continue;
                        if (_attr.IsMultiple) {
                            MultiValue multi = (MultiValue)_incur;
                            _xmlBuilder.AppendFormat(itemTemplte, _attr.Key, multi.Value, " ID=\"" + multi.ID + "\" ");
                        } else {
                            _xmlBuilder.AppendFormat(itemTemplte, _attr.Key, _incur, string.Empty);
                        }
                    }
                }
            }
            _xmlBuilder.AppendFormat("</{0}>", _cache.TypeInstance.Key);
            return _xmlBuilder.ToString();
        }

        public System.Xml.XPath.XPathDocument GetXml() {
            return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(GetXmlString()));
        }
    }
}
