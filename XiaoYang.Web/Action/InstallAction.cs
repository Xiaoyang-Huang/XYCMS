using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class InstallAction: Xy.Web.Page.PageAbstract {
        private string logFilePath = Xy.AppSetting.LogDir + "instal_process.txt";
        private string updateFilePath = Xy.AppSetting.LogDir + "update.xml";
        private string binFileDir = AppDomain.CurrentDomain.BaseDirectory + "Bin\\";

        public override void Validate() {
            if (!string.IsNullOrEmpty(Request.GroupString["type"])) {
                switch (Request.GroupString["type"]) {
                    case "start":
                        if (!hasUpdate()) {
                            throw new Exception("There is no update file.");
                        }
                        if (updateStarted()) {
                            throw new Exception("There is a update started already.");
                        }
                        writeUpdateProcess("Loading...");
                        XiaoYang.Installation.InstallationCollection _installCollection = new Installation.InstallationCollection();
                        _installCollection.LoadInstalled();
                        System.Xml.XmlDocument _updateDom = new System.Xml.XmlDocument();
                        _updateDom.Load(updateFilePath);
                        foreach (System.Xml.XmlNode _item in _updateDom.SelectNodes("Install/Item")) {
                            XiaoYang.Installation.Installation _install = new Installation.Installation();
                            _install.Name = _item.SelectSingleNode("Label").InnerText;
                            _install.Version = _item.SelectSingleNode("Version").InnerText;
                            _install.Depend = _item.SelectSingleNode("Depend") == null ? string.Empty : _item.SelectSingleNode("Depend").InnerText;
                            _install.SQL = _item.SelectSingleNode("SQL") == null ? string.Empty : _item.SelectSingleNode("SQL").InnerText;
                            _install.Code = _item.SelectSingleNode("Code") == null ? string.Empty : _item.SelectSingleNode("Code").InnerText;
                            if (!string.IsNullOrEmpty(_install.Code)) {
                                _install.CodeLanguage = _item.SelectSingleNode("Code").Attributes["Language"] == null ? "CSharp" : _item.SelectSingleNode("Code").Attributes["Language"].Value;
                            }
                            _install.Message = _item.SelectSingleNode("Message") == null ? "No message" : _item.SelectSingleNode("Message").InnerText;
                            _installCollection.Add(_install);
                        }
                        _installCollection.Arrange(true);

                        writeUpdateProcess("Install list:");
                        for (int i = 0; i < _installCollection.Count; i++) {
                            writeUpdateProcess(string.Format("\t{0}({1}):", _installCollection[i].Name, _installCollection[i].Version));
                            writeUpdateProcess(string.Format("\t\t{0}", _installCollection[i].Message));
                        }
                        writeUpdateProcess("Preparing...");
                        for (int i = 0; i < _installCollection.Count; i++) {
                            XiaoYang.Installation.Installation _updateItem = _installCollection[i];
                            if (!string.IsNullOrEmpty(_updateItem.Code)) {
                                _updateItem.GeneratedAssembly = buildAssembly(_updateItem.CodeLanguage, _updateItem.Code, _updateItem.Depend);
                            }
                        }

                        writeUpdateProcess("Updating...");
                        Xy.Data.DataBase DB = new Xy.Data.DataBase("Update");
                        for (int i = 0; i < _installCollection.Count; i++) {
                            DB.Open();
                            DB.StartTransaction();
                            XiaoYang.Installation.Installation _updateItem = _installCollection[i];
                            try {
                                if (!string.IsNullOrEmpty(_updateItem.SQL)) {
                                    Xy.Data.Procedure _updateSQL = new Xy.Data.Procedure("UpdateSQL", _updateItem.SQL);
                                    _updateSQL.InvokeProcedure(DB);
                                }
                                if (!string.IsNullOrEmpty(_updateItem.Code)) {
                                    XiaoYang.Installation.IInstallItem _installItem = (XiaoYang.Installation.IInstallItem)_updateItem.GeneratedAssembly.CreateInstance("XyUpdate.XyUpdate");
                                    _installItem.Update(DB);
                                }
                                XiaoYang.Installation.Installation.Add(_updateItem, DB);
                                DB.CommitTransaction();
                                writeUpdateProcess(string.Format("{0}({1}) is updated",_updateItem.Name,_updateItem.Version));
                            } catch {
                                DB.RollbackTransation();
                                throw;
                            } finally {
                                DB.Close();
                            }
                        }
                        System.IO.File.Delete(updateFilePath);
                        writeUpdateProcess("All Updated!");
                        Response.Write("{status:'success'}");
                        break;
                    case "check":
                        Response.Write("{status:'" + (System.IO.File.Exists(updateFilePath) ? (System.IO.File.Exists(logFilePath) ? "processing" : "success" ) : "failure") + "'}");
                        break;
                    case "process":
                        if (!System.IO.File.Exists(logFilePath)) break;
                        System.IO.FileStream _updateLog = System.IO.File.Open(logFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Write);
                        System.IO.StreamReader _sr = new System.IO.StreamReader(_updateLog);
                        Response.Write(_sr.ReadToEnd());
                        _sr.Close();
                        _updateLog.Close();
                        if (!System.IO.File.Exists(updateFilePath) && System.IO.File.Exists(logFilePath)) {
                            System.IO.File.Delete(logFilePath);
                        }
                        break;
                    case "test":
                        try {
                            System.Web.HttpRuntime.UnloadAppDomain();
                            //Response.Write(Xy.Tools.LogTools.DumpObjectTableHTML(System.Web.HttpContext.Current.ApplicationInstance), true, true);
                            //System.DirectoryServices.DirectoryEntry de = new System.DirectoryServices.DirectoryEntry(AppDomain.CurrentDomain.BaseDirectory);
                            //Response.Write(de.Name, true, true);
                            //Response.Write(de.ToString(), true, true);
                        } catch (Exception ex) {
                            Response.Write(ex.Message.ToString());
                        }
                        break;
                }
            }
        }

        public override Xy.Web.Page.PageErrorState onError(Exception ex) {
            writeUpdateProcess(ex.Message);
            Response.Write(string.Format("{{status:\"error\",message:\"{0}\"}}", ex.Message.Replace("\"", "\\\"")));
            return Xy.Web.Page.PageErrorState.Handled;
        }

        private void writeUpdateProcess(string message) {
            System.IO.FileStream _updateLog = System.IO.File.Open(logFilePath, System.IO.FileMode.OpenOrCreate | System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Read);
            using (_updateLog) {
                System.IO.StreamWriter _sw = new System.IO.StreamWriter(_updateLog);
                using (_sw) {
                    _sw.WriteLine("<p>" + message.Replace("\t", "<span></span>") + "<p>");
                    _sw.Flush();
                    _sw.Close();
                    _sw.Dispose();
                }
                _updateLog.Close();
                _updateLog.Dispose();
            }
        }

        private bool updateStarted() {
            if (System.IO.File.Exists(logFilePath)) {
                return true;
            }
            return false;
        }

        private bool hasUpdate() {
            if (System.IO.File.Exists(updateFilePath)) {
                return true;
            }
            return false;
        }

        private bool checkDependDLL(string DLLName) {
            try {
                System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Bin\\" + DLLName);
                return true;
            } catch (System.IO.FileNotFoundException fileNotFoundEx) {
                throw fileNotFoundEx;
            }
        }

        private System.Reflection.Assembly buildAssembly(string Language, string Expression, string Depend) {
            System.CodeDom.Compiler.CodeDomProvider provider;
            switch (Language) {
                case "CSharp":
                    provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp");
                    break;
                case "VisalBasic":
                    provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("VisalBasic");
                    break;
                default:
                    throw new Exception("Unknow language: " + Language);
            }
            System.CodeDom.Compiler.CompilerParameters cp = new System.CodeDom.Compiler.CompilerParameters();
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.TempFiles = new System.CodeDom.Compiler.TempFileCollection(Xy.AppSetting.LogDir);
            //cp.CompilerOptions = "/nooutput";
            if (!string.IsNullOrEmpty(Depend)) {
                string[] _userdepends = Depend.Split(',');
                List<string> _depends = new List<string>();
                _depends.Add("Xy.Data.dll");
                _depends.Add("XiaoYang.Installation.dll");
                for (int i = 0; i < _userdepends.Length; i++) {
                    if (!_depends.Contains(_userdepends[i]) && checkDependDLL(_userdepends[i])) _depends.Add(_userdepends[i]);
                }
                for (int i = 0; i < _depends.Count; i++) {
                    cp.ReferencedAssemblies.Add(binFileDir + _depends[i]);
                }
            }
            
            StringBuilder code = new StringBuilder();
            switch (Language) {
                case "CSharp":
                    code.AppendLine("namespace XyUpdate {");
                    code.AppendLine("public class XyUpdate : XiaoYang.Installation.IInstallItem {");
                    code.AppendLine("public void Update(Xy.Data.DataBase updateDB) {");
                    code.AppendLine(Expression);
                    code.AppendLine("} } }");
                    break;
                case "VisalBasic":
                    code.AppendLine("Namespace XyUpdate");
                    code.AppendLine("Public Class XyUpdate Implements XiaoYang.Installation.IInstallItem");
                    code.AppendLine("Public Sub Update(updateDB as Xy.Data.DataBase)");
                    code.AppendLine(Expression);
                    code.AppendLine("End Function");
                    code.AppendLine("End Class");
                    code.AppendLine("End Namespace");
                    break;
                default:
                    throw new Exception("Unknow language: " + Language);
            }

            System.CodeDom.Compiler.CompilerResults cr = provider.CompileAssemblyFromSource(cp, code.ToString());

            foreach (System.CodeDom.Compiler.CompilerError _error in cr.Errors) {
                string[] codes = code.ToString().Split(new string[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.None);
                if (_error.Line > 0) {
                    throw new Exception(string.Format("error:\"{0}\" on \"{1}\"", _error.ErrorText, codes[_error.Line - 1]));
                } else {
                    throw new Exception(string.Format("error:\"{0}\"", _error.ErrorText));
                }
            }
            return cr.CompiledAssembly;
        }
    }
}
