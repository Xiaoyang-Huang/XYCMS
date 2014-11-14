using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityHandlesDataBuilder : Xy.Data.IDataBuilder {

        static string[] _handles = null;

        static EntityHandlesDataBuilder() {
            System.Xml.XmlDocument _document = new System.Xml.XmlDocument();
            _document.Load(Xy.Tools.IO.File.foundConfigurationFile("App", Xy.AppSetting.FILE_EXT));
            List<string> _tempHandles = new List<string>();
            foreach (System.Xml.XmlNode _item in _document.SelectNodes("AppSetting/EntityHandles/Item")) {
                if (!_tempHandles.Contains(_item.InnerText)) _tempHandles.Add(_item.InnerText);
            }
            _handles = _tempHandles.ToArray();
        }

        public string GetRoot() {
            return "Handles/Handle";
        }

        private static System.Xml.XPath.XPathDocument _cachedHandles = null;
        public System.Xml.XPath.XPathDocument HandleData(Xy.Web.Page.PageAbstract CurrentPageClass) {
            if (_cachedHandles == null) {
                StringBuilder _xml = new StringBuilder("<Handles>");
                foreach (string _item in _handles) {
                    _xml.Append(string.Format("<Handle><Name>{0}</Name></Handle>", _item));
                }
                _xml.Append("</Handles>");
                _cachedHandles = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xml.ToString()));
            }
            return _cachedHandles;
        }

        public void Init(System.Collections.Specialized.NameValueCollection Tags) {
        }

        public string InsertParameter(string xslt) {
            return xslt;
        }
    }
}
