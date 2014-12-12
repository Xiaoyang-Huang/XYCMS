using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityCollection: IList<Entity>, Xy.Data.IDataModelDisplay {

        private bool _dirty = true;
        private List<Entity> _core = null;
        private string _xmlString = string.Empty;
        System.Xml.XPath.XPathDocument _xml = null;
        System.Xml.XPath.XPathNavigator _navgator = null;
        private Cache _cache = null;

        internal EntityCollection(Cache inCache) {
            _cache = inCache;
            _core = new List<Entity>();
        }

        private void _GeneratXml() {
            if (!_dirty) return;
            StringBuilder _temp = new StringBuilder();
            _temp.AppendFormat("<{0}Collection>", _cache.Type.Key);
            for (int i = 0; i < this.Count; i++) {
                _temp.Append(this[i].XMLString);
            }
            _temp.AppendFormat("</{0}Collection>", _cache.Type.Key);
            _xmlString = _temp.ToString();
            _dirty = false;
        }

        public string[] GetAttributesName() {
            return _cache.AttributeNameList;
        }

        public object GetAttributesValue(string inName) {
            if (_dirty || _navgator == null) { 
                _GeneratXml();
                _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlString));
                _navgator = _xml.CreateNavigator();
            }
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
            if (string.IsNullOrEmpty(_table)) throw new Exception("Can not found table");
            StringBuilder _value = new StringBuilder();
            System.Xml.XPath.XPathNodeIterator _iter = _navgator.Select(string.Format("{0}Collection/{0}/{1}/{2}", _cache.Type.Key, _table, _column));
            if (_iter.Count > 0) {
                while (_iter.MoveNext()) {
                    _value.Append(',').Append(_iter.Current.Value);
                }
            }
            return _value.ToString();
        }

        public System.Xml.XPath.XPathDocument GetXml() {
            if (_dirty || _xml == null) {
                _GeneratXml();
                _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlString));
            }
            return _xml;
        }

        public void Insert(int index, Entity item) {
            _core.Insert(index, item);
            _dirty = true;
        }

        public void RemoveAt(int index) {
            _core.RemoveAt(index);
            _dirty = true;
        }

        

        public void Add(Entity item) {
            _core.Add(item);
            _dirty = true;
        }

        public void Clear() {
            _core.Clear();
            _dirty = true;
        }

        public bool Remove(Entity item) {
            bool _result = _core.Remove(item);
            _dirty = true;
            return _result;
        }

        
        
        public Entity this[int index] {
            get {
                return _core[index];
            }
            set {
                _core[index] = value;
                _dirty = true;
            }
        }

        public int Count {
            get { return _core.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }
        
        public bool Contains(Entity item) {
            return _core.Contains(item);
        }

        public int IndexOf(Entity item) {
            return _core.IndexOf(item);
        }

        public void CopyTo(Entity[] array, int arrayIndex) {
            _core.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Entity> GetEnumerator() {
            return _core.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _core.GetEnumerator();
        }
    }
}
