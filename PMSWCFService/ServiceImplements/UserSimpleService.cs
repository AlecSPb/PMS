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
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public DcUserRole GetRole(Guid roleId)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var role = dc.Roles.Where(i => i.ID == roleId).FirstOrDefault();
                    Mapper.Initialize(cfg => cfg.CreateMap<UserRole, DcUserRole>());
                    return Mapper.Map<DcUserRole>(role);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public DcUser GetUser(string username, string password)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }
    }
}