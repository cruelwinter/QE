using QE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Services
{
    public class ClientSessionService
    {
        public static ClientSession Login(QE_USER user)
        {
            try {
                //user.PASSWORD = AccountService.aesEncryptBase64(user.PASSWORD, "QEKey");
                QE_USER loginedUser = KennyORM.GetDBSource("QE_USER", "select * from QE_USER where USER_ID='" + user.USER_ID + "' and PASSWORD='" + user.PASSWORD + "' and active=1").Cast<QE_USER>().First();
                if (loginedUser.ID != 0)
                {
                    if (loginedUser.FIRST_LOGIN.Date.Year == 1900 || loginedUser.FIRST_LOGIN.Date == null)
                        loginedUser.FIRST_LOGIN = DateTime.Now;
                    else
                        loginedUser.LAST_LOGIN = DateTime.Now;

                    KennyORM.UpdateRecord(loginedUser);
                }
                ClientSession session = new ClientSession();
                session.loginedUser = loginedUser;
                HttpContext.Current.Session["ClientSession"] = session;
                return session;
            }
            catch { return new ClientSession(); }
        }

        // Gets the current session.
        public static ClientSession GetSession
        {
            get
            {
                ClientSession session = (ClientSession)HttpContext.Current.Session["ClientSession"];

                if (session == null)
                    session = new ClientSession();

                HttpContext.Current.Session["ClientSession"] = session;
                return session;
            }
        }

        public static bool IsLogined
        {
            get
            {
                return (GetSession.loginedUser.ID != Constant.DEF_INT);
            }
        }

        public static void Logout()
        {
            HttpContext.Current.Session["ClientSession"] = null;
        }
    }
}