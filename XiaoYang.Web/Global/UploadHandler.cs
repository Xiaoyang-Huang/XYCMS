using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;


namespace XiaoYang.Web.Global {
    public class UEditorHandler : UserPage {
        private String url = String.Empty;
        private String title = String.Empty;
        private String state = "SUCCESS";
        private string error = String.Empty;
        private string UploadPath = "/Upload/PostImage/";

        private string returnString = String.Empty;
        private Random rnd = new Random();

        public override void ValidateUser() {
            if (CurrentUser == null) {
                End();
            } else {
                base.ValidateUser();
            }
        }

        public override void ValidateUrl() {
            switch (Request.GroupString["type"]) {
                case "uploadimage":
                    UploadImage();
                    break;
                case "imageManager":
                    ImageManager();
                    break;
                case "uploadfile":
                    UploadFile();
                    break;
                case "getmovie":
                    GetMovie();
                    break;
                case "getremoteimage":
                    GetRemoteImage();
                    break;
            }
            Response.Write(returnString);
        }

        private void ImageManager() {
            //string path = Server.MapPath(UploadPath + string.Format("/{0}/", CurrentUser.ID));                  //最好使用缩略图地址，否则当网速慢时可能会造成严重的延时
            string path = Server.MapPath(UploadPath);                  //最好使用缩略图地址，否则当网速慢时可能会造成严重的延时
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                    //文件允许格式

            string action = Server.HtmlEncode(Request["action"]);

            if (action == "get") {
                String str = String.Empty;
                DirectoryInfo info = new DirectoryInfo(path);

                //目录验证
                if (info.Exists) {
                    foreach (FileInfo fi in info.GetFiles()) {
                        if (Array.IndexOf(filetype, fi.Extension) != -1) {
                            str += fi.Name + "ue_separate_ue";
                        }
                    }
                }
                returnString = str;
            }
        }

        private void UploadImage() {
            //上传配置
            //String pathbase = UploadPath +string.Format("/{0}/", CurrentUser.ID);                                      //保存路径
            String pathbase = UploadPath;                                      //保存路径
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };          //文件允许格式
            int size = 1024;                                                          //文件大小限制，单位KB

            //文件上传状态,初始默认成功，可选参数{"SUCCESS","ERROR","SIZE","TYPE"}



            String filename = String.Empty;
            String currentType = String.Empty;
            String uploadpath = String.Empty;

            uploadpath = Server.MapPath(pathbase);

            try {
                HttpPostedFile uploadFile = Request.Files[0];
                title = uploadFile.FileName;

                //目录验证
                if (!Directory.Exists(uploadpath)) {
                    Directory.CreateDirectory(uploadpath);
                }

                if (!Directory.Exists(uploadpath + "/120x120/")) {
                    Directory.CreateDirectory(uploadpath + "/120x120/");
                }

                //格式验证
                string[] temp = title.Split('.');
                currentType = "." + temp[temp.Length - 1];
                if (Array.IndexOf(filetype, currentType) == -1) {
                    state = "TYPE";
                }

                //大小验证
                if (uploadFile.ContentLength / 1024 > size) {
                    state = "SIZE";
                }

                //保存图片
                if (state == "SUCCESS") {
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + '-' + rnd.Next() + currentType;
                    uploadFile.SaveAs(uploadpath + filename);

                    Xy.Tools.ImageHandler _IH = new Xy.Tools.ImageHandler(title, uploadFile.InputStream);
                    _IH.GetImage(120, 120, Xy.Tools.ImageHandler.ResizeType.stretch, Xy.Tools.ImageHandler.ResizeType.ignore);
                    _IH.Save(uploadpath + "/120x120/" + filename);
                    url = pathbase + filename;
                }
            } catch (Exception ex) {
                state = "ERROR";
                error = ex.Message;
            }

            //获取图片描述
            if (Request.Form["pictitle"] != null) {
                if (!String.IsNullOrEmpty(Request.Form["pictitle"])) {
                    title = Request.Form["pictitle"];
                }
            }

            url = url.Replace("../", "");
            returnString = "{'url':'" + url + "','title':'" + title + "','state':'" + state + "'" + (String.IsNullOrEmpty(error) ? String.Empty : (",'error':'" + error + "'").Replace(Environment.NewLine, String.Empty)) + "}";
        }

