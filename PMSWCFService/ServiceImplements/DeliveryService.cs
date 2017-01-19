using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public partial class PMSService : IRecordDeliveryService
    {
        public int AddRecordDelivery(DcRecordDelivery model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>());
                var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
                dc.Deliverys.Add(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int AddRecordDeliveryItem(DcRecordDeliveryItem model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeliveryItem, PMSDAL.RecordDeliveryItem>());
                var record = Mapper.Map<PMSDAL.RecordDeliveryItem>(model);
                dc.DeliveryItems.Add(record);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordDelivery(Guid id)
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

        public int DeleteRecordDeliveryItem(Guid id)
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

        public List<DcRecordDelivery> GetDelivery(int skip, int take, DateTime shipTime)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                var result = dc.Deliverys.Include("RecordDeliveryItem").Where(d => d.ShipTime.Date == shipTime.Date)
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

        public int UpdateReocrdDelivery(DcRecordDelivery model)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDelivery, PMSDAL.RecordDelivery>());
                var record = Mapper.Map<PMSDAL.RecordDelivery>(model);
                dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                result=dc.SaveChanges();
                return result;
            }
        }

        public int UpdateReocrdDeliveryItem(DcRecordDeliveryItem model)
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
    }
}