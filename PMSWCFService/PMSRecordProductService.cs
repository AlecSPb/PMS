using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;

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
                dc.Products.Add(product);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordProduct(DcRecordProduct model)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordProduct> GetRecordProductBySearchInPage(int skip, int take, string productId, string compositionStd)
        {
            throw new NotImplementedException();
        }

        public int GetRecordProductCountBySearchInPage(int skip, int take, string productid, string compositionStd)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecordProduct(DcRecordProduct model)
        {
            throw new NotImplementedException();
        }
    }
}