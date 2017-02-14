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
    public partial class PMSService : IRecordTestResultService
    {
        public int AddRecordTestResult(DcRecordTestResult model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTestResult, RecordTestResult>());
                var product = Mapper.Map<RecordTestResult>(model);
                dc.RecordTestResults.Add(product);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordTestResult(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var product = dc.RecordTestResults.Find(id);
                if (product != null)
                {
                    dc.RecordTestResults.Remove(product);
                    result = dc.SaveChanges();
                }

                return result;
            }
        }

        public List<DcRecordTestResult> GetRecordTestResultBySearchInPage(int skip, int take, string productId, string compositionStd)
        {
            using (var dc = new PMSDbContext())
            {
                var result = dc.RecordTestResults.Where(p => p.ProductID.Contains(productId) && p.Composition.Contains(compositionStd)
                  && p.State != OrderState.Deleted.ToString()).OrderByDescending(p => p.CreateTime).Skip(skip).Take(take).ToList();
                Mapper.Initialize(cfg => cfg.CreateMap<RecordTestResult, DcRecordTestResult>());
                var products = Mapper.Map<List<RecordTestResult>, List<DcRecordTestResult>>(result);
                return products;
            }
        }

        public int GetRecordTestResultCountBySearchInPage(string productId, string compositionStd)
        {
            using (var dc = new PMSDbContext())
            {
                return dc.RecordTestResults.Where(p => p.ProductID.Contains(productId) && p.Composition.Contains(compositionStd)
                  && p.State != OrderState.Deleted.ToString()).Count();
            }
        }

        public int UpdateRecordTestResult(DcRecordTestResult model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTestResult, RecordTestResult>());
                var product = Mapper.Map<RecordTestResult>(model);
                dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }
    }
}