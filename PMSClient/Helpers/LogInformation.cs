using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserSimpleService;

namespace PMSClient.Helper
{
    /// <summary>
    /// 存储和处理客户端所需要的用户信息
    /// </summary>
    public class LogInformation
    {
        public DcUser CurrentUser { get; set; }
        public DcUserRole CurrentUserRole { get; set; }
        public List<DcUserAccess> CurrentAccesses { get; set; }
        public LogInformation()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses = new List<DcUserAccess>();
        }
        /// <summary>
        /// 注销
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses.Clear();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogIn(string username, string password)
        {
            try
            {
                using (var service = new UserSimpleServiceClient())
                {
                    CurrentUser = service.GetUser(username, password);
                    if (CurrentUser != null)
                    {
                        CurrentUserRole = service.GetRole(CurrentUser.RoleID);

                        var accesses = service.GetAccesses(CurrentUser.RoleID);
                        CurrentAccesses.Clear();
                        accesses.ToList().ForEach(i => CurrentAccesses.Add(i));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsAuthorized(string accessCode)
        {
            if (CurrentUser == null)
            {
                return false;
            }
            return CurrentAccesses.Where(i => i.AccessCode == accessCode).Count() > 0;
        }

        public bool IsOKInGroup(string[] groups)
        {
            if (groups.Length == 0) return true;
            if (CurrentUserRole!=null)
            {
                foreach (var item in groups)
                {
                    if (CurrentUserRole.GroupName.Contains(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsLogIn()
        {
            return CurrentUser != null;
        }
    }
}
