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

        public int AddProductByUID(DcProduct model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddProduct(model);
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
                                && p.State != InventoryState.作废.ToString()
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

        public int GetProductCountByYear(string productid, int year)
        {
            throw new NotImplementedException();
        }

        public int GetProductCountUnCompleted(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.Products
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State == InventoryState.库存.ToString()
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
                                && p.State != InventoryState.作废.ToString()
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

        public List<DcProduct> GetProductsByYear(int skip, int take, int year)
        {
            throw new NotImplementedException();
        }

        public List<DcProduct> GetProductUnCompleted(int skip, int take, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.Products
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State == InventoryState.库存.ToString()
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

        public int UpdateProductByUID(DcProduct model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateProduct(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcProduct  model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcProduct, ProductHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<ProductHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.ProductHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
        }


    }
}