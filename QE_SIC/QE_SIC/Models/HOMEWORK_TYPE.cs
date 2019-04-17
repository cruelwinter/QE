using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class HOMEWORK_TYPE
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public int DURATION_DAY { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public HOMEWORK_TYPE()
        {
            ID = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            DURATION_DAY = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

    }
}