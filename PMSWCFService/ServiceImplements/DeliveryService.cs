using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public partial class PMSService : IDeliveryService
    {
        public int AddDelivery(DcDelivery model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcDelivery, Delivery>();
                    });
                    var record = Mapper.Map<Delivery>(model);
                    dc.Deliverys.Add(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw;
            }

        }

        public int AddDeliveryByUID(DcDelivery model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddDelivery(model);
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddDeliveryItem(DcDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItem, DeliveryItem>());
                    var record = Mapper.Map<DeliveryItem>(model);
                    dc.DeliveryItems.Add(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw;
            }

        }

        public int AddDeliveryItemByUID(DcDeliveryItem model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddDeliveryItem(model);
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteDelivery(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var record = dc.Deliverys.Find(id);
                    dc.Deliverys.Remove(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw;
            }

        }

        public int DeleteDeliveryItem(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var record = dc.DeliveryItems.Find(id);
                    dc.DeliveryItems.Remove(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcDelivery> GetDelivery(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.Skip(skip).Take(take).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public List<DcDelivery> GetDeliveryBySearch(int skip, int take, string deliveryName)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                &&d.DeliveryName.Contains(deliveryName)
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.Skip(skip).Take(take).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetDeliveryCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                select d;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetDeliveryCountBySearch(string deliveryName)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.DeliveryName.Contains(deliveryName)
                                select d;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItem> GetDeliveryItemByDeliveryID(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DeliveryItem, DcDeliveryItem>());

                    var result = dc.DeliveryItems
                        .Where(i => i.DeliveryID == id && i.State != PMSCommon.SimpleState.作废.ToString())
                        .OrderByDescending(i=>i.CreateTime)
                        .ToList();
                    return Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(result);
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public List<DcDeliveryItemExtra> GetDeliveryItemExtra(int skip, int take, string productid, string composition,string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                 join dd in dc.Deliverys
                                 on d.DeliveryID equals dd.ID
                                 where d.State != PMSCommon.SimpleState.作废.ToString()
                                 && dd.State!=PMSCommon.DeliveryState.作废.ToString()
                                 && d.ProductID.Contains(productid)
                                 && d.Composition.Contains(composition)
                                 && d.Customer.Contains(customer)
                                orderby  dd.CreateTime descending,d.CreateTime descending
                                 select new PMSDeliveryItemExtra()
                                 {
                                     Delivery = dd,
                                     DeliveryItem=d
                                 };

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSDeliveryItemExtra, DcDeliveryItemExtra>();
                        cfg.CreateMap<Delivery, DcDelivery>();
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<PMSDeliveryItemExtra>, List<DcDeliveryItemExtra>>(query.Skip(skip).Take(take).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }


        public int GetDeliveryItemExtraCount(string productid, string composition,string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                join dd in dc.Deliverys
                                on d.DeliveryID equals dd.ID
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && dd.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.Composition.Contains(composition)
                                &&d.Customer.Contains(customer)
                                select new PMSDeliveryItemExtra()
                                {
                                    Delivery = dd,
                                    DeliveryItem = d
                                };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }
        public List<DcDeliveryItemExtra> GetDeliveryItemExtraByYear(int skip, int take, int year)
        {
            throw new NotImplementedException();
        }
        public int GetDeliveryItemExtraCountByYear(string productid, int year)
        {
            throw new NotImplementedException();
        }

        public List<DcDeliveryItem> GetDeliveryItems(int skip, int take, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                && d.Composition.Contains(composition)
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(query.Skip(skip).Take(take).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryItemsCount(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.ProductID.Contains(productid)
                                &&d.Composition.Contains(composition)
                                select d;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateDelivery(DcDelivery model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcDelivery, Delivery>();
                        cfg.CreateMap<DcDeliveryItem, DeliveryItem>();
                    });
                    var record = Mapper.Map<Delivery>(model);
                    dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int UpdateDeliveryByUID(DcDelivery model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateDelivery(model);
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateDeliveryItem(DcDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItem, DeliveryItem>());
                    var record = Mapper.Map<DeliveryItem>(model);
                    dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int UpdateDeliveryItemByUID(DcDeliveryItem model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateDeliveryItem(model);
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcDelivery model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcDelivery, DeliveryHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<DeliveryHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.DeliveryHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
            }
        }
        private void SaveHistory(DcDeliveryItem model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcDeliveryItem,DeliveryItemHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<DeliveryItemHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.DeliveryItemHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
            }
        }

        public List<DcDeliveryItem> GetDeliveryItemByProductID(string productid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItems
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.ProductID==productid
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(query.ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcDelivery> GetDeliveryUnFinished()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State == PMSCommon.DeliveryState.未完成.ToString()
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}