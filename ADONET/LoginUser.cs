using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET
{
    class LoginUser
    {
        public int Id { get; private set; }
        public bool IsLogin { get; private set; }

        public LoginUser()
        {
            IsLogin = false;
            Id = -1;
        }

        public void Login(UsersDAL uDal, string login, string password)
        {
            Id = uDal.Login(login, password);
            if (Id >= 0) IsLogin = true;
        }

        public void Logout()
        {
            Id = -1;
            IsLogin = false;
        }
    }
}
