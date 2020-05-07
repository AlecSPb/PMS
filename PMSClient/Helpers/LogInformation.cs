using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserSimpleService;
using Newtonsoft.Json;
using System.IO;

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

        public List<DcAccessGrant> AccessGrants { get; set; }
        public LogInformation()
        {
            CurrentUser = null;
            CurrentUserRole = null;
            CurrentAccesses = new List<DcUserAccess>();
            AccessGrants = new List<DcAccessGrant>();
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

        /// <summary>
        /// 判断当前用户是否在用户组里
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public bool IsInGroup(string[] groups)
        {
            if (groups == null) return false;
            if (groups.Length == 0) return false;
            if (CurrentUserRole != null)
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
        /// <summary>
        /// 判断当前用户是否在用户组里-从数据库里
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public bool IsInGroup(string controlName)
        {
            if (string.IsNullOrEmpty(controlName)) return false;
            return IsInGroup(GetAccessArrayFromLocal(controlName));
        }
        /// <summary>
        /// 从数据库里根据控制名来查询组字符串
        /// </summary>
        /// <param name="controlname"></param>
        /// <returns></returns>
        public string[] GetRoleGroupByControlName(string controlname)
        {
            try
            {
                using (var userRole = new UserSimpleServiceClient())
                {
                    string roleStr = userRole.GetAccessGrantByControl(controlname);
                    return GetRoles(roleStr);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// 将数据库里+连接的用户组名转换为数组
        /// </summary>
        /// <param name="roleStr"></param>
        /// <returns></returns>
        private string[] GetRoles(string roleStr)
        {
            if (string.IsNullOrEmpty(roleStr))
            {
                return null;
            }
            string[] roles = roleStr.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }

        public bool IsLogIn()
        {
            return CurrentUser != null;
        }

        /// <summary>
        /// 从数据库里获取对应权限组
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string[] GetAccessArrayFromDB(string controlName)
        {
            try
            {
                using (var service = new UserSimpleServiceClient())
                {
                    var groupstring = service.GetAccessGrantByControl(controlName);
                    string[] groups = groupstring.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                    return groups;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 从本地权限表获取权限组
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string[] GetAccessArrayFromLocal(string controlName)
        {
            try
            {
                var groupstring = AccessGrants.Where(i => i.ControlName == controlName).Select(i=>i.RoleGroupString).FirstOrDefault();
                string[] groups = groupstring.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                return groups;

            }
            catch (Exception)
            {
                return null;
            }

        }


        /// <summary>
        /// 下载权限表到本地
        /// </summary>
        public void DownloadAccessSheet()
        {
            try
            {
                string jsonStr = "";
                using (var s = new UserSimpleServiceClient())
                {
                    var result = s.GetAllAccessGrant();
                    jsonStr = JsonConvert.SerializeObject(result);
                }
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    string filename = XSHelper.XS.File.GetCurrentFolderPath("al.json");
                    XSHelper.XS.File.SaveText(filename, jsonStr);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        /// <summary>
        /// 读取本地权限表
        /// </summary>
        public void ReadAccessSheetFromLocal()
        {
            try
            {
                string filename = XSHelper.XS.File.GetCurrentFolderPath("al.json");
                string jsonStr = XSHelper.XS.File.ReadText(filename);
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    var models = JsonConvert.DeserializeObject<List<DcAccessGrant>>(jsonStr);
                    AccessGrants.Clear();
                    foreach (var item in models)
                    {
                        AccessGrants.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}
