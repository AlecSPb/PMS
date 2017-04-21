using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
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
                    Mapper.Initialize(cfg => cfg.CreateMap<DcProduct, Product>());
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
                    var query = from p in dc.Products
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State != ProductState.作废.ToString()
                                orderby p.CreateTime descending
                                select p;
                    return query.Count();
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
                    var query = from p in dc.Products
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State != ProductState.作废.ToString()
                                orderby p.CreateTime descending
                                select p;
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, DcProduct>());
                    var products = Mapper.Map<List<Product>, List<DcProduct>>(query.Skip(skip).Take(take).ToList());
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
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcProduct, Product>());
                    var product = Mapper.Map<Product>(model);
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