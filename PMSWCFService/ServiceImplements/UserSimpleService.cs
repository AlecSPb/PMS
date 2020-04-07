using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public class UserSimpleService : IUserSimpleService
    {
        public List<DcUserAccess> GetAccesses(Guid roleId)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var role = dc.Roles.Where(i => i.ID == roleId).FirstOrDefault();
                    if (role != null)
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap<UserAccess, DcUserAccess>());
                        return Mapper.Map<List<UserAccess>, List<DcUserAccess>>(role.UserAccesses);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public string GetAccessGrantByControl(string controlName)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var item = dc.AccessGrants.Where(i => i.ControlName == controlName).FirstOrDefault();
                    if (item != null)
                    {
                        return item.RoleGroupString;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcUserRole GetRole(Guid roleId)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var role = dc.Roles.Where(i => i.ID == roleId).FirstOrDefault();
                    Mapper.Initialize(cfg => cfg.CreateMap<UserRole, DcUserRole>());
                    return Mapper.Map<DcUserRole>(role);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcUser GetUser(string username, string password)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<User, DcUser>());
                    var user = dc.Users.Where(i => i.UserName == username
                    && i.Password == password
                    && i.State == PMSCommon.UserState.雇佣.ToString()).FirstOrDefault();
                    return Mapper.Map<DcUser>(user);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}