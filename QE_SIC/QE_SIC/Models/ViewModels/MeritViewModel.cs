using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class MeritViewModel
    {
        public List<MERIT> merits { get; set; }
        public MERIT newMerit { get; set; }
        public List<int> removeList { get; set; }

        public MeritViewModel()
        {
            merits = new List<MERIT>();
            newMerit = new MERIT();
            removeList = new List<int>();
        }
    }
}
