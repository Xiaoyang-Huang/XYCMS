using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityTemplateControl: Xy.Web.Control.IControl {

        private string _map;
        public string Map { get { return _map; } set { _map = value; } }
        private bool _isNeedData = false;
        public bool IsNeedData { get { return _isNeedData; } }
        private Xy.Web.HTMLContainer _innerData = null;
        public Xy.Web.HTMLContainer InnerData { get { return _innerData; } set { _innerData = value; } }

        private long _templateID = 0;
        private long _fieldID = 0;

        public void Init(System.Collections.Specialized.NameValueCollection CreateTag, string map, int Index) {
            for (int i = 0; i < CreateTag.Count; i++) {
                switch (CreateTag.Keys[i]) {
                    case "TemplateID":
                        _templateID = Convert.ToInt64(CreateTag[i]);
                        break;
                    case "FieldID":
                        _fieldID = Convert.ToInt64(CreateTag[i]);
                        break;
                }
            }
            _map = string.Concat(map, "EntityTemplateControl", Index);
        }

        public void Handle(Xy.Web.ThreadEntity CurrentThreadEntity, Xy.Web.Page.PageAbstract CurrentPageClass, Xy.Web.HTMLContainer contentContainer) {
            CurrentThreadEntity.ControlIndex += 1;

            Xy.Web.Control.ControlAnalyze _controls = new Xy.Web.Control.ControlAnalyze(CurrentThreadEntity, this.Map, false);
            //_controls.SetContent(_temp.ToArray());
            //_controls.Analyze();
            //_temp.Clear();
            //_controls.Handle(CurrentPageClass, _temp);
            CurrentThreadEntity.ControlIndex -= 1;
        }
    }
}
