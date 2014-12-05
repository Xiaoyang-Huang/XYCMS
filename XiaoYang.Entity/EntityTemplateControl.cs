using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity {
    public class EntityTemplateControl : Xy.Web.Control.IControl {

        private string _map;
        public string Map { get { return _map; } set { _map = value; } }
        private bool _isNeedData = false;
        public bool IsNeedData { get { return _isNeedData; } }
        private Xy.Web.HTMLContainer _innerData = null;
        public Xy.Web.HTMLContainer InnerData { get { return _innerData; } set { _innerData = value; } }

        private long _templateID = 0;
        private long _fieldID = 0;
        private static readonly string _cachePath = Xy.AppSetting.CacheDir + "\\FieldTemplate\\";

        static EntityTemplateControl(){
            if (!System.IO.Directory.Exists(_cachePath)) System.IO.Directory.CreateDirectory(_cachePath);
        }

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
            string _templatePath = _cachePath + _fieldID + "_" + _templateID + ".tmp";
            byte[] _templateBytes = null;
            XiaoYang.Entity.EntityFieldForm _fieldForm = XiaoYang.Entity.EntityFieldForm.GetInstance(_templateID);

            System.IO.FileInfo _fileInfo = new System.IO.FileInfo(_templatePath);
            if (!_fileInfo.Exists || _fileInfo.LastWriteTime < _fieldForm.UpdateTime) {
                XiaoYang.Entity.EntityField _field = XiaoYang.Entity.EntityField.GetInstance(_fieldID);
                XiaoYang.Entity.EntityTable _table = XiaoYang.Entity.EntityTable.GetInstance(_field.TableID);
                XiaoYang.Entity.EntityType _type = XiaoYang.Entity.EntityType.GetInstance(_table.TypeID);
                string _template = _fieldForm.Template
                                            .Replace("{{AttributeName}}", _field.Name)
                                            .Replace("{{AttributeKey}}", _field.Key)
                                            .Replace("{{TableName}}", _table.Name)
                                            .Replace("{{TableKey}}", _table.Key)
                                            .Replace("{{TypeID}}", _type.ID.ToString())
                                            .Replace("{{TypeKey}}", _type.Key)
                                            .Replace("{{TypeName}}", _type.Name)
                                            .Replace("{{IsMultiple}}", _table.Multiply.ToString());

                string _scriptList = string.Empty;
                if (CurrentPageClass.PageData["EntityFormTemplateScriptList"] != null) _scriptList = CurrentPageClass.PageData["EntityFormTemplateScriptList"].GetDataString();
                string _resourceID = "|" + _templateID + "|";
                if (!_scriptList.Contains(_resourceID)) {
                    _scriptList += _resourceID;
                    CurrentPageClass.PageData.Add("EntityFormTemplateScriptList", _scriptList);
                    _template = _fieldForm.Resource + Environment.NewLine + _template;
                }

                _templateBytes = CurrentPageClass.WebSetting.Encoding.GetBytes(_template);
                using (System.IO.FileStream _file = System.IO.File.Create(_templatePath)) {
                    _file.Write(_templateBytes, 0, _templateBytes.Length);
                    _file.Flush();
                    _file.Close();
                }
            } else {
                _templateBytes = System.IO.File.ReadAllBytes(_templatePath);
            }
            Xy.Web.Control.ControlAnalyze _controls = new Xy.Web.Control.ControlAnalyze(CurrentThreadEntity, this.Map, false);
            _controls.SetContent(_templateBytes);
            _controls.Analyze();
            _controls.Handle(CurrentPageClass, contentContainer);
            CurrentThreadEntity.ControlIndex -= 1;
        }
    }
}
