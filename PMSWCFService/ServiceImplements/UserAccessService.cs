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
        public int AddAccess(DcUserAccess model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcUserAccess, UserAccess>());
                var mapper = config.CreateMapper();
                var access = mapper.Map<UserAccess>(model);
                dc.Accesses.Add(access);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int AddRole(DcUserRole model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcUserRole, UserRole>());
                var mapper = config.CreateMapper();
                var role = mapper.Map<UserRole>(model);
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
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcUserAccess, UserAccess>());
                var mapper = config.CreateMapper();
                var access = mapper.Map<UserAccess>(model);
                dc.Accesses.Add(access);
                result = dc.SaveChanges();
                return result;
            }
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

        public DcUser GetUser(string username, string password)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<User, DcUser>());
                    var user = dc.Users.Where(i => i.UserName == username && i.Password == password).FirstOrDefault();
                    return Mapper.Map<DcUser>(user);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsUserNameExsit(string userName)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var count = dc.Users.Where(i => i.UserName == userName).Count();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<DcUserAccess> GetAccessesByRoleId(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public List<DcUserAccess> GetAllAccesses()
        {
            throw new NotImplementedException();
        }

        public List<DcUserRole> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<DcUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public DcUserRole GetRoleByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAccess(DcUserAccess model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;


                return result;
            }
        }

        public int UpdateRole(DcUserRole model)
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