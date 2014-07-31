using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity.Runtime {
    public class HandleFactory {
        private static string[] _handleString;
        public static string[] HandleString { get { return _handleString; } }

        private static Xy.Tools.Runtime.IFactory<XiaoYang.Entity.IEntityHandle> _factory;

        static HandleFactory() {
            List<string> _handleStringList = new List<string>();
            foreach (string _webConfigName in Xy.WebSetting.WebSettingCollection.ConfigNameList) {
                Xy.WebSetting.WebSettingItem _webConfigItem = Xy.WebSetting.WebSettingCollection.GetWebSetting(_webConfigName);
                if (!string.IsNullOrEmpty(_webConfigItem.Config["EntityHandles"])) {
                    string[] _handleStrings = _webConfigItem.Config["EntityHandles"].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < _handleStrings.Length; i++) {
                        string _item = _handleStrings[i].Trim();
                        if (!string.IsNullOrEmpty(_item) && !_handleStringList.Contains(_item)) {
                            _handleStringList.Add(_item);
                        }
                    }
                }
            }
            _handleString = _handleStringList.ToArray();
            LoadAllHandle();
        }

        private static void LoadAllHandle() {
            List<string> _reference = new List<string>();
            Xy.Tools.Runtime.Compiler<XiaoYang.Entity.IEntityHandle> _compiler = new Xy.Tools.Runtime.Compiler<XiaoYang.Entity.IEntityHandle>();
            _compiler.ErrorMessage = "Can not found handle with key: {0}";
            _compiler.CompilerItems = new Xy.Tools.Runtime.CompilerItem[_handleString.Length];
            int i = 0;
            foreach (string _nameString in _handleString) {
                string _binName = Xy.AppSetting.BinDir + _nameString.Split(',')[0] + ".dll";
                string _nameSpace = _nameString.Split(',')[1];
                if (!_reference.Contains(_binName)) _reference.Add(_binName);
                _compiler.CompilerItems[i] = new Xy.Tools.Runtime.CompilerItem() {
                    Identity = new string[] { _nameString, _nameSpace },
                    Name = _nameSpace
                };
                i++;
            }
            _compiler.ReferencedAssemblies = _reference.ToArray();
            _factory = _compiler.GetFactory();
        }

        public static XiaoYang.Entity.IEntityHandle GetEntityTypeHandle(string className){
            return _factory.GetInstance(className);
        }
    }
}
