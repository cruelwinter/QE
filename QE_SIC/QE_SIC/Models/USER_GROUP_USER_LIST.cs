using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class USER_GROUP_USER_LIST
    {
        [Key]
        public int ID { get; set; }
        public int QE_USER { get; set; }
        public int USER_GROUP { get; set; }

        public USER_GROUP_USER_LIST()
        {
            ID = Constant.DEF_INT;
            QE_USER = Constant.DEF_INT;
            USER_GROUP = Constant.DEF_INT;
        }
    }
}