using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace PMSWCFService.ServiceContracts
{
    /// <summary>
    /// 简单只读的用户信息服务
    /// 用于普通类型客户端中
    /// </summary>
    [ServiceContract]
    public interface IUserSimpleService
    {
        [OperationContract]
        DcUser GetUser(string username, string password);
        [OperationContract]
        DcUserRole GetRole(Guid roleId);
        [OperationContract]
        List<DcUserAccess> GetAccesses(Guid roleId);
    }
}