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
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcDelivery, PMSDAL.Delivery>();
                        cfg.CreateMap<DcDeliveryItem, PMSDAL.DeliveryItem>();
                    });
                    var record = Mapper.Map<PMSDAL.Delivery>(model);
                    dc.Deliverys.Add(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int AddDeliveryItem(DcDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItem, PMSDAL.DeliveryItem>());
                    var record = Mapper.Map<PMSDAL.DeliveryItem>(model);
                    dc.DeliveryItems.Add(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int DeleteDelivery(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
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
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int DeleteDeliveryItem(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
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
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcDelivery> GetDelivery(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var result = dc.Deliverys
                        .OrderByDescending(d => d.CreateTime)
                        .ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSDAL.Delivery, DcDelivery>();
                        //cfg.CreateMap<PMSDAL.DeliveryItem, DcDeliveryItem>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(result);

                    return records;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int GetDeliveryCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    return dc.Deliverys.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcDeliveryItem> GetDeliveryItemByDeliveryID(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DeliveryItem, DcDeliveryItem>());

                    var result = dc.DeliveryItems
                        .Where(i => i.DeliveryID == id && i.State != PMSCommon.CommonState.作废.ToString())
                        .OrderByDescending(i=>i.CreateTime)
                        .ToList();
                    return Mapper.Map<List<DeliveryItem>, List<DcDeliveryItem>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateDelivery(DcDelivery model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcDelivery, PMSDAL.Delivery>();
                        cfg.CreateMap<DcDeliveryItem, PMSDAL.DeliveryItem>();
                    });
                    var record = Mapper.Map<PMSDAL.Delivery>(model);
                    dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateDeliveryItem(DcDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItem, PMSDAL.DeliveryItem>());
                    var record = Mapper.Map<PMSDAL.DeliveryItem>(model);
                    dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }
    }
}