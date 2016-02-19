using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserAccountService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserAccountService.svc 或 UserAccountService.svc.cs，然后开始调试。
    public class UserAccountService : IUserAccountService
    {
        public bool AddAppAccess(AppAccess access)
        {
            throw new NotImplementedException();
        }

        public bool AddAppRole(AppRole role)
        {
            throw new NotImplementedException();
        }

        public bool AddAppUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppAccess(AppAccess access)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppRole(AppRole role)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public List<AppAccess> GetAllAppAccesses()
        {
            throw new NotImplementedException();
        }

        public List<AppRole> GetAllAppRoles()
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetAllAppUsers()
        {
            throw new NotImplementedException();
        }

        public List<AppAccess> GetAppAccessByRoleId(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public AppRole GetAppRoleByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public AppUser GetAppUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppAccess(AppAccess access)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppRole(AppRole role)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
