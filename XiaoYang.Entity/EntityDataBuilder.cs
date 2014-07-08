using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityDataBuilder : Xy.Data.IDataBuilder {
        private string _dataConnection = string.Empty;
        private short _typeID = 0;
        private int _pageIndex = 0;
        private int _pageSize = int.MaxValue;
        private string _where = string.Empty;
        private string _order = string.Empty;
        private int _totalCount = -1;
        private XiaoYang.Entity.EntityTypeCacheItem _typeCache = null;
        private string _parameter;
        private string _defaultParameter;
        private Xy.Data.DataParameterCollection _dataParameterCollection = null;

        public void Init(System.Collections.Specialized.NameValueCollection Tags) {
            for (int i = 0; i < Tags.Count; i++) {
                switch (Tags.Keys[i]) {
                    case "Connection":
                        _dataConnection = Tags[i];
                        break;
                    case "Where":
                        _where = Tags[i];
                        break;
                    case "Order":
                        _order = Tags[i];
                        break;
                    case "Parameter":
                        _parameter = Tags[i];
                        break;
                    case "DefaultParameter":
                        _defaultParameter = Tags[i];
                        break;
                    case "PageIndex":
                        _pageIndex = Convert.ToInt32(Tags[i]);
                        break;
                    case "PageSize":
                        _pageSize = Convert.ToInt32(Tags[i]);
                        break;
                    case "TotalCount":
                        _totalCount = Convert.ToInt32(Tags[i]);
                        break;
                    case "TypeID":
                        _typeID = Convert.ToInt16(Tags[i]);
                        break;
                }
            }
            if (_typeID == 0) throw new Exception("incorrect type ID");
            _typeCache = EntityTypeCache.GetInstance(_typeID, string.IsNullOrEmpty(_dataConnection) ? new Xy.Data.DataBase() : new Xy.Data.DataBase(_dataConnection));
            _dataParameterCollection = new Xy.Data.DataParameterCollection();
        }

        public System.Xml.XPath.XPathDocument HandleData(Xy.Web.Page.PageAbstract CurrentPageClass) {
            if (!string.IsNullOrEmpty(_parameter) || !string.IsNullOrEmpty(_defaultParameter)) {
                if (!_dataParameterCollection.Inited) {
                    _dataParameterCollection.AnalyzeParameter(_parameter, _defaultParameter);
                }
                _dataParameterCollection.HandleValue(CurrentPageClass);
                if (_dataParameterCollection.HasContent) {
                    foreach (string _key in _dataParameterCollection.Keys) {
                        _where = _where.Replace("@" + _key, _dataParameterCollection[_key].Value.ToString());
                    }
                }
            }
            XiaoYang.Entity.EntityHelper _helper = new EntityHelper(_typeCache.TypeInstance.ID);
            EntityCollection _result = _helper.GetList(_where, _pageIndex, _pageSize, _order, ref _totalCount);
            return _result.GetXml();
        }

        public string InsertParameter(string xslt) {
            StringBuilder xsltParametersb = new StringBuilder();
            xsltParametersb.Append(string.Format(@"<xsl:variable name=""PageIndex"">{0}</xsl:variable>", _pageIndex));
            xsltParametersb.Append(string.Format(@"<xsl:variable name=""PageSize"">{0}</xsl:variable>", _pageSize));
            xsltParametersb.Append(string.Format(@"<xsl:variable name=""TotalCount"">{0}</xsl:variable>", _totalCount));
            xsltParametersb.Append("<xsl:template");
            xslt = xslt.Replace("<xsl:template", xsltParametersb.ToString());
            if (_dataParameterCollection.HasContent) {
                _dataParameterCollection.GenerateXsltParameter(xslt);
            }
            return xslt;
        }

        public string GetRoot() {
            return string.Format("{0}Collection/{0}", _typeCache.TypeInstance.Key);
        }
    }
}
