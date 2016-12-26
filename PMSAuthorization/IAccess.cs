using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAuthorization
{
    public interface IAccess
    {
        IList<Access> GetAll();
        Access GetAccessByName(string accessName);
        IList<Access> GetByRole(Role role);
        bool IsAccessExsit(Access access);

        int Add(Access access);
        int Upate(Access access);
        int Disable(Access access);
    }
}
