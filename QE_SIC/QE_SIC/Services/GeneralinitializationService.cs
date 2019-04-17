using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using QE.Models;
using QE.Models.ViewModels;
using System.Web.Mvc;

namespace QE.Services
{
    public class GeneralinitializationService
    {
        private static IDBservice DBS = new DBservice();

        private static SelectList atermSelectList = TermService.GetSelectList();
        private static int auserID = ClientSessionService.GetSession.loginedUser.ID;
        private static TERM acurrentTerm = TermService.GetCurrentTerm();
        private static Dictionary<int, TERM> atermlist = transTerm(DBS.findActiveRecords<TERM>());
        private static IEnumerable<SelectListItem> ateacherSelectList = UserService.getTeacherSelectList();
        private static SelectList auserSelectList = UserService.GetSelectList();
        private static List<QE_USER> aAllUser = UserService.GetAllUser();

        private static Dictionary<int, TERM> transTerm(List<TERM> terms)
        {
            Dictionary<int, TERM> termlist = new Dictionary<int, TERM>();
            foreach (var t in terms)
            {
                termlist.Add(t.ID, t);
            }
            return termlist;
        }

        public class COMMONPARM
        {
            public SelectList termSelectList { get;  set; }
            public SelectList userSelectList { get; set; }
            public int userID { get; private set; }
            public TERM currentTerm { get; private set; }
            public Dictionary<int, TERM> termlist { get;  set; }
            public IEnumerable<SelectListItem> teacherSelectList { get;  set; }
            public  List<QE_USER> AllUser { get; set; }

            public COMMONPARM()
            {
                termSelectList = atermSelectList;
                userID = auserID;
                currentTerm = acurrentTerm;
                termlist = atermlist;
                teacherSelectList = ateacherSelectList;
                userSelectList = auserSelectList;
                AllUser = aAllUser;
            }
        }
        
        public int getNewID()
        {
            return ((int)((DateTime.Now.Ticks / 10) % 100000000))* -1;
        }
    }
}
