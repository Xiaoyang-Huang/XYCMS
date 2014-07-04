using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Installation {
    public class InstallationCollection : List<Installation> {
        private bool IsLoadedDefault = false;
        public void LoadInstalled() {
            if (IsLoadedDefault) return;
            try {
                System.Data.DataTable _dt = Installation.GetAll();
                foreach (System.Data.DataRow _row in _dt.Rows) {
                    Installation _item = new Installation();
                    _item.Fill(_row);
                    _item.IsInstalled = true;
                    this.Add(_item);
                }
            } catch (System.Data.SqlClient.SqlException ex) {
                if (!(ex.Number == 2812 && ex.Message.IndexOf("XiaoYang_Installation_Installation_GetAll") > 0)) throw ex;
            }
            Arrange(true);
            IsLoadedDefault = true;
        }

        public void Arrange(bool RemoveInstalled) {
            this.Sort(Installation.Compare);
            for (int i = this.Count; i >= 0; i--) {
                if (i >= this.Count) continue;
                if (this[i].IsInstalled) {
                    for (int j = this.Count; j >= 0; j--) {
                        if (j >= this.Count) continue;
                        if (string.Compare(this[i].Name, this[j].Name, true) == 0)
                            if (Installation.CompareVersion(this[i].Version, this[j].Version) == 0 && this[i] != this[j])
                                throw new Exception(string.Format("\"{0}\" version \"{1}\" already installed", this[j].Name, this[j].Version));
                            else if (Installation.CompareVersion(this[i].Version, this[j].Version) > 0 && !this[j].IsInstalled)
                                throw new Exception(string.Format("you cannot install \"{0}\" case current version \"{1}\" is higher than your version \"{2}\"", this[i].Name, this[i].Version, this[j].Version));
                    }
                    if (RemoveInstalled) {
                        this.Remove(this[i]);
                    }
                }
            }
        }
    }
}
