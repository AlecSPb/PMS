using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface IUserAccountService
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AppUser> GetAllAppUsers();
        /// <summary>
        /// 按照id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        AppUser GetAppUserById(Guid id);
        [OperationContract]
        bool AddAppUser(AppUser user);
        [OperationContract]
        bool UpdateAppUser(AppUser user);
        [OperationContract]
        bool DeleteAppUser(AppUser user);

        /// <summary>
        /// 返回所有角色
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AppRole> GetAllAppRoles();
        /// <summary>
        /// 按照userid查找角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        AppRole GetAppRoleByUserId(Guid userId);
        [OperationContract]
        bool AddAppRole(AppRole role);
        [OperationContract]
        bool UpdateAppRole(AppRole role);
        [OperationContract]
        bool DeleteAppRole(AppRole role);

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AppAccess> GetAllAppAccesses();
        /// <summary>
        /// 获得roleid的所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract]
        List<AppAccess> GetAppAccessByRoleId(Guid roleId);
        [OperationContract]
        bool AddAppAccess(AppAccess access);
        [OperationContract]
        bool UpdateAppAccess(AppAccess access);
        [OperationContract]
        bool DeleteAppAccess(AppAccess access);




    }
}
