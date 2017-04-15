using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;

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
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.Products.Find(id);
                    if (product != null)
                    {
                        dc.Products.Remove(product);
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

        public int GetProductCount(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.Products.Where(p => p.ProductID.Contains(productid) && p.Composition.Contains(composition)
                      && p.State != CommonState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcProduct> GetProducts(int skip, int take, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.Products.Where(p => p.ProductID.Contains(productid) && p.Composition.Contains(composition)
                      && p.State != ProductState.作废.ToString()).OrderByDescending(p => p.CreateTime).Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, DcProduct>());
                    var products = Mapper.Map<List<Product>, List<DcProduct>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateProduct(DcProduct model)
        {
            throw new NotImplementedException();
        }
    }
}