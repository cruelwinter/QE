using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class USER_GROUP_RIGHT
    {
        [Key]
        public int ID { get; set; }
        public int RIGHT_ID { get; set; }
        public string RIGHT_NAME { get; set; }
        public bool R { get; set; }
        public bool W { get; set; }
        public int Right_1_level { get; set; }
        public int Right_2_level { get; set; }
        public int Right_3_level { get; set; }
        public int Right_4_level { get; set; }
        public int USER_GROUP { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }

        public USER_GROUP_RIGHT()
        {
            ID = Constant.DEF_INT;
            RIGHT_ID = Constant.DEF_INT;
            RIGHT_NAME = Constant.DEF_STRING;
            R = Constant.DEF_BOOL;
            W = Constant.DEF_BOOL;
            Right_1_level = Constant.DEF_INT;
            Right_2_level = Constant.DEF_INT;
            Right_3_level = Constant.DEF_INT;
            Right_4_level = Constant.DEF_INT;
            USER_GROUP = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
        }
    }
}
