using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class AttributeDisplay: Xy.Web.Page.PageAbstract{
        public override void onGetRequest() {
            long _attrID = Convert.ToInt64(Request.Values["AttributeID"]);
            Entity.EntityAttribute _attr = Entity.EntityAttribute.GetInstance(_attrID);
            Entity.EntityAttributeDisplay _attrDisplay = Entity.EntityAttributeDisplay.GetInstance(_attr.Display);
            _attrDisplay.Template = _attrDisplay.Template
                            .Replace("{{AttributeName}}", _attr.Name)
                            .Replace("{{AttributeKey}}", _attr.Key)
                            .Replace("{{AttributeTable}}", _attr.Table)
                            .Replace("{{TypeID}}", PageData["Type.ID"].GetDataString())
                            .Replace("{{TypeKey}}", PageData["Type.Key"].GetDataString())
                            .Replace("{{TypeName}}", PageData["Type.Name"].GetDataString())
                            .Replace("{{EntityID}}", PageData["Entity.ID"] == null ? string.Empty : PageData["Entity.ID"].GetDataString())
                            .Replace("{{IsMultiple}}", _attr.IsMultiple.ToString())
                            .Replace("{{Split}}", _attr.Split)
                            .Replace("{{Value}}", PageData["Entity." + _attr.Key] == null ? _attr.Default : PageData["Entity." + _attr.Key].GetDataString());
            Xy.Web.HTMLContainer _container = new Xy.Web.HTMLContainer(WebSetting.Encoding);
            if (PageData["Resource" + _attrDisplay.ID] == null) {
                PageData.Add("Resource" + _attrDisplay.ID, _attrDisplay.Resource);
                _container.Write(_attrDisplay.Resource);
            }
            _container.Write(_attrDisplay.Template);
            SetContent(_container);
        }
    }
}
