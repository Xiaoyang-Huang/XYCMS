using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.User {
    public partial class UserExtra : Xy.Data.IDataModel {
        static void RegisterEvents() { }

        private static void before_Add(ref long inUserID, ref string inHeadImage, ref short inSex, ref DateTime inBirthday, ref string inHometown, ref string inDescription, ref string inJob, ref string inWeibo, ref string inQQ) {
            if (string.IsNullOrEmpty(inHeadImage)) inHeadImage = "nopic.png";
            if (string.IsNullOrEmpty(inHometown)) inHometown = "未知";
            if (string.IsNullOrEmpty(inDescription)) inDescription = "啥也没写...";
            if (string.IsNullOrEmpty(inJob)) inJob = "未知";
            if (string.IsNullOrEmpty(inWeibo)) inWeibo = "未知";
            if (string.IsNullOrEmpty(inQQ)) inQQ = "未知";
        }

        private static void before_Edit(ref long inUserID, ref string inHeadImage, ref short inSex, ref DateTime inBirthday, ref string inHometown, ref string inDescription, ref string inJob, ref string inWeibo, ref string inQQ) { 
            if (string.IsNullOrEmpty(inHeadImage)) inHeadImage = "nopic.png";
            if (string.IsNullOrEmpty(inHometown)) inHometown = "未知";
            if (string.IsNullOrEmpty(inDescription)) inDescription = "啥也没写...";
            if (string.IsNullOrEmpty(inJob)) inJob = "未知";
            if (string.IsNullOrEmpty(inWeibo)) inWeibo = "未知";
            if (string.IsNullOrEmpty(inQQ)) inQQ = "未知";
        }

    }
}
