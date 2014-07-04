using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Admin {
    public class Install : Xy.Web.Page.Page {
        public override void onGetRequest() {
        }

        private bool checkUpdate() {
            System.IO.DirectoryInfo _directory = new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            foreach (System.IO.FileInfo _file in _directory.GetFiles("*.xml")) {
                if (string.Compare(_file.Name, "install", true) == 0) {
                    return true;
                }
            }
            return false;
        }

        private bool checkDependDLL(string DLLName) {
            try {
                System.Reflection.Assembly.Load("Xy.Web");
                return true;
            } catch (System.IO.FileNotFoundException fileNotFoundEx) {
                return false;
            }
        }
    }
}
