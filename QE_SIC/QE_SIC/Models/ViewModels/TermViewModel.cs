using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class TermViewModel
    {
        public TERM previous { get; set; }
        public TERM current { get; set; }
        public TERM next { get; set; }

        public TermViewModel()
        {
            previous = new TERM();
            current = new TERM();
            next = new TERM();
        }
        public TermViewModel(TERM previous, TERM current, TERM next)
        {
            this.previous = previous;
            this.current = current;
            this.next = next;
        }
    }

}