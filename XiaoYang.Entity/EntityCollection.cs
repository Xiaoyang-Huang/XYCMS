using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityCollection: IList<Entity>, Xy.Data.IDataModelDisplay {

        private bool _dirty = true;
        private List<Entity> _core = null;
        System.Xml.XPath.XPathDocument _xml = null;
        System.Xml.XPath.XPathNavigator _navgator = null;
        private Cache _cache = null;

        internal EntityCollection(Cache inCache) {
            _cache = inCache;
            _core = new List<Entity>();
        }

        private void _GeneratXml() {
            if (!_dirty) return;

            _dirty = false;
        }

        public string[] GetAttributesName() {
            _GeneratXml();
            throw new NotImplementedException();
        }

        public object GetAttributesValue(string inName) {
            _GeneratXml();
            throw new NotImplementedException();
        }

        public System.Xml.XPath.XPathDocument GetXml() {
            _GeneratXml();
            throw new NotImplementedException();
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
