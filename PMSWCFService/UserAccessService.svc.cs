using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService
{
    //public class UserService : IUserService
    //{
    //    public UserService()
    //    {

    //    }

    //    public int Add(UserDc model)
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            int result = 0;
    //            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDc, PMSUser>());
    //            var mapper = config.CreateMapper();
    //            var user = mapper.Map<PMSUser>(model);
    //            dc.Users.Add(user);
    //            result = dc.SaveChanges();
    //            return result;
    //        }
    //    }


    //    public int Delete(Guid id)
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            int result = 0;
    //            var user = dc.Users.Find(id);
    //            if (user != null)
    //            {
    //                user.State = (int)ModelState.Deleted;
    //                result = dc.SaveChanges();
    //            }
    //            return result;
    //        }
    //    }



    //    public void DoSomething()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public UserDc FindById(Guid id)
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSUser, UserDc>());
    //            var mapper = config.CreateMapper();
    //            return mapper.Map<UserDc>(dc.Users.Find(id));
    //        }

    //    }

    //    public List<UserDc> GetAll()
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            var users = dc.Users.ToList();
    //            var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSUser, UserDc>());
    //            var mapper = config.CreateMapper();
    //            var userDcs = new List<UserDc>();
    //            userDcs = mapper.Map<List<PMSUser>, List<UserDc>>(users);
    //            return userDcs;
    //        }

    //    }

    //    public int GetCount()
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            return dc.Users.Count();
    //        }
    //    }

    //    public int Update(UserDc model)
    //    {
    //        using (var dc = new PMSDbContext())
    //        {
    //            int result = 0;
    //            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDc, PMSUser>());
    //            var mapper = config.CreateMapper();
    //            var newUser = mapper.Map<PMSUser>(model);

    //            dc.Entry(newUser).State = System.Data.Entity.EntityState.Modified;
    //            result = dc.SaveChanges();
    //            return result;
    //        }
    //    }
    //}
}
