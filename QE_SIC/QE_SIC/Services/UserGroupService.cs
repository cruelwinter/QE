using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class UserGroupService
    {
        public static List<USER_GROUP> GetGroup()
        {
            try { return WillORM.GetDBSource("USER_GROUP").Cast<USER_GROUP>().ToList(); }
            catch { return null; }
        }

        public static USER_GROUP GetGroup(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID", id.ToString()}
            };

            try { return WillORM.GetDBSource("USER_GROUP", WillORM.QueryBuilder(null, "v", DS, null)).Cast<USER_GROUP>().FirstOrDefault(); }
            catch { return null; }
        }

        public static int AddRecord(object newRecord)
        {
            try { return WillORM.InsertRecord(newRecord); }
            catch { return 0; }
        }

        public static bool RemoveGroup(int group_id)
        {
            try { return WillORM.DeleteRecord("USER_GROUP", group_id); }
            catch { return false; }
        }

        public static bool UpdateGroup(USER_GROUP group)
        {
            group.MODIFY_BY = ClientSessionService.GetSession.loginedUser.ID;
            group.MODIFY_DATE = DateTime.Now;

            try { return WillORM.UpdateRecord(group); }
            catch { return false; }
        }

        public static List<GROUP_RIGHT> GetRights()
        {
            try { return WillORM.GetDBSource("GROUP_RIGHT").Cast<GROUP_RIGHT>().ToList(); }
            catch { return null; }
        }


        public static List<USER_GROUP_USER_LIST> GetGroupUsers()
        {
            try { return WillORM.GetDBSource("USER_GROUP_USER_LIST").Cast<USER_GROUP_USER_LIST>().ToList(); }
            catch { return null; }
        }

        public static List<USER_GROUP_RIGHT> GetGroupRights()
        {
            try { return WillORM.GetDBSource("USER_GROUP_RIGHT").Cast<USER_GROUP_RIGHT>().ToList(); }
            catch { return null; }
        }

        public static bool RemoveGroupRight(int group_id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>() {
                {"USER_GROUP", group_id.ToString() }
            };

            try { return WillORM.DeleteRecord("USER_GROUP_RIGHT", DS); }
            catch { return false; }
        }

        public static bool RemoveGroupUser(int list_id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>() {
                {"ID", list_id.ToString() }
            };

            try { return WillORM.DeleteRecord("USER_GROUP_USER_LIST", DS); }
            catch { return false; }
        }

        public static bool UpdateRight(USER_GROUP_RIGHT right)
        {
            right.MODIFY_BY = ClientSessionService.GetSession.loginedUser.ID;
            right.MODIFY_DATE = DateTime.Now;

            try { return WillORM.UpdateRecord(right); }
            catch { return false; }
        }

    }
}