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
    public partial class PMSService : IRecordVHPService
    {
        public int AddRecordVHP(DcRecordVHP model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordVHP, RecordVHP>());
                var newModel = Mapper.Map<RecordVHP>(model);
                dc.RecordVHPs.Add(newModel);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordVHP(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var model = dc.RecordVHPs.Find(id);
                dc.RecordVHPs.Remove(model);
                result = dc.SaveChanges();
                return result;
            }
        }


        public List<DcRecordVHP> GetRecordVHP(Guid planVHPId)
        {
            using (var dc = new PMSDbContext())
            {
                var result = dc.RecordVHPs.Where(p => p.PlanVHPID == planVHPId)
                    .OrderByDescending(v => v.CurrentTime).ToList();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<RecordVHP, DcRecordVHP>();
                });

                var final = Mapper.Map<List<RecordVHP>, List<DcRecordVHP>>(result);
                return final;
            }
        }

        public int GetRecordVHPCount()
        {
            using (var dc = new PMSDbContext())
            {
                var result = dc.RecordVHPs.Count();
                return result;
            }
        }
        public int UpdateReocrdVHP(DcRecordVHP model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordVHP, RecordVHP>());
                var newModel = Mapper.Map<RecordVHP>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }


    }
}