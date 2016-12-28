using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;

namespace PMSAuthorization
{
    public interface IUser
    {
        bool IsExsit(PMSUser user);
        PMSUser GetUserByUserName(string userName);
        IList<PMSUser> GetAll();

        PMSRole GetRole(PMSUser user);
        int Add(PMSUser user);
        int Update(PMSUser user);
        int Disable(PMSUser user);
    }
}
