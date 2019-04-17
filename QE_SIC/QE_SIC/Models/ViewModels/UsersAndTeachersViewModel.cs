using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class UsersAndTeachersViewModel
    {
        public List<QE_USER> UserList { get; set; }
        public QE_USER DisplayingUser { get; set; }

        public UsersAndTeachersViewModel()
        {
            UserList = new List<QE_USER>();
            DisplayingUser = new QE_USER();
        }

        public UsersAndTeachersViewModel(List<QE_USER> UserList, QE_USER DisplayingUser)
        {
            this.UserList = UserList;
            this.DisplayingUser = DisplayingUser;
        }
    }
}