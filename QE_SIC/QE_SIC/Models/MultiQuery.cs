using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class MutliQuery
    {
        [Required]
        public string field1 { get; set; }

        public string function { get; set; }

        [Required]
        public string field2 { get; set; }

        public MutliQuery()
        {
            field1 = Constant.DEF_STRING;
            function = Constant.DEF_STRING;
            field2 = Constant.DEF_STRING;
        }

        public MutliQuery(string field1, string function, string field2)
        {
            this.field1 = field1;
            this.function = function;
            this.field2 = field2;
        }

    }
}