using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;

namespace PMSAuthorization
{
    public interface IAccess
    {
        IList<PMSAccess> GetAll();
        PMSAccess GetAccessByName(string accessName);
        IList<PMSAccess> GetByRole(PMSRole role);
        bool IsAccessExsitPMS(PMSAccess access);

        int Add(PMSAccess access);
        int Upate(PMSAccess access);
        int Disable(PMSAccess access);
    }
}
