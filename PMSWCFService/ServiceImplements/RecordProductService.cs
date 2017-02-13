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
    public partial class PMSService : IRecordProductService
    {
        public int AddRecordProduct(DcRecordProduct model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordProduct, RecordProduct>());
                var product = Mapper.Map<RecordProduct>(model);
                dc.RecordProducts.Add(product);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordProduct(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var product = dc.RecordProducts.Find(id);
                if (product != null)
                {
                    dc.RecordProducts.Remove(product);
                    result = dc.SaveChanges();
                }

                return result;
            }
        }

        public List<DcRecordProduct> GetRecordProductBySearchInPage(int skip, int take, string productId, string compositionStd)
        {
            using (var dc = new PMSDbContext())
            {
                var result = dc.RecordProducts.Where(p => p.ProductID.Contains(productId) && p.Composition.Contains(compositionStd)
                  && p.State != ModelState.Deleted.ToString()).OrderByDescending(p => p.CreateTime).Skip(skip).Take(take).ToList();
                Mapper.Initialize(cfg => cfg.CreateMap<RecordProduct, DcRecordProduct>());
                var products = Mapper.Map<List<RecordProduct>, List<DcRecordProduct>>(result);
                return products;
            }
        }

        public int GetRecordProductCountBySearchInPage(string productId, string compositionStd)
        {
            using (var dc = new PMSDbContext())
            {
                return dc.RecordProducts.Where(p => p.ProductID.Contains(productId) && p.Composition.Contains(compositionStd)
                  && p.State != ModelState.Deleted.ToString()).Count();
            }
        }

        public int UpdateRecordProduct(DcRecordProduct model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordProduct, RecordProduct>());
                var product = Mapper.Map<RecordProduct>(model);
                dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }
    }
}