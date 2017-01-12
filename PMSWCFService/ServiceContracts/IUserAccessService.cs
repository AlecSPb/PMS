using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IUserAccessService
    {
        [OperationContract]
        List<DcUser> GetAllUsers();
        [OperationContract]
        List<DcRole> GetAllRoles();
        [OperationContract]
        List<DcAccess> GetAllAccesses();

        [OperationContract]
        DcRole GetRoleByUserId(Guid userId);
        [OperationContract]
        List<DcAccess> GetAccessesByRoleId(Guid roleId);

        [OperationContract]
        int AddUser(DcUser user);
        [OperationContract]
        int AddRole(DcRole role);
        [OperationContract]
        int AddAccess(DcAccess access);

        [OperationContract]
        int UpdateUser(DcUser user);
        [OperationContract]
        int UpdateRole(DcRole role);
        [OperationContract]
        int UpdateAccess(DcAccess access);

        [OperationContract]
        int DeleteUser(DcUser user);
        [OperationContract]
        int DeleteRole(DcRole role);
        [OperationContract]
        int DeleteAccess(DcAccess access);

        [OperationContract]
        bool CheckUserName(string userName);
        [OperationContract]
        bool CheckLogIn(DcUser user);
        [OperationContract]
        bool CheckAccess(DcUser user, string AccessCode);

    }
}