using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class GROUP_RIGHT
    {
        [Key]
        public int ID { get; set; }
        public string RIGHT_NAME { get; set; }

        public GROUP_RIGHT()
        {
            ID = Constant.DEF_INT;
            RIGHT_NAME = Constant.DEF_STRING;
        }
    }
}