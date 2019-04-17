using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class MERITS
    {
        [Key]
        public int ID { get; set; }
        public int MERIT { get; set; }
        public int STUDENT { get; set; }
        public int GRANTER { get; set; }
        public int TERM { get; set; }
        public string REMARK { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public MERITS()
        {
            ID = Constant.DEF_INT;
            MERIT = Constant.DEF_INT;
            STUDENT = Constant.DEF_INT;
            GRANTER = Constant.DEF_INT;
            TERM = Constant.DEF_INT;
            REMARK = Constant.DEF_STRING;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
            
        }
    }
}
