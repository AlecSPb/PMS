using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService.ServiceImplements
{
    public partial class PMSService : IProductService
    {
        public int AddProduct(DcProduct model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTest, RecordTest>());
                    var product = Mapper.Map<Product>(model);
                    dc.Products.Add(product);
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

        public int DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetProductCount(string productid, string composition)
        {
            throw new NotImplementedException();
        }

        public List<DcProduct> GetProducts(int skip, int take, string productid, string composition)
        {
            throw new NotImplementedException();
        }

        public int UpdateProduct(DcProduct model)
        {
            throw new NotImplementedException();
        }
    }
}