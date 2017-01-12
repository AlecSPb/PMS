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
        int AddUser(DcUser model);
        [OperationContract]
        int AddRole(DcRole model);
        [OperationContract]
        int AddAccess(DcAccess model);

        [OperationContract]
        int UpdateUser(DcUser model);
        [OperationContract]
        int UpdateRole(DcRole model);
        [OperationContract]
        int UpdateAccess(DcAccess model);

        [OperationContract]
        int DeleteUser(Guid id);
        [OperationContract]
        int DeleteRole(Guid id);
        [OperationContract]
        int DeleteAccess(Guid id);

        [OperationContract]
        bool CheckUserName(string userName);
        [OperationContract]
        bool CheckLogIn(DcUser model);
        [OperationContract]
        bool CheckAccess(DcUser model, string AccessCode);

    }
}