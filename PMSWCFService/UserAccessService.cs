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
    public class UserAccessService : IUserAccessService
    {
        public int AddAccess(DcAccess model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcAccess, PMSAccess>());
                var mapper = config.CreateMapper();
                var access = mapper.Map<PMSAccess>(model);
                dc.Accesses.Add(access);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int AddRole(DcRole model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRole, PMSRole>());
                var mapper = config.CreateMapper();
                var role = mapper.Map<PMSRole>(model);
                dc.Roles.Add(role);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int AddUser(DcUser model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcAccess, PMSAccess>());
                var mapper = config.CreateMapper();
                var access = mapper.Map<PMSAccess>(model);
                dc.Accesses.Add(access);
                result = dc.SaveChanges();
                return result;
            }
        }

        public bool CheckAccess(DcUser model, string AccessCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogIn(DcUser model)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public int DeleteAccess(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public int DeleteRole(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public int DeleteUser(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public List<DcAccess> GetAccessesByRoleId(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public List<DcAccess> GetAllAccesses()
        {
            throw new NotImplementedException();
        }

        public List<DcRole> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<DcUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public DcRole GetRoleByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAccess(DcAccess model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public int UpdateRole(DcRole model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public int UpdateUser(DcUser model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }
    }
}