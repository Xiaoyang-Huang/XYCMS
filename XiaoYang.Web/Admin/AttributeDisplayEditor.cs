using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class AttributeDisplayEditor : Xy.Web.Page.PageAbstract {
        //public override void onGetRequest() {
        //    if (!string.IsNullOrEmpty(Request.QueryString["ID"])) {
        //        PageData.AddXyDataModel("Item", XiaoYang.Entity.EntityAttributeDisplay.GetInstance(Request.QueryString));
        //    }
        //    if (!string.IsNullOrEmpty(Request.Values["TypeID"])) {
        //        long _entityID = Convert.ToInt64(Request.Values["EntityID"]);
        //        short _typeID = Convert.ToInt16(Request.Values["TypeID"]);
        //        XiaoYang.Entity.EntityType _typeInstance = XiaoYang.Entity.EntityType.GetInstance(_typeID);
        //        string _typeName = _typeInstance.Key;
        //        System.Data.DataTable _attributesTable = XiaoYang.Entity.EntityAttribute.GetByTypeID(_typeID);
        //        System.Data.DataTable _entity_ext = null;
        //        if (_entityID > 0) {
        //            XiaoYang.Entity.EntityHelper _entity = new Entity.EntityHelper(_typeID);
        //            _entity_ext = _entity.Get(_entityID);
        //        }
        //        _attributesTable.Columns.Add("Template");
        //        Dictionary<long, string> _attr_resource = new Dictionary<long, string>();
        //        foreach (System.Data.DataRow _row in _attributesTable.Rows) {
        //            XiaoYang.Entity.EntityAttributeDisplay _pad = XiaoYang.Entity.EntityAttributeDisplay.GetInstance(Convert.ToInt64(_row["Display"]));
        //            if (!_attr_resource.ContainsKey(_pad.ID)) _attr_resource.Add(_pad.ID, _pad.Resource);
        //            _pad.Template = _pad.Template
        //                                   .Replace("{{AttributeName}}", _row["Name"].ToString())
        //                                   .Replace("{{TypeID}}", Request.Values["TypeID"].ToString())
        //                                   .Replace("{{TypeName}}", _typeName)
        //                                   .Replace("{{EntityID}}", Request.Values["EntityID"] == null ? string.Empty : Request.Values["EntityID"].ToString())
        //                                   .Replace("{{IsMultiple}}", Convert.ToBoolean(_row["IsMultiple"]).ToString())
        //                                   .Replace("{{Split}}", _row["Split"].ToString());
        //            if (_entity_ext != null) {
        //                if (Convert.ToBoolean(_row["IsMultiple"])) {
        //                    StringBuilder _multi_val = new StringBuilder();
        //                    List<long> _multi_IDs = new List<long>();
        //                    foreach (System.Data.DataRow _inrow in _entity_ext.Rows) {
        //                        long _inID = Convert.ToInt64(_inrow[_row["Name"].ToString() + ".ID"]);
        //                        if (_multi_IDs.IndexOf(_inID) == -1) {
        //                            _multi_IDs.Add(_inID);
        //                            if (_multi_val.Length > 0) _multi_val.Append(_row["Split"].ToString());
        //                            _multi_val.Append(_inrow[_row["Name"].ToString()].ToString());
        //                        }
        //                    }
        //                    _pad.Template = _pad.Template.Replace("{{Value}}", _multi_val.ToString());
        //                } else {
        //                    _pad.Template = _pad.Template.Replace("{{Value}}", _entity_ext.Rows[0][_row["Name"].ToString()].ToString());
        //                }
        //            } else {
        //                _pad.Template = _pad.Template.Replace("{{Value}}", _row["Default"] == null ? string.Empty : _row["Default"].ToString());
        //            }
        //            Xy.Web.Control.IncludeControl _padt = new Xy.Web.Control.IncludeControl();
        //            _padt.InnerData = new Xy.Web.HTMLContainer(WebSetting.Encoding);
        //            _padt.InnerData.Write(_pad.Template);
        //            System.Collections.Specialized.NameValueCollection _createTag = new System.Collections.Specialized.NameValueCollection();
        //            _createTag.Add("EnableScript", "True");
                    
        //            if (!string.IsNullOrEmpty(_pad.TypeClass)) {
        //                _createTag.Add("Type", _pad.TypeClass);
        //                StringBuilder _ext_values = new StringBuilder();
        //                _ext_values.Append("{");
        //                _ext_values.AppendFormat(" {0}=\"{1}\" ", "AttributeName", _row["Name"].ToString())
        //                            .AppendFormat(" {0}=\"{1}\" ", "EntityID", Request.Values["EntityID"] == null ? string.Empty : Request.Values["EntityID"].ToString())
        //                            .AppendFormat(" {0}=\"{1}\" ", "TypeID", Request.Values["TypeID"].ToString())
        //                            .AppendFormat(" {0}=\"{1}\" ", "TypeName", _typeName)
        //                            .AppendFormat(" {0}=\"{1}\" ", "IsMultiple", Convert.ToBoolean(_row["IsMultiple"]).ToString())
        //                            .AppendFormat(" {0}=\"{1}\" ", "Split", _row["Split"].ToString());
        //                _ext_values.Append("}");
        //                if (_ext_values.Length > 2) {
        //                    _createTag.Add("Value", _ext_values.ToString());
        //                }
        //            }
        //            Xy.Web.HTMLContainer _template = new Xy.Web.HTMLContainer(WebSetting.Encoding);
        //            _padt.Init(_createTag, "attrubiteTemplate", 0);
        //            _padt.Handle(ThreadEntity, this, _template);
        //            _row["Template"] = _template.ToString();
        //        }
        //        PageData.Add("Attributes", _attributesTable);

        //        StringBuilder _attr_resource_template = new StringBuilder();
        //        foreach (string _item in _attr_resource.Values) {
        //            _attr_resource_template.AppendLine(_item);
        //        }
        //        PageData.Add("Resource", _attr_resource_template.ToString());
        //    }
        //}
    }
}
