using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public partial class PMSService : IFeedBackService
    {
        public int AddFeedBack(DcFeedBack model, string uid)
        {
            try
            {
                using (var dc=new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcFeedBack, FeedBack>());
                    var entity = Mapper.Map<FeedBack>(model);
                    dc.FeedBacks.Add(entity);
                    return dc.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteFeedBack(Guid id, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.FeedBacks.Find(id);
                    dc.FeedBacks.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcFeedBack> GetFeedBack(int s, int t, string productId, string composition, string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcFeedBack, FeedBack>());
                    var query = from i in dc.FeedBacks
                                where i.State != PMSCommon.SimpleState.作废.ToString()
                                &&i.ProductID.Contains(productId)
                                &&i.Composition.Contains(composition)
                                &&i.Customer.Contains(customer)
                                orderby i.CreateTime descending
                                select i;

                    return Mapper.Map<List<FeedBack>,List<DcFeedBack>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetFeedBackCount(string productId, string composition, string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.FeedBacks
                                where i.State != PMSCommon.SimpleState.作废.ToString()
                                && i.ProductID.Contains(productId)
                                && i.Composition.Contains(composition)
                                && i.Customer.Contains(customer)
                                select i;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateFeedBack(DcFeedBack model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcFeedBack, FeedBack>());
                    var entity = Mapper.Map<FeedBack>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}