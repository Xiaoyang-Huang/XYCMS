using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopControl {
    class Program {
        static void Main(string[] args) {
            //XiaoYang.Installation.InstallationCollection ic = new XiaoYang.Installation.InstallationCollection();
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test1", Version = "1.0", IsInstalled = true });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test3", Version = "1.1", IsInstalled = true });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test3", Version = "1.11", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test2", Version = "1.2", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test2", Version = "1.2.1", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test2", Version = "1.3", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test3", Version = "1.3", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test3", Version = "1.0", IsInstalled = true });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test1", Version = "1.2", IsInstalled = false });
            //ic.Add(new XiaoYang.Installation.Installation() { Name = "test1", Version = "1.3", IsInstalled = false });
            //ic.Arrange(false);
            //foreach (XiaoYang.Installation.Installation _item in ic) {
            //    Console.WriteLine(string.Format("{0}\t{1}\t{2}", _item.Name, _item.Version, _item.IsInstalled));
            //}

            //Console.Write("abcd".Substring(1, 1) + "abcd".Substring(2));
            //List<string> 我们校花 = new List<string>();
            //我们校花.Add("刘春花");
            //我们校花.Add("刘夏花");
            //我们校花.Add("刘秋花");
            //我们校花.Add("刘冬花");
            //foreach (string 人员 in 我们校花) {
            //    Console.WriteLine(人员.Substring(1, 1));
            //}


            //XiaoYang.Entity.EntityHelper entity = new XiaoYang.Entity.EntityHelper(19);
            //Console.WriteLine(Xy.Tools.IO.XML.ConvertDataTableToXMLWithFormat(_tb));
            //System.Collections.Specialized.NameValueCollection _nvc = new System.Collections.Specialized.NameValueCollection();
            //_nvc.Add("Title", "Title不影响子属性4");
            //_nvc.Add("SubTitle", "SubTitle不影响子属性4");
            //_nvc.Add("AnotherTitle", "AnotherTitle不影响子属性4");
            //_nvc.Add("ImageList", "用来测试多属性_列表123,用来测试多属性_列表234,用来测试多属性_列表456,用来测试多属性_列表789");
            //_nvc.Add("PhotoList", "用来测试多属性_照片987,用来测试多属性_照片876");
            //long _id = entity.Add(_nvc);
            //Console.WriteLine(_id);
            //Console.Write(Xy.Tools.IO.XML.ConvertDataTableToXMLWithFormat(entity.Get(_id)));


            //Console.WriteLine(new System.Xml.XPath.XPathDocument(new System.IO.StringReader(string.Empty)));

            //System.Data.DataTable _tb = entity.Get(25);
            //Xy.Data.IDataModelDisplay _display = (Xy.Data.IDataModelDisplay)new XiaoYang.Entity.EntityCollection(_tb);
            //Console.WriteLine(_display.GetXml().CreateNavigator().OuterXml);

            //string[] _array1 = new string[] { "a", "b", "c" };
            //string[] _array2 = new string[] { "d", "e", "f", "g" };
            //string[] _array3 = new string[10];
            //_array1.CopyTo(_array3, 0);
            //Console.WriteLine(string.Join(",", _array1));
            //Console.WriteLine(string.Join(",", _array2));
            //Console.WriteLine(string.Join(",", _array3));
            //XiaoYang.Entity.EntityHelper _helper = new XiaoYang.Entity.EntityHelper(25);
            //int rowCount = -1;
            //Xy.Data.IDataModelDisplay _display = (Xy.Data.IDataModelDisplay)_helper.GetList(string.Empty, 0, 10, "ID desc", ref rowCount);
            //Console.WriteLine(_display.GetXml().CreateNavigator().OuterXml);

            //XiaoYang.Entity.DefaultHandler _handle = new XiaoYang.Entity.DefaultHandler();
            //_handle.Init(8);
            //System.Collections.Specialized.NameValueCollection _nvc = new System.Collections.Specialized.NameValueCollection();
            //_nvc.Add("IsActive", "False");
            //_nvc.Add("Title", "testTitle");
            //_nvc.Add("Content", "testContent");
            //_handle.Add(_nvc);

