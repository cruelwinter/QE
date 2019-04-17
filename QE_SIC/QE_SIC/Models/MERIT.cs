using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class MERIT
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string NAME_CHI { get; set; }
        public int POINTS { get; set; }
        public bool DEMERIT { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public MERIT()
        {
            ID = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            NAME_CHI = Constant.DEF_STRING;
            POINTS = Constant.DEF_INT;
            DEMERIT = Constant.DEF_BOOL;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;

        }

    }
}