        private void UploadFile() {
            //上传配置
            String pathbase = UploadPath;                                      //保存路径
            string[] filetype = { ".rar", ".doc", ".docx", ".zip", ".pdf", ".txt", ".swf", ".wmv" };    //文件允许格式
            int size = 100;   //文件大小限制,单位MB,同时在web.config里配置环境默认为100MB

            //文件上传状态,当成功时返回SUCCESS,其余值将直接返回对应字符串
            String state = "SUCCESS";

            String title = String.Empty;
            String filename = String.Empty;
            String url = String.Empty;
            String currentType = String.Empty;
            String uploadpath = String.Empty;

            uploadpath = Server.MapPath(pathbase);

            try {
                HttpPostedFile uploadFile = Request.Files["upfile"];
                title = uploadFile.FileName;

                //目录验证
                if (!Directory.Exists(uploadpath)) {
                    Directory.CreateDirectory(uploadpath);
                }
                if (uploadFile == null) {
                    returnString = "{'state':'文件大小可能超出服务器环境配置！','url':'null','fileType':'null'}";
                }

                //格式验证
                string[] temp = uploadFile.FileName.Split('.');
                currentType = "." + temp[temp.Length - 1].ToLower();
                if (Array.IndexOf(filetype, currentType) == -1) {
                    state = "不支持的文件类型！";
                }

                //大小验证
                if ((uploadFile.ContentLength / 1024) / 1024 > size) {
                    state = "文件大小超出限制！";
                }

                //保存图片
                if (state == "SUCCESS") {
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + '-' + rnd.Next() + currentType;
                    uploadFile.SaveAs(uploadpath + filename);
                    url = pathbase + filename;
                }
            } catch (Exception) {
                state = "文件保存失败";
            }
            //向浏览器返回数据json数据
            returnString = "{'state':'" + state + "','url':'" + url + "','fileType':'" + currentType + "'}";
        }

        private void GetMovie() {
            string key = Server.HtmlEncode(Request.Form["searchKey"]);
            string type = Server.HtmlEncode(Request.Form["videoType"]);

            Uri httpURL = new Uri("http://api.tudou.com/v3/gw?method=item.search&appKey=myKey&format=json&kw=" + key + "&pageNo=1&pageSize=20&channelId=" + type + "&inDays=7&media=v&sort=s");
            System.Net.WebClient MyWebClient = new System.Net.WebClient();


            MyWebClient.Credentials = System.Net.CredentialCache.DefaultCredentials;           //获取或设置用于向Internet资源的请求进行身份验证的网络凭据
            Byte[] pageData = MyWebClient.DownloadData(httpURL);

            returnString = Encoding.UTF8.GetString(pageData);
        }

        private void GetRemoteImage() {
            string savePath = Server.MapPath(UploadPath);       //保存文件地址
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };             //文件允许格式
            int fileSize = 3000;                                                        //文件大小限制，单位kb

