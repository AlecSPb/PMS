using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IRecordTestService
    {
        public int AddRecordTest(DcRecordTest model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTest, RecordTest>());
                    var product = Mapper.Map<RecordTest>(model);
                    dc.RecordTests.Add(product);
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

        public int DeleteRecordTest(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.RecordTests.Find(id);
                    if (product != null)
                    {
                        dc.RecordTests.Remove(product);
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

        public List<DcRecordTest> GetRecordTestBySearchInPage(int skip, int take, string productId, string compositionStd)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State != CommonState.作废.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public List<DcRecordTest> GetRecordTestChecked(int skip, int take, string productId, string compositionStd)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State == CommonState.已核验.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetRecordTestCountBySearchInPage(string productId, string compositionStd)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State != CommonState.作废.ToString()
                                select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetRecordTestCountChecked(string productId, string compositionStd)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State == CommonState.已核验.ToString()
                                select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordTest(DcRecordTest model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTest, RecordTest>());
                    var product = Mapper.Map<RecordTest>(model);
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
    }
}