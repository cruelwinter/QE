using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class QE_CLASS
    {
        [Key]
        public int ID { get; set; }
        public int TERM { get; set; }
        public int FORM { get; set; }
        public string NAME { get; set; }         
        public int TEACHER { get; set; }         
        public int TEACHER_2 { get; set; }        
        public string CLASSROOM { get; set; }      
        public int ADD_BY { get; set; }        
        public DateTime ADD_DATE { get; set; }    
        public int MODIFY_BY { get; set; }       
        public DateTime MODIFY_DATE { get; set; }   
        public bool ACTIVE { get; set; }
        
        public QE_CLASS()
        {
            ID = Constant.DEF_INT;
            TERM = Constant.DEF_INT;
            FORM = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            TEACHER = Constant.DEF_INT;
            TEACHER_2 = Constant.DEF_INT;
            CLASSROOM = Constant.DEF_STRING;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }
    }
}                                                                                                     