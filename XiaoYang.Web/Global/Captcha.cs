using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace XiaoYang.Web.Global {
   public class Captcha: Xy.Web.Page.PageAbstract {
        // 验证码长度
        private int codeLen = 4;
        // 图片清晰度
        private int fineness = 90;
        // 图片宽度
        private int imgWidth = 55;
        // 图片高度
        private int imgHeight = 24;
        // 字体家族名称
        private string fontFamily = "Times New Roman";
        // 字体大小
        private int fontSize = 14;
        // 字体样式
        private int fontStyle = 2;
        // 绘制起始坐标 X
        private int posX = 0;
        // 绘制起始坐标 Y
        private int posY = 0;

        private Bitmap bitmap = null;

        public override void onGetRequest() {

            string validateCode = CreateValidateCode();

            // 生成BITMAP图像
            bitmap = new Bitmap(imgWidth, imgHeight);

            // 给图像设置干扰
            DisturbBitmap(bitmap);

            // 绘制验证码图像
            DrawValidateCode(bitmap, validateCode);

            // 保存验证码图像，等待输出
            //bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            if (!string.IsNullOrEmpty(Request.QueryString["path"])) {
                Session[Request.QueryString["path"] + "-SecureCode"] = validateCode;
            } else {
                Session["SecureCode"] = validateCode;
            }
            using (System.IO.MemoryStream temp = new System.IO.MemoryStream()) {
                bitmap.Save(temp, System.Drawing.Imaging.ImageFormat.Gif);
                Response.Write(temp);
                End();
            }
        }


        private string CreateValidateCode() {
            string validateCode = "";
            // 随机数对象
            Random random = new Random();

            for (int i = 0; i < codeLen; i++) {
                // 26: a - z
                int n = random.Next(26);


                // 将数字转换成大写字母
                validateCode += (char)(n + 65);
            }

            return validateCode;
        }

        //------------------------------------------------------------
        // 为图片设置干扰点
        //------------------------------------------------------------
        private void DisturbBitmap(Bitmap bitmap) {
            // 通过随机数生成
            Random random = new Random();

            for (int i = 0; i < bitmap.Width; i++) {
                for (int j = 0; j < bitmap.Height; j++) {
                    if (random.Next(100) <= this.fineness)
                        bitmap.SetPixel(i, j, Color.White);
                }
            }
        }

        //------------------------------------------------------------
        // 绘制验证码图片
        //------------------------------------------------------------
        private void DrawValidateCode(Bitmap bitmap, string validateCode) {
            // 获取绘制器对象
            Graphics g = Graphics.FromImage(bitmap);

            // 设置绘制字体
            Font font = new Font(fontFamily, fontSize, GetFontStyle());

            // 绘制验证码图像
            g.DrawString(validateCode, font, Brushes.Black, posX, posY);
        }

        //------------------------------------------------------------
        // 换算验证码字体样式：1 粗体 2 斜体 3 粗斜体，默认为普通字体
        //------------------------------------------------------------
        private FontStyle GetFontStyle() {
            if (fontStyle == 1)
                return FontStyle.Bold;
            else if (fontStyle == 2)
                return FontStyle.Italic;
            else if (fontStyle == 3)
                return FontStyle.Bold | FontStyle.Italic;
            else
                return FontStyle.Regular;
        }
    }
}