            string uri = Server.HtmlEncode(Request["upfile"]);
            uri = uri.Replace("&amp;", "&");
            string[] imgUrls = System.Text.RegularExpressions.Regex.Split(uri, "ue_separate_ue", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Collections.ArrayList tmpNames = new System.Collections.ArrayList();
            System.Net.WebClient wc = new System.Net.WebClient();
            System.Net.HttpWebResponse res;
            String tmpName = String.Empty;
            String imgUrl = String.Empty;
            String currentType = String.Empty;

            try {
                for (int i = 0, len = imgUrls.Length; i < len; i++) {
                    imgUrl = imgUrls[i];

                    if (imgUrl.Substring(0, 7) != "http://") {
                        tmpNames.Add("error!");
                        continue;
                    }

                    //格式验证
                    int temp = imgUrl.LastIndexOf('.');
                    currentType = imgUrl.Substring(temp).ToLower();
                    if (Array.IndexOf(filetype, currentType) == -1) {
                        tmpNames.Add("error!");
                        continue;
                    }

                    res = (System.Net.HttpWebResponse)System.Net.WebRequest.Create(imgUrl).GetResponse();
                    //http检测
                    if (res.ResponseUri.Scheme.ToLower().Trim() != "http") {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //大小验证
                    if (res.ContentLength > fileSize * 1024) {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //死链验证
                    if (res.StatusCode != System.Net.HttpStatusCode.OK) {
                        tmpNames.Add("error!");
                        continue;
                    }
                    //检查mime类型
                    if (res.ContentType.IndexOf("image") == -1) {
                        tmpNames.Add("error!");
                        continue;
                    }
                    res.Close();

                    //创建保存位置
                    if (!Directory.Exists(savePath)) {
                        Directory.CreateDirectory(savePath);
                    }

                    //写入文件
                    tmpName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + '-' + rnd.Next() + currentType;
                    wc.DownloadFile(imgUrl, savePath + tmpName);
                    tmpNames.Add(tmpName);
                }
            } catch (Exception) {
                tmpNames.Add("error!");
            } finally {
                wc.Dispose();
            }
            returnString = "{url:'" + converToString(tmpNames) + "',tip:'远程图片抓取成功！',srcUrl:'" + uri + "'}";
        }

        private string converToString(System.Collections.ArrayList tmpNames) {
            String str = String.Empty;
            for (int i = 0, len = tmpNames.Count; i < len; i++) {
                str += tmpNames[i] + "ue_separate_ue";
                if (i == tmpNames.Count - 1)
                    str += tmpNames[i];
            }
            return str;
        }
    }

    public class SWFUploadHandler : UserPage {
        private string returnString = string.Empty;
        private Random rnd = new Random();

        public override void ValidateUser() {
            if (CurrentUser == null) {
                End();
            } else {
                base.ValidateUser();
            }
        }

        public override void ValidateUrl() {

            Xy.Tools.ImageHandler _IH = null;
            string UploadPath = "/Upload/";
            string currentType = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["path"])) {
                UploadPath += Request.QueryString["path"] + "/";
            }

            try {
                // Get the data
                HttpPostedFile upload_file = Request.Files["Filedata"];
                _IH = new Xy.Tools.ImageHandler(upload_file.FileName, upload_file.InputStream);

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + '-' + rnd.Next() + _IH.Type;

                string relativepath, saveFolder;


                switch (_IH.Type) {
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                    case ".png":
                    case ".gif":
                        relativepath = UploadPath + "/original/";
                        saveFolder = Server.MapPath(relativepath);
                        if (!System.IO.Directory.Exists(saveFolder)) {
                            System.IO.Directory.CreateDirectory(saveFolder);
                        }
                        _IH.Save(saveFolder + fileName);

                        int target_width, target_height;
                        Xy.Tools.ImageHandler.ResizeType target_width_resize, target_height_resize;
                        if (!string.IsNullOrEmpty(Request.QueryString["Thumbnail"])) {
                            string[] sizes = Request.QueryString["Thumbnail"].Split(',');
                            for (int i = 0; i < sizes.Length; i++) {
                                target_width = target_height = 0; target_width_resize = target_height_resize = 0;
                                string[] size = sizes[i].Split('|');
                                string[] types;
                                for (int j = 0; j < 4; j++) {
                                    switch (j) {
                                        case 0:
                                            target_width = Convert.ToInt32(size[j]);
                                            break;
                                        case 1:
                                            target_height = Convert.ToInt32(size[j]);
                                            break;
                                        case 2:
                                            types = size[j].Split('&');
                                            foreach (string type in types) {
                                                target_width_resize = target_width_resize | (Xy.Tools.ImageHandler.ResizeType)Convert.ToInt32(type);
                                            }
                                            break;
                                        case 3:
                                            types = size[j].Split('&');
                                            foreach (string type in types) {
                                                target_height_resize = target_height_resize | (Xy.Tools.ImageHandler.ResizeType)Convert.ToInt32(type);
                                            }
                                            break;
                                    }
                                }
                                _IH.GetImage(target_width, target_height, target_width_resize, target_height_resize);

                                relativepath = UploadPath + string.Format("/{0}x{1}/", target_width, target_height);
                                saveFolder = Server.MapPath(relativepath);
                                if (!System.IO.Directory.Exists(saveFolder)) {
                                    System.IO.Directory.CreateDirectory(saveFolder);
                                }
                                _IH.Save(saveFolder + fileName);
                                //returnString += "?" + relativepath + fileName;
                            }
                        }
                        break;
                    default:
                        relativepath = UploadPath + "/other/";
                        saveFolder = Server.MapPath(relativepath);
                        if (!System.IO.Directory.Exists(saveFolder)) {
                            System.IO.Directory.CreateDirectory(saveFolder);
                        }
                        upload_file.SaveAs(saveFolder + fileName);
                        break;
                }
                returnString = fileName;
                Response.StatusCode = 200;

            } catch (Exception ex) {
                if (string.IsNullOrEmpty(returnString)) {
                    //Response.StatusCode = 500;
                    returnString = "Error: " + ex.Message;
                }
            } finally {
                // Clean up
                if (_IH != null) _IH.Dispose();
                Response.Write(returnString);
            }
        }
    }

}
