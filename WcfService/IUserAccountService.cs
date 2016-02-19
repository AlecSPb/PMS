using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserAccountService”。
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAppUser(AppUser user);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAppUser(AppUser user);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAppRole(AppRole role);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAppRole(AppRole role);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAppAccess(AppAccess access);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAppAccess(AppAccess access);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteAppAccess(AppAccess access);




    }
}
