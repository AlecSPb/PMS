using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAuthorization
{
   public  interface IRole
    {
        bool IsRoleExsit(Role role);
        IList<Role> GetAll();
        Role GetRoleByName(string roleName);
        Role GetRoleByUser(User user);

        int Add(Role role);
        int Update(Role role);
        int Disable(Role role);

    }
}
