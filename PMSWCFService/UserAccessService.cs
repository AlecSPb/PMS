using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public class UserAccessService : IUserAccessService
    {
        public int AddAccess(DcAccess access)
        {
            throw new NotImplementedException();
        }

        public int AddRole(DcRole role)
        {
            throw new NotImplementedException();
        }

        public int AddUser(DcUser user)
        {
            throw new NotImplementedException();
        }

        public bool CheckAccess(DcUser user, string AccessCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogIn(DcUser user)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public int DeleteAccess(DcAccess access)
        {
            throw new NotImplementedException();
        }

        public int DeleteRole(DcRole role)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(DcUser user)
        {
            throw new NotImplementedException();
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

        public int UpdateAccess(DcAccess access)
        {
            throw new NotImplementedException();
        }

        public int UpdateRole(DcRole role)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(DcUser user)
        {
            throw new NotImplementedException();
        }
    }
}