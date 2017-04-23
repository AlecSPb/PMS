using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSWCFService.DataContracts;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IRecordBondingService
    {
        public int AddRecordBongding(DcRecordBonding model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordBonding, RecordBonding>());
                    var product = Mapper.Map<RecordBonding>(model);
                    dc.RecordBondings.Add(product);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddRecordBongdingByUID(DcRecordBonding model, string uid)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordBongding(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.RecordBondings.Find(id);
                    if (product != null)
                    {
                        dc.RecordBondings.Remove(product);
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetRecordBondingCount(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordBondings.Where(p => p.TargetProductID.Contains(productid) && p.TargetComposition.Contains(composition)
                      && p.State != CommonState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondings(int skip, int take, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordBondings.Where(p => p.TargetProductID.Contains(productid) && p.TargetComposition.Contains(composition)
                      && p.State != CommonState.作废.ToString()).OrderByDescending(p => p.CreateTime).Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordBongding(DcRecordBonding model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordBonding, RecordBonding>());
                    var product = Mapper.Map<RecordBonding>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordBongdingByUID(DcRecordBonding model, string uid)
        {
            throw new NotImplementedException();
        }
    }
}