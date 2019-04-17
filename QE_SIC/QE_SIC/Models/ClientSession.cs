using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class ClientSession
    {
        public QE_USER loginedUser { get; set; }
        public string UserType { get; set; }
        public List<USER_GROUP> UserGroupList { get; set; }
        public List<USER_GROUP_RIGHT> UserRightList { get; set; }

        public ClientSession()
        {
            loginedUser = new QE_USER();
            UserType = Constant.DEF_STRING;
            UserGroupList = new List<USER_GROUP>();
            UserRightList = new List<USER_GROUP_RIGHT>();
        }
    }
}