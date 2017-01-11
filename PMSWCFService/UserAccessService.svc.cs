using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PMSWCFService.Models;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService
{
    public class UserAccessService : IUserAccessService, IDisposable
    {
        private PMSDbContext dc;
        public UserAccessService()
        {
            dc = new PMSDbContext();
        }
        public void Dispose()
        {
            dc.Dispose();
        }

        public int Add(UserDc model)
        {
            int result = 0;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDc, PMSDAL.PMSUser>());
            var mapper = config.CreateMapper();
            var user = mapper.Map<PMSDAL.PMSUser>(model);
            dc.Users.Add(user);
            result = dc.SaveChanges();
            return result;
        }


        public int Delete(Guid id)
        {
            int result = 0;
            var user = dc.Users.Find(id);
            if (user != null)
            {
                user.State = 0;
                dc.Entry<PMSUser>(user).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }



        public void DoSomething()
        {
            throw new NotImplementedException();
        }

        public UserDc FindById(Guid id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSUser,UserDc>());
            var mapper = config.CreateMapper();
            return mapper.Map<UserDc>(dc.Users.Find(id));
        }

        public List<UserDc> GetAll()
        {
            var users = dc.Users.ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSUser, UserDc>());
            var mapper = config.CreateMapper();
            var userDcs = new List<UserDc>();
            userDcs = mapper.Map<List<PMSUser>,List<UserDc>>(users);
            return userDcs;
        }

        public int GetCount()
        {
            return dc.Users.Count();
        }

        public int Update(UserDc model)
        {
            int result = 0;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDc, PMSUser>());
            var mapper = config.CreateMapper();
            var user = dc.Users.Find(model.ID);
            user = mapper.Map<PMSUser>(model);
            dc.Entry<PMSUser>(user).State = System.Data.Entity.EntityState.Modified;
            result = dc.SaveChanges();
            return result;
        }
    }
}
