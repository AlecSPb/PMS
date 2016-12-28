using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;

namespace PMSAuthorization
{
   public  interface IRole
    {
        bool IsRoleExsit(PMSRole role);
        IList<PMSRole> GetAll();
        PMSRole GetRoleByName(string roleName);
        PMSRole GetRoleByUser(PMSUser user);

        int Add(PMSRole role);
        int Update(PMSRole role);
        int Disable(PMSRole role);

    }
}
