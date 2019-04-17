using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class AccessViewModel
    {
        public List<USER_GROUP> group { get; set; }
        public List<int> inactiveList { get; set; }
        public USER_GROUP newGroup { get; set; }
        public int SelectedGroup { get; set; }
        public List<USER_GROUP_RIGHT> groupRights { get; set; }
        public List<GroupUserView> groupUserlist { get; set; }

        public AccessViewModel()
        {
            group = new List<USER_GROUP>();
            inactiveList = new List<int>();
            newGroup = new USER_GROUP();
            SelectedGroup = Constant.DEF_INT;
            groupRights = new List<USER_GROUP_RIGHT>();
            groupUserlist = new List<GroupUserView>();
        }
    }

    public class GroupUserView : USER_GROUP_USER_LIST
    {
        public string user_name {get;set;}
        public bool contain { get; set; }
        public GroupUserView()
        {
            ID = Constant.DEF_INT;
            QE_USER = Constant.DEF_INT;
            user_name = Constant.DEF_STRING;
            USER_GROUP = Constant.DEF_INT;
            contain = Constant.DEF_BOOL;
        }


        public GroupUserView(QE_USER user)
        {
            ID = Constant.DEF_INT;
            QE_USER = user.ID;
            user_name = user.USER_NAME;
            USER_GROUP = Constant.DEF_INT;
            contain = Constant.DEF_BOOL;
        }
    }
}