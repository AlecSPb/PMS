using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAuthorization
{
    public interface IUser
    {
        bool IsExsit(User user);
        Role GetRole(User user);

        User GetUserByUserName(string userName);
        IList<User> GetAllUsers();
        int AddUser(User user);
        int UpdateUser(User user);
        int DisableUser(User user);
    }
}
