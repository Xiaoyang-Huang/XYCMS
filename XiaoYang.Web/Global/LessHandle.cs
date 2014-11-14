using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Web.Global {
    public class LessHandler: Xy.Web.Page.PageAbstract {
        public override void onGetRequest() {
            string _path = Request.PhysicalPath;

            dotless.Core.configuration.DotlessConfiguration _config = new dotless.Core.configuration.DotlessConfiguration();
            if (_path.IndexOf("compress.less") > -1) {
                _config.MinifyOutput = true;
                _path = _path.Replace("_compress.less", ".less");
            }

            string _lessContet = System.IO.File.ReadAllText(_path, WebSetting.Encoding);
            _lessContet = dotless.Core.Less.Parse(_lessContet, _config);
            Response.Write(_lessContet);
            End();
        }
    }
}