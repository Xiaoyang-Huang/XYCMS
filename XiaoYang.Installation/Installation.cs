using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Installation {
    public partial class Installation : Xy.Data.IDataModel {
        public string CodeLanguage { get; set; }
        public bool IsInstalled { get; set; }
        public System.Reflection.Assembly GeneratedAssembly { get; set; }

        static void RegisterEvents() { }

        public static long Add(Installation inInstallation, Xy.Data.DataBase DB = null) {
            Xy.Data.Procedure item = XiaoYang.Installation.Installation.GetProcedure(R("Add"));
            inInstallation.FillProcedure(item);
            return (long)item.InvokeProcedureResult(DB);
        }

        internal static int Compare(Installation x, Installation y) {
            int result = 0;
            result = string.Compare(x.Name, y.Name, true);
            if (result == 0) {
                result = CompareVersion(x.Version, y.Version);
            }
            if (result == 0) {
                if (x.IsInstalled && !y.IsInstalled) result = -1;
                if (!x.IsInstalled && y.IsInstalled) result = 1;
            }
            return result;
        }

        internal static int CompareVersion(string x, string y) {
            int result = 0;
            string[] xv = x.Split('.');
            string[] yv = y.Split('.');
            int maxlengh = xv.Length > yv.Length ? xv.Length : yv.Length;
            for (int i = 0; i < maxlengh; i++) {
                if (result != 0) break;
                if (i < xv.Length && i < yv.Length) {
                    if (Convert.ToInt16(xv[i]) > Convert.ToInt16(yv[i])) result = 1;
                    if (Convert.ToInt16(xv[i]) < Convert.ToInt16(yv[i])) result = -1;
                } else {
                    if (i < xv.Length) result = 1;
                    if (i < yv.Length) result = -1;
                }
            }
            return result;
        }
    }

    public interface IInstallItem {
        void Update(Xy.Data.DataBase DB);
    }
}