//            System.Data.SqlClient.SqlConnection _con = new System.Data.SqlClient.SqlConnection(Xy.DataSetting.DataSettingCollection.GetDataBaseItem("Default").ConnectionString);
//            System.Data.SqlClient.SqlCommand _cmd = new System.Data.SqlClient.SqlCommand(@"declare @PrimaryKey bigint 
//set @PrimaryKey = 1
//select * from [Entity_testKey] where [ID] = @PrimaryKey
//select * from [Entity_Article] where [ID] = 20", _con);
//            _con.Open();
//            try {
//                System.Data.SqlClient.SqlDataAdapter _sda = new System.Data.SqlClient.SqlDataAdapter(_cmd);
//                System.Data.DataSet _ds = new System.Data.DataSet();
//                _sda.Fill(_ds);
//                Console.WriteLine("Table count:"+_ds.Tables.Count);
//                for (int i = 0; i < _ds.Tables.Count; i++) {
//                    System.Data.DataTable _dt = _ds.Tables[i];
//                    //Console.WriteLine(Xy.Tools.Debug.Decompose.DecomposeObject(_dt));
//                    Console.WriteLine("==============================");
//                }
//            } catch (Exception ex) {
//                Console.WriteLine(ex.Message);
//            } finally {
//                _con.Close();
//            }
            //XiaoYang.Entity.DefaultHandler _dh = new XiaoYang.Entity.DefaultHandler();
            //_dh.Init(8, new Xy.Data.DataBase());
            //System.Collections.Specialized.NameValueCollection _nvc = new System.Collections.Specialized.NameValueCollection();
            //_nvc.Add("ID", "1");
            //XiaoYang.Entity.Entity _entity = _dh.Get(_nvc);
            //Console.WriteLine(_entity.GetAttributesValue("Content"));

            Xy.Tools.Debug.TimeWatch _tw = new Xy.Tools.Debug.TimeWatch();
            _tw.WatchEvent += _tw_WatchEvent1;
            _tw.WatchEvent += _tw_WatchEvent2;
            _tw.WatchEvent += _tw_WatchEvent3;
            _tw.Watch(100000, 3, true);

            //_tw_WatchEvent3();
        }

        static void _tw_WatchEvent3() {
            System.Xml.XPath.XPathNavigator _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlString)).CreateNavigator();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(8);
            StringBuilder _command = new StringBuilder();
            int _fieldPoint = 0;
            //_command.Append("DECLARE @PrimaryKey ").Append(_cache.PrimaryField.SqlDeclare).AppendLine(";");
            for (int i = 0; i < _cache.TableList.Count; i++) {
                XiaoYang.Entity.EntityTable _table = _cache.TableList[i];
                System.Xml.XPath.XPathNodeIterator _tableIter = _xml.Select(_cache.Type.Key + "/" + _table.Key);
                if (_tableIter.Count > 0) {
                    int _tableFieldStart = _fieldPoint;
                    while (_tableIter.MoveNext()) {
                        //Console.WriteLine(_table.Key + ":");
                        bool _edit = false;
                        int _commandStart = _command.Length;
                        _command.Append("Insert into [").Append(_table.Key).AppendLine("](").Append("\t");
                        int _parameterOffset = _command.Length;
                        _command.AppendLine().AppendLine(")VALUES(").Append("\t");
                        int _valueOffset = _command.Length;
                        _command.AppendLine().AppendLine(");");
                        if (_table.Main) _command.AppendLine("set @PrimaryKey = SCOPE_IDENTITY();");
                        int _commandLength = _command.Length;
                        _fieldPoint = _tableFieldStart;
                        for (int j = _fieldPoint; j < _cache.FieldList.Count; j = _fieldPoint) {
                            XiaoYang.Entity.EntityField _field = _cache.FieldList[j];
                            if (_field.Table != _table) break;
                            if (_field.Increase) {
                                _fieldPoint++; continue;
                            }
                            if (_field.Foreign) {
                                _command.Insert(_parameterOffset, string.Format("{1}[{0}]", _field.Key, _edit ? "," : string.Empty));
                                _parameterOffset += _command.Length - _commandLength; _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _command.Insert(_valueOffset, string.Format("{0}@PrimaryKey", _edit ? "," : string.Empty));
                                _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _fieldPoint++; _edit = true; continue;
                            }
                            System.Xml.XPath.XPathNavigator _value = _tableIter.Current.SelectSingleNode(_field.Key);
                            if (_value != null) {
                                _command.Insert(_parameterOffset, string.Format("{1}[{0}]", _field.Key, _edit ? "," : string.Empty));
                                _parameterOffset += _command.Length - _commandLength; _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _command.Insert(_valueOffset, string.Format("{1}'{0}'", _value.Value, _edit ? "," : string.Empty));
                                _valueOffset += _command.Length - _commandLength; _commandLength = _command.Length;
                                _edit = true;
                            }
                            _fieldPoint++;
                        }
                    }
                }
            }
            //Console.WriteLine(_command);
            //Xy.Data.ProcedureParameter _primaryKey = new Xy.Data.ProcedureParameter("PrimaryKey", System.Data.DbType.Int64);
            //_primaryKey.Value = 0;
            //_primaryKey.Direction = System.Data.ParameterDirection.InputOutput;
            //Xy.Data.Procedure _procedure = new Xy.Data.Procedure("AddNew", _command.ToString());
            //_procedure.AddItem(_primaryKey);
            //_procedure.InvokeProcedure();
            //long id = Convert.ToInt64(_procedure.GetItem("PrimaryKey"));
            //Console.WriteLine(id);
        }

        static void _tw_WatchEvent2() {
            System.Xml.XPath.XPathNavigator _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlString)).CreateNavigator();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(8);
            int _fieldPoint = 0;
            for (int i = 0; i < _cache.TableList.Count; i++) {
                XiaoYang.Entity.EntityTable _table = _cache.TableList[i];
                System.Xml.XPath.XPathNodeIterator _tableIter = _xml.Select(_cache.Type.Key + "/" + _table.Key);
                if (_tableIter.Count > 0) {
                    int _tableFieldStart = _fieldPoint;
                    while (_tableIter.MoveNext()) {
                        //Console.WriteLine(_table.Key + ":");
                        _fieldPoint = _tableFieldStart;
                        for (int j = _fieldPoint; j < _cache.FieldList.Count; j = _fieldPoint) {
                            XiaoYang.Entity.EntityField _field = _cache.FieldList[j];
                            if (_field.Table != _table) break;
                            System.Xml.XPath.XPathNavigator _value = _tableIter.Current.SelectSingleNode(_field.Key);
                            if (_value != null) {
                                //Console.WriteLine("\t" + _field.Key + " | " + _value.Value);
                            }
                            _fieldPoint++;
                        }
                    }
                }
                
            }
        }

        static void _tw_WatchEvent1() {
            System.Xml.XPath.XPathNavigator _xml = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(_xmlString)).CreateNavigator();
            XiaoYang.Entity.Cache _cache = XiaoYang.Entity.CacheManager.Get(8);
            //StringBuilder _command = new StringBuilder();
            //_command.Append("DECLARE @PrimaryKey ").Append(_cache.PrimaryField.SqlDeclare).AppendLine(";");
            for (int i = 0; i < _cache.TableList.Count; i++) {
                XiaoYang.Entity.EntityTable _table = _cache.TableList[i];
                System.Xml.XPath.XPathNodeIterator _tableIter = _xml.Select(_cache.Type.Key + "/" + _table.Key);
                if (_tableIter.Count > 0) {
                    while (_tableIter.MoveNext()) {
                        //Console.WriteLine(_tableIter.Current.OuterXml);
                        //Console.WriteLine(_tableIter.Current.Name + ":");
                        System.Xml.XPath.XPathNodeIterator _fieldIter = _tableIter.Current.Select("*");
                        if (_fieldIter.Count > 0) {
                            while (_fieldIter.MoveNext()) {
                                //Console.WriteLine(_fieldIter.Current.OuterXml);
                                System.Xml.XPath.XPathNavigator _current = _fieldIter.Current;
                                //Console.WriteLine("\t" + _current.Name + " | " + _current.Value);
                            }
                        }

                    }
                }
            }
        }

        static string _xmlString = @"
<testKey>
    <Entity_testKey>
        <IsActive>True</IsActive>
    </Entity_testKey>
    <Entity_Article>
        <Title>中文标题</Title>
        <Content>中文内容</Content>
    </Entity_Article>
    <Entity_Article>
        <Title>English Title</Title>
        <Content>English Content</Content>
    </Entity_Article>
</testKey>
";

        #region Multiple attribute test
//        static void Main(string[] args) {
//            Xy.Tools.Debug.TimeWatch _tw = new Xy.Tools.Debug.TimeWatch();
//            _tw.WatchEvent += new Xy.Tools.Debug.TimeWatch.WatchFunction(_tw_WatchEvent1);
//            _tw.WatchEvent += new Xy.Tools.Debug.TimeWatch.WatchFunction(_tw_WatchEvent2);
//            _tw.WatchEvent += new Xy.Tools.Debug.TimeWatch.WatchFunction(_tw_WatchEvent3);
//            _tw.Watch(100, 10, true);
//        }
//        static void _tw_WatchEvent3() {
//            Xy.Data.Procedure _procedure1 = new Xy.Data.Procedure("get1",
//               @"select base.*
//, [Entity_Test].[Title]
//, [Entity_TestChild].[SubTitle]
//, [Entity_TestChild].[AnotherTitle]
//, [Entity_TestChild_ImageList].[ID] as 'ImageListID',[Entity_TestChild_ImageList].[ImageList]
//, [Entity_TestChild_PhotoList].[ID] as 'PhotoListID',[Entity_TestChild_PhotoList].[PhotoList]
//from [EntityBase] base
//left join [Entity_TestChild] on [Entity_TestChild].[EntityID] = base.[ID]
//left join [Entity_Test] on [Entity_Test].[EntityID] = base.[ID]
//left join [Entity_TestChild_ImageList] on [Entity_TestChild_ImageList].[EntityID] = base.[ID]
//left join [Entity_TestChild_PhotoList] on [Entity_TestChild_PhotoList].[EntityID] = base.[ID]
//where [TypeID] = 19 and  base.[ID] in (15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25)");
//            _procedure1.InvokeProcedureFill();
//        }

//        static long[] _ids = new long[] { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
//        static Xy.Data.Procedure _procedure1 = new Xy.Data.Procedure("get1",
//@"select base.*
//, [Entity_Test].[Title]
//, [Entity_TestChild].[SubTitle]
//, [Entity_TestChild].[AnotherTitle]
//from [EntityBase] base
//left join [Entity_TestChild] on [Entity_TestChild].[EntityID] = base.[ID]
//left join [Entity_Test] on [Entity_Test].[EntityID] = base.[ID]
//where [TypeID] = 19 and  base.[ID] in (15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25)", new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64)),
//                                _procedure2 = new Xy.Data.Procedure("get2",
//@"select [Entity_TestChild_ImageList].[ID] as 'ImageListID',[Entity_TestChild_ImageList].[ImageList]
//from [Entity_TestChild_ImageList] where [EntityID] = @ID", new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64)),
//                                _procedure3 = new Xy.Data.Procedure("get3",
//@"select [Entity_TestChild_PhotoList].[ID] as 'PhotoListID',[Entity_TestChild_PhotoList].[PhotoList]
//from [Entity_TestChild_PhotoList] where [EntityID] = @ID", new Xy.Data.ProcedureParameter("ID", System.Data.DbType.Int64));
//        static bool _inited = false;
//        static void _tw_WatchEvent2() {
//            foreach (long _id in _ids) {
//                if (!_inited) {
//                    _procedure1.SetItem("ID", _id);
//                    _procedure1.InvokeProcedureFill();
//                    _inited = true;
//                }
//                _procedure2.SetItem("ID", _id);
//                _procedure2.InvokeProcedureFill();
//                _procedure3.SetItem("ID", _id);
//                _procedure3.InvokeProcedureFill();
//            }
//            _inited = false;
//        }

//        static void _tw_WatchEvent1() {
//            XiaoYang.Entity.EntityHelper entity = new XiaoYang.Entity.EntityHelper(19);
//            System.Data.DataTable _tb = entity.Get(15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25);
//            Xy.Data.IDataModelDisplay _display = (Xy.Data.IDataModelDisplay)new XiaoYang.Entity.EntityCollection(_tb);
//            string result = _display.GetXml().CreateNavigator().OuterXml;
//        }
        #endregion

        static void InitWebSite() {
            Console.Write("Make sure you want re-install all data in this website? [Y/N] ");
            string inputKey = Console.ReadLine();
            if (string.Compare(inputKey, "Y", true) == 0) {
                Console.WriteLine("re-install now, please wait..." + Environment.NewLine);
                Xy.Data.DataBase db = new Xy.Data.DataBase("Update");

                ClearData(db);
                InsertProcedure(db);
                InsertData(db);

                Console.Write(Environment.NewLine + "Done! ");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void ClearData(Xy.Data.DataBase db) {
            db.Open();
            db.StartTransaction();
            List<Xy.Data.Procedure> deleteProcess = new List<Xy.Data.Procedure>();
            deleteProcess.Add(new Xy.Data.Procedure("DeleteUserExtra", @"delete from [UserExtra];"));
            deleteProcess.Add(new Xy.Data.Procedure("DeleteUser", @"delete from [User]; DBCC CHECKIDENT ('User', RESEED, 0);"));
            deleteProcess.Add(new Xy.Data.Procedure("DeletePowerList", @"delete from [PowerList]; DBCC CHECKIDENT ('PowerList', RESEED, 0);delete from [PowerShip]; DBCC CHECKIDENT ('PowerShip', RESEED, 0);"));
            deleteProcess.Add(new Xy.Data.Procedure("DeleteUserGroup", @"delete from [UserGroup]; DBCC CHECKIDENT ('UserGroup', RESEED, 0);"));
            deleteProcess.Add(new Xy.Data.Procedure("DeleteInstallation", @"delete from [Installation]; DBCC CHECKIDENT ('Installation', RESEED, 0);"));
            //deleteProcess.Add(new Xy.Data.Procedure("deletePostType", @"delete from [PostType]; DBCC CHECKIDENT ('PostType', RESEED, 0);delete from [PostAttribute]; DBCC CHECKIDENT ('PostAttribute', RESEED, 0);"));
            try {
                Console.WriteLine("\tDelete Data");
                foreach (Xy.Data.Procedure _item in deleteProcess) {
                    Console.WriteLine("\t" + _item.Name);
                    _item.InvokeProcedure(db);
                }
                db.CommitTransaction();
            } catch(Exception ex) {
                db.RollbackTransation();
            } finally {
                db.Close();
            }
        }

        static void InsertProcedure(Xy.Data.DataBase db) {
        }

        static void InsertData(Xy.Data.DataBase db) {
            Console.WriteLine("\tInsert init data");
            //group
            int _adminGroup = XiaoYang.User.UserGroup.Add("系统超级管理员", "SuperAdmin");
            int _userGroup = XiaoYang.User.UserGroup.Add("普通用户", "User");
            //permission
            XiaoYang.User.PowerShip.Add(XiaoYang.User.PowerList.Add("BO-Login", "登录后台"), _adminGroup);
            XiaoYang.User.PowerShip.Add(XiaoYang.User.PowerList.Add("BO-PowerManage", "权限管理"), _adminGroup);
            XiaoYang.User.PowerShip.Add(XiaoYang.User.PowerList.Add("BO-UserManage", "用户管理"), _adminGroup);
            XiaoYang.User.PowerShip.Add(XiaoYang.User.PowerList.Add("BO-PostManage", "用户管理"), _adminGroup);
            //user
            XiaoYang.User.User.Add("XyAdmin", "Super Admin", "XyAdmin", "110752897@qq.com", 1);
        }
    }
}
