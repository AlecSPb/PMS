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
        User GetUserByUserName(string userName);
        IList<User> GetAll();

        Role GetRole(User user);
        int Add(User user);
        int Update(User user);
        int Disable(User user);
    }
}
