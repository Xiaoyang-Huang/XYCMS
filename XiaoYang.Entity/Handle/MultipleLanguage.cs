﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoYang.Entity.Handle {
    public class MultipleLanguage : IEntityHandle {
        public void Init(EntityType type) {
            throw new NotImplementedException();
        }

        public Xy.Web.Page.PageAbstract GetEditPageClass() {
            throw new NotImplementedException();
        }

        public Xy.Web.HTMLContainer GetEditPageTemplate(Xy.Web.HTMLContainer container) {
            throw new NotImplementedException();
        }


        public void CreateTable(Xy.Data.DataBase DB) {
            throw new NotImplementedException();
        }

        public void DropTable(Xy.Data.DataBase DB) {
            throw new NotImplementedException();
        }
    }
}