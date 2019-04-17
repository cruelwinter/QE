using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class TermService
    {

        public static TERM GetTerm(int id)
        {
            try { return KennyORM.GetDBSource("TERM", "select * from TERM where ID='" + id + "' and active=1").Cast<TERM>().First(); }
            catch { return new TERM(); }
        }

        public static List<TERM> GetTerms()
        {
            try { return KennyORM.GetDBSource("TERM").Cast<TERM>().ToList(); }
            catch { return new List<TERM>(); }
        }

        public static TERM GetTermByName(string name)
        {
            try { return KennyORM.GetDBSource("TERM", "select * from TERM where TERM_NAME='"+name+"' and active=1").Cast<TERM>().First(); }
            catch { return new TERM(); }
        }

        public static TERM GetPreviousTerm()
        {
            try{ return KennyORM.GetDBSource("TERM", "select top 1 * from TERM where TERM_END < CURRENT_TIMESTAMP and active=1").Cast<TERM>().First(); }
            catch{ return new TERM(); }           
        }
        
        public static TERM GetCurrentTerm()
        {
            try{ return KennyORM.GetDBSource("TERM", "select * from TERM where TERM_START <= CURRENT_TIMESTAMP and TERM_END >= CURRENT_TIMESTAMP and active=1").Cast<TERM>().First(); }
            catch{ return new TERM(); }           
        }

        public static TERM GetNextTerm()
        {
            try{ return KennyORM.GetDBSource("TERM", "select top 1 * from TERM where TERM_START > CURRENT_TIMESTAMP and active=1").Cast<TERM>().First(); }
            catch { return new TERM(); }           
        }

        public static SelectList GetSelectList()
        {

            List<SelectListItem> TermItemList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();
            foreach (var term in GetTerms())
            {
                TermItemList.Add(new SelectListItem() { Value = term.ID.ToString(), Text = term.TERM_NAME });
            }
            return new SelectList(TermItemList, "Value", "Text");          
        }
        
    }
}