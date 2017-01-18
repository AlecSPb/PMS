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
        List<DcUserRole> GetAllRoles();
        [OperationContract]
        List<DcUserAccess> GetAllAccesses();

        [OperationContract]
        DcUserRole GetRoleByUserId(Guid userId);
        [OperationContract]
        List<DcUserAccess> GetAccessesByRoleId(Guid roleId);

        [OperationContract]
        int AddUser(DcUser model);
        [OperationContract]
        int AddRole(DcUserRole model);
        [OperationContract]
        int AddAccess(DcUserAccess model);

        [OperationContract]
        int UpdateUser(DcUser model);
        [OperationContract]
        int UpdateRole(DcUserRole model);
        [OperationContract]
        int UpdateAccess(DcUserAccess model);

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