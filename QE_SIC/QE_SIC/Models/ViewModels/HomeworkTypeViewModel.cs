using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class HomeworkTypeViewModel
    {
        public List<HOMEWORK_TYPE> types { get; set; }
        public HOMEWORK_TYPE newType { get; set; }
        public List<int> inactiveList { get; set; }

        public HomeworkTypeViewModel()
        {
            types = new List<HOMEWORK_TYPE>();
            newType = new HOMEWORK_TYPE();
            inactiveList = new List<int>();
        }
    }
}
