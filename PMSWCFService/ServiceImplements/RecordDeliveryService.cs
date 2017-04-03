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
    public partial class PMSService : IRecordDeliveryService
    {
        public int AddRecordDelivery(DcRecordDelivery model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>();
                        cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>();
                    });
                    var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
                    dc.RecordDeliverys.Add(record);
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

        public int AddRecordDeliveryItem(DcRecordDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>());
                    var record = Mapper.Map<PMSDAL.RecordDeliveryItem>(model);
                    dc.RecordDeliveryItems.Add(record);
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

        public int DeleteRecordDelivery(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    var record = dc.RecordDeliverys.Find(id);
                    dc.RecordDeliverys.Remove(record);
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

        public int DeleteRecordDeliveryItem(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    var record = dc.RecordDeliveryItems.Find(id);
                    dc.RecordDeliveryItems.Remove(record);
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

        public List<DcRecordDelivery> GetDelivery(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var result = dc.RecordDeliverys.Include("DeliveryItems")
                        .OrderByDescending(d => d.CreateTime)
                        .ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSDAL.RecordDelivery, DcRecordDelivery>();
                        cfg.CreateMap<PMSDAL.RecordDeliveryItem, DcRecordDeliveryItem>();
                    });

                    var records = Mapper.Map<List<PMSDAL.RecordDelivery>, List<DcRecordDelivery>>(result);

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
                    return dc.RecordDeliverys.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcRecordDeliveryItem> GetRecordDeliveryItemByRecordDeliveryID(Guid id)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordDeliveryItem, DcRecordDeliveryItem>());

                    var result = dc.RecordDeliveryItems
                        .Where(i => i.DeliveryID == id && i.State != PMSCommon.CommonState.Deleted.ToString())
                        .ToList();
                    return Mapper.Map<List<RecordDeliveryItem>, List<DcRecordDeliveryItem>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateReocrdDelivery(DcRecordDelivery model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>();
                        cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>();
                    });
                    var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
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

        public int UpdateReocrdDeliveryItem(DcRecordDeliveryItem model)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>());
                    var record = Mapper.Map<PMSDAL.RecordDeliveryItem>(model);
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