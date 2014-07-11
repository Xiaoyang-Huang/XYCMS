using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Action {
    public class EntityAction : Global.UserPage {
        
        public override void ValidateUser() {
            base.ValidateUser();
            if (CurrentUser == null || !CurrentUser.HasPower("BO-EntityManage")) {
                throw new Exception(WebSetting.Translate["action-ignore"]);
            }
        }

        public override Xy.Web.Page.PageErrorState onError(Exception ex) {
            Response.Write(string.Format("{{status:'{0}',message:'{1}'}}", "failed", ex.Message.Replace("'", "\\'").Replace(Environment.NewLine, "<br />")));
            return Xy.Web.Page.PageErrorState.Handled;
        }


        public override void ValidateUrl() {
            switch (Request.GroupString["class"]) {
        //        case "entity":
        //            switch (Request.GroupString["type"]) {
        //                case "list":
        //                    short _typeID = Convert.ToInt16(Request.GroupString["id"]);

        //                    int _totalCout = -1;

        //                    int _pageIndex = 0,_pageSize = 20;
        //                    string _where = string.Empty, _order = string.Empty, _filters = string.Empty;
        //                    if (!string.IsNullOrEmpty(Request.Form["PageIndex"])) _pageIndex = Convert.ToInt32(Request.Form["PageIndex"]);
        //                    if (!string.IsNullOrEmpty(Request.Form["PageSize"])) _pageSize = Convert.ToInt32(Request.Form["PageSize"]);
        //                    if (!string.IsNullOrEmpty(Request.Form["Where"])) _where = Request.Form["Where"];
        //                    if (!string.IsNullOrEmpty(Request.Form["Order"])) _order = Request.Form["Order"];
        //                    if (!string.IsNullOrEmpty(Request.Form["Filter"])) _filters = Request.Form["Filter"].Trim(',') + ',';

        //                    XiaoYang.Entity.EntityHelper _helper = new Entity.EntityHelper(_typeID);


        //                    PageData.AddXyDataModel("EntityType", Entity.EntityType.GetInstance(_typeID));
        //                    System.Data.DataTable _dt = new System.Data.DataTable();
        //                    _dt.Columns.Add("Name", typeof(string));
        //                    _dt.Columns.Add("Key", typeof(string));
        //                    _dt.Columns.Add("IsShow", typeof(bool));
        //                    for (int i = 0; i < _helper.CacheInstance.AttributeKeys.Length; i++) {
        //                        System.Data.DataRow _row = _dt.NewRow();
        //                        _row["Name"] = _helper.CacheInstance.AttributeNames[i];
        //                        _row["Key"] = _helper.CacheInstance.AttributeKeys[i];
        //                        _row["IsShow"] = _filters.IndexOf(_helper.CacheInstance.AttributeKeys[i] + ',') != -1;
        //                        _dt.Rows.Add(_row);
        //                    }
        //                    PageData.Add("Attributes", _dt);
        //                    XiaoYang.Entity.EntityCollection _ec = _helper.GetList(_where, _pageIndex, _pageSize, _order, ref _totalCout);
        //                    PageData.AddEntireXyDataModel("EntityList", _ec);
        //                    PageData["EntityList"].CreatePagination(_pageIndex, _pageSize, _totalCout, 9);
        //                    break;
        //                case "add":
        //                    XiaoYang.Entity.EntityHelper _addHelper = new Entity.EntityHelper(Convert.ToInt16(Request.GroupString["id"]));
        //                    long _addID = _addHelper.Add(Request.Form);
        //                    Response.Write("{status:'success',message:'Success',returnURL:'/entity_edit_" + _addID + "." + WebSetting.Config["ext"] + "'}");
        //                    Response.Redirect("/entity_edit_" + _addID + "." + WebSetting.Config["ext"] + "#successed");
        //                    break;
        //                case "edit":
        //                    Entity.EntityBase _editEntity = Entity.EntityBase.GetInstance(Convert.ToInt64(Request.GroupString["id"]));
        //                    XiaoYang.Entity.EntityHelper _editHelper = new Entity.EntityHelper(_editEntity.TypeID);
        //                    _editHelper.Edit(Request.Form, _editEntity.ID);
        //                    Response.Redirect("/entity_edit_" + _editEntity.ID + "." + WebSetting.Config["ext"] + "#successed");
        //                    break;
        //                case "del":
        //                    Entity.EntityBase _delEntity = Entity.EntityBase.GetInstance(Convert.ToInt64(Request.GroupString["id"]));
        //                    XiaoYang.Entity.EntityHelper _delHelper = new Entity.EntityHelper(_delEntity.TypeID);
        //                    _delHelper.Del(_delEntity.ID);
        //                    break;
        //            }
        //            break;
                case "type":
                    switch (Request.GroupString["type"]) {
                        case "add":
                            XiaoYang.Entity.EntityType.Add(Request.Form);
                            break;
                        //case "getattr":
                        //    Xy.Web.Control.DataControl _dc = new Xy.Web.Control.DataControl();
                        //    System.Collections.Specialized.NameValueCollection _dcInitTag = new System.Collections.Specialized.NameValueCollection();
                        //    Xy.Web.HTMLContainer _dcData = new Xy.Web.HTMLContainer(WebSetting.Encoding);
                        //    Xy.Web.HTMLContainer _dcContent = new Xy.Web.HTMLContainer(WebSetting.Encoding);
                        //    _dcInitTag.Add("Provider", "Procedure");
                        //    _dcInitTag.Add("Command", @"select [PostAttribute].*, [PostType].[IsActive] from [PostAttribute] left join [PostType] on [PostAttribute].[TypeID] = [PostType].[ID] where [TypeID] = @TypeID");
                        //    _dcInitTag.Add("Parameter", @"{ TypeID=""" + Request.QueryString["ID"] + @"|i"" }");
                        //    _dcInitTag.Add("EnableScript", "True");
                        //    _dcInitTag.Add("EnableCode", "True");
                        //    _dcInitTag.Add("Xslt", "attributelist.xslt");
                        //    _dc.Init(_dcInitTag, "PostAttributeControl", 999);
                        //    _dc.InnerData = _dcData;
                        //    _dc.Handle(ThreadEntity, this, _dcContent);
                        //    Response.Write(_dcContent);
                        //    break;
                        case "active":
                            XiaoYang.Entity.EntityType.EditActive(Request.Form);
                            break;
                        case "display":
                            XiaoYang.Entity.EntityType.EditDisplay(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityType.Del(Convert.ToInt16(Request.Form["ID"]));
                            break;
                    }
                    break;
                case "attr":
                    switch (Request.GroupString["type"]) {
                        case "add":
                            XiaoYang.Entity.EntityAttribute.Add(Request.Form);
                            break;
                        case "edit":
                            XiaoYang.Entity.EntityAttribute.Edit(Request.Form);
                            break;
                        case "del":
                            XiaoYang.Entity.EntityAttribute.Del(Request.Form);
                            break;
                    }
                    break;
            }
        }

        public override List<Xy.Web.Control.IControl> HandleControl(List<Xy.Web.Control.IControl> Controls) {
            return base.HandleControl(Controls);
        }

        public override Xy.Web.HTMLContainer OutputHtml(Xy.Web.HTMLContainer HTMLContainer) {
            if (!HTMLContainer.HasContent) Response.Write("{status:'success',message:'Success'}");
            return base.OutputHtml(HTMLContainer);
        }
    }
}
