using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;
namespace PMSClient
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
        public void ResetUserData()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses.Clear();
        }

        public void LoadUserData(string username, string password)
        {
            try
            {
                using (var service = new UserAccessServiceClient())
                {
                    CurrentUser = service.GetUser(username, password);
                    CurrentUserRole = service.GetRole(CurrentUser.RoleID);

                    var accesses = service.GetAccesses(CurrentUser.RoleID);
                    CurrentAccesses.Clear();
                    accesses.ToList().ForEach(i => CurrentAccesses.Add(i));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
