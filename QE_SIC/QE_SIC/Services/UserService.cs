using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class UserService
    {
        public static List<QE_USER> GetUsers()
        {
            try { return KennyORM.GetDBSource("QE_USER").Cast<QE_USER>().ToList(); }
            catch { return new List<QE_USER>(); }
        }

        public static QE_USER GetUser(int id)
        {
            try { return KennyORM.GetDBSource("QE_USER", "select * from QE_USER where ID=" + id + " and active=1").Cast<QE_USER>().First(); }
            catch { return new QE_USER(); }
        }

        public static List<QE_USER> GetUsersList()
        {
            try { return KennyORM.GetDBSource("QE_USER", "select ID,USER_ID,USER_NAME,USER_NAME_CHI from QE_USER where active=1").Cast<QE_USER>().ToList(); }
            catch { return new List<QE_USER>(); }
        }

        public static List<QE_USER> GetAllUser()
        {
            try { return WillORM.GetDBSource("QE_USER").Cast<QE_USER>().ToList(); }
            catch { return new List<QE_USER>(); }
        }

        public static SelectList GetSelectList()
        {
            SelectListItem selectedValue = new SelectListItem();
            List<SelectListItem> UserList = new List<SelectListItem>();
            UserList.Add(new SelectListItem() { Value = "0", Text = "" });
            foreach (var user in getActiveUsers())
            {
                UserList.Add(new SelectListItem() { Value = user.ID.ToString(), Text = user.USER_NAME });
            }
            return new SelectList(UserList, "Value", "Text");
        }

        public static List<QE_USER> getActiveUsers()
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("QE_USER", WillORM.QueryBuilder(null, "QE_USER", Ds, null)).Cast<QE_USER>().ToList(); }
            catch { return new List<QE_USER>(); }
        }

        public static IEnumerable<SelectListItem> getTeacherSelectList()
        {
            List<SelectListItem> TermItemList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();
            foreach (var teacher in getActiveUsers())
            {
                TermItemList.Add(new SelectListItem() { Value = teacher.ID.ToString(), Text = teacher.USER_NAME });
            }
            return TermItemList;
        }
    }
}
