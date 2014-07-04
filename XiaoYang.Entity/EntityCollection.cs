using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityCollection:List<Entity>, Xy.Data.IDataModelDisplay {

        private EntityTypeCacheItem _cache = null;

        public EntityCollection(System.Data.DataTable Data, short TypeID = 0) {
            long _id = 0;
            short _typeID = TypeID;
            Entity _entity = null;
            foreach (System.Data.DataRow _row in Data.Rows) {
                long _rowID = Convert.ToInt64(_row["ID"]);
                short _rowTypeID = Convert.ToInt16(_row["TypeID"]);
                if (_id != _rowID) {
                    if (_id == 0) {
                        _typeID = _rowTypeID;
                        _cache = EntityTypeCache.GetInstance(_typeID);
                    }
                    _id = _rowID;
                    _entity = new Entity(_cache);
                    this.Add(_entity);
                }
                if (_rowTypeID != _typeID) continue;
                _entity.AddRow(_row);
            }
        }

        public string[] GetAttributesName() {
            if (this.Count == 0) return null;
            return this[0].GetAttributesName();
        }

        public object GetAttributesValue(string inName) {
            if (this.Count == 1) return this[0].GetAttributesValue(inName);
            return null;
        }

        public System.Xml.XPath.XPathDocument GetXml() {
            if (this.Count == 0) return new System.Xml.XPath.XPathDocument(new System.IO.StringReader("<EntityCollection></EntityCollection>"));
            StringBuilder _xmlBuilder = new StringBuilder();
            _xmlBuilder.AppendFormat("<{0}Collection>", _cache.TypeInstance.Key);
            foreach (Entity _item in this) {
                _xmlBuilder.Append(_item.GetXmlString());
            }
            _xmlBuilder.AppendFormat("</{0}Collection>", _cache.TypeInstance.Key);
            return new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlBuilder.ToString()));
        }
    }
}
