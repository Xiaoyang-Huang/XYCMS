using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.User {
    public partial class User : Xy.Data.IDataModel, Xy.Web.Security.IUser {

        public string HeadImage { get; set; }
        public short Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Hometown { get; set; }
        public string Job { get; set; }
        public string Weibo { get; set; }
        public string QQ { get; set; }
        public string Description { get; set; }

        public const int DefaultUserGroupID = 2;

        public void Fill(System.Data.DataRow _tempRow) {
            System.Data.DataColumnCollection cols = _tempRow.Table.Columns;
            if (cols["ID"] != null) { this.ID = Convert.ToInt64(_tempRow["ID"]); }
            if (cols["Name"] != null) { this.Name = Convert.ToString(_tempRow["Name"]); }
            if (cols["Nickname"] != null) { this.Nickname = Convert.ToString(_tempRow["Nickname"]); }
            if (cols["Password"] != null) { this.Password = Convert.ToString(_tempRow["Password"]); }
            if (cols["Email"] != null) { this.Email = Convert.ToString(_tempRow["Email"]); }
            if (cols["UserGroup"] != null) { this.UserGroup = Convert.ToInt32(_tempRow["UserGroup"]); }
            if (cols["HeadImage"] != null) { this.HeadImage = Convert.ToString(_tempRow["HeadImage"]); }
            if (cols["Sex"] != null) { this.Sex = Convert.ToInt16(_tempRow["Sex"]); }
            if (cols["Birthday"] != null) { this.Birthday = Convert.ToDateTime(_tempRow["Birthday"]); }
            if (cols["Hometown"] != null) { this.Hometown = Convert.ToString(_tempRow["Hometown"]); }
            if (cols["Job"] != null) { this.Job = Convert.ToString(_tempRow["Job"]); }
            if (cols["Weibo"] != null) { this.Weibo = Convert.ToString(_tempRow["Weibo"]); }
            if (cols["QQ"] != null) { this.QQ = Convert.ToString(_tempRow["QQ"]); }
            if (cols["Description"] != null) { this.Description = Convert.ToString(_tempRow["Description"]); }
        }

        private static void RegisterEvents() {
            _procedures[R("EditPassword")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EditPassword_BeforeInvoke);
            _procedures[R("Add")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(Add_BeforeInvoke);
            _procedures[R("Add")].AfterInvoke += new Xy.Data.afterInvokeHandler(Add_AfterInvoke);
            _procedures[R("EditUserGroup")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(EditUserGroup_BeforeInvoke);
            _procedures[R("Del")].BeforeInvoke += new Xy.Data.beforeInvokeHandler(User_BeforeInvoke);
        }

        static void User_BeforeInvoke(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            long id = Convert.ToInt64(produce.GetItem("ID"));
            UserExtra.Del(id, DB);
        }

        private static void Add_AfterInvoke(Xy.Data.ProcedureResult result, Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            UserExtra.Add(Convert.ToInt64(result.IntResult), string.Empty, 0, new DateTime(1900, 1, 1), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DB);
        }

        private static void Add_BeforeInvoke(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            EditUserGroup_BeforeInvoke(produce, DB);
            EditPassword_BeforeInvoke(produce, DB);
        }

        private static void EditUserGroup_BeforeInvoke(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            int groupID = Convert.ToInt32(produce.GetItem("UserGroup").ToString());
            if (groupID < 0) {
                produce.SetItem("UserGroup", DefaultUserGroupID);
            }
        }

        private static void EditPassword_BeforeInvoke(Xy.Data.Procedure produce, Xy.Data.DataBase DB) {
            produce.SetItem("Password", Encrypt.Encrypt(produce.GetItem("Password").ToString()));
        }

        private static Xy.Tools.Encrypt.IEncrypt _encrypt;

        private static Xy.Tools.Encrypt.IEncrypt Encrypt {
            get {
                if (_encrypt == null) {
                    _encrypt = new Xy.Tools.Encrypt.Rijndael(
                                Xy.WebSetting.WebSettingCollection.GetWebSetting(string.Empty).EncryptKey,
                                Xy.WebSetting.WebSettingCollection.GetWebSetting(string.Empty).EncryptIV);
                }
                return _encrypt;
            }
        }

        public static Xy.Web.Security.IUser LoginByKey(string key, out string ErrorString) {
            string orgstr = key;
            ErrorString = string.Empty;
            if (Encrypt.Decrypt(ref orgstr)) {
                string[] temp = orgstr.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length == 2) {
                    return Login(temp[0], temp[1], false, out ErrorString);
                }
            }
            return null;
        }

        public static string GenerateUserKey(string inUserName, string inPassword) {
            return Encrypt.Encrypt(inUserName + "|" + inPassword);
        }

        public static Xy.Web.Security.IUser Login(string inUserName, string inPassword, bool inOrginalPassword, out string ErrorString) {
            ErrorString = string.Empty;
            Xy.Data.Procedure tp = User.GetProcedure(R("Login"));
            tp.SetItem("Name", inUserName);
            if (inOrginalPassword) inPassword = Encrypt.Encrypt(inPassword);
            tp.SetItem("Password", inPassword);
            System.Data.DataTable ti = tp.InvokeProcedureFill();
            if (ti.Rows.Count > 0) {
                User tu = new User();
                tu.Fill(ti.Rows[0]);
                return tu;
            } else {
                ErrorString = "该用户未注册 或 用户密码错误";
                Xy.Tools.Web.Cookie.Del("UserKey");
            }
            return null;
        }

        public static void Logout() {
            Xy.Tools.Web.Cookie.Del("UserKey");
        }

        #region 权限部分
        private UserPermission _userPermission;
        public UserPermission UserPermission {
            get {
                if (_userPermission == null) {
                    _userPermission = UserPermissionCollection.GetInstance(this.UserGroup);
                }
                return _userPermission;
            }
        }

        public bool HasPower(int powerId) {
            return this.UserPermission.HasPower(powerId);
        }

        public bool HasPower(string powerKey) {
            return this.UserPermission.HasPower(powerKey);
        }

        public bool InGroup(int groupId) {
            return this.UserPermission.InGroup(groupId);
        }

        public bool InGroup(string groupKey) {
            return this.UserPermission.InGroup(groupKey);
        }
        #endregion


        public void WriteCookie(int Expire, string Domain) {
            string userKey = GenerateUserKey(Name, Password);
            if (string.IsNullOrEmpty(Xy.Tools.Web.Cookie.Get("UserKey")) || Xy.Tools.Web.Cookie.Get("UserKey").CompareTo(userKey) != 0) {
                Xy.Tools.Web.Cookie.Add("UserKey", userKey, Expire, Domain);
            }
        }
    }

    public class UserPermissionCollection {
        private static Dictionary<int, UserPermission> _instanceList = null;
        public static UserPermission GetInstance(int GroupId) {
            if (_instanceList == null) _instanceList = new Dictionary<int, UserPermission>();
            if (_instanceList.ContainsKey(GroupId)) {
                return _instanceList[GroupId].Clone();
            } else {
                UserPermission _temp = new UserPermission(GroupId);
                try {
                    _instanceList.Add(GroupId, _temp);
                } catch (Exception ex) {
                    Xy.Tools.Debug.Log.WriteErrorLog(string.Format("an error occured on UserPermissionCollection: {0}", ex.Message));
                }
                return _temp;
            }
        }
        public static void ClearCache() {
            PowerList.ClearCache();
            UserGroup.ClearCache();
            PowerShip.ClearCache();
            _instanceList = null;
        }
    }

    public class UserPermission {
        public List<int> GroupListID { get; private set; }
        public List<string> GroupListKey { get; private set; }
        public List<int> PowerListID { get; private set; }
        public List<string> PowerListKey { get; private set; }

        private UserPermission() {
        }

        public UserPermission(int GroupId) {
            UserGroup tempGroup = UserGroup.GetInstance(GroupId);
            GroupListID = new List<int>();
            GroupListKey = new List<string>();
            PowerListID = new List<int>();
            PowerListKey = new List<string>();
            if (tempGroup != null) {
                GroupListID.Add(tempGroup.ID);
                GroupListKey.Add(tempGroup.Key);

                System.Data.DataTable _dt = PowerShip.GetByGroup(tempGroup.ID);
                foreach (System.Data.DataRow _row in _dt.Rows) {
                    PowerListID.Add(Convert.ToInt32(_row["PowerList"]));
                }
                foreach (int _id in PowerListID) {
                    PowerList _temp = PowerList.GetInstance(_id);
                    if (_temp != null) PowerListKey.Add(_temp.Key);
                }
            }
        }

        public void AddPower(int GroupId) {
            UserPermission _temp = UserPermissionCollection.GetInstance(GroupId);
            this.GroupListID.AddRange(_temp.GroupListID);
            this.GroupListKey.AddRange(_temp.GroupListKey);
            this.PowerListID.AddRange(_temp.PowerListID);
            this.PowerListKey.AddRange(_temp.PowerListKey);
        }


        public bool HasPower(int powerId) {
            foreach (int _temp in PowerListID) {
                if (_temp == powerId) return true;
            }
            return false;
        }

        public bool HasPower(string powerKey) {
            foreach (string _temp in PowerListKey) {
                if (string.Compare(_temp, powerKey, true) == 0) return true;
            }
            return false;
        }

        public bool InGroup(int groupId) {
            foreach (int _temp in GroupListID) {
                if (_temp == groupId) return true;
            }
            return false;
        }

        public bool InGroup(string groupKey) {
            foreach (string _temp in GroupListKey) {
                if (string.Compare(_temp, groupKey, true) == 0) return true;
            }
            return false;
        }

        public UserPermission Clone() {
            return new UserPermission() {
                GroupListID = new List<int>(this.GroupListID),
                GroupListKey = new List<string>(this.GroupListKey),
                PowerListID = new List<int>(this.PowerListID),
                PowerListKey = new List<string>(this.PowerListKey)
            };
        }
    }
}
