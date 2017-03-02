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
    public partial class PMSService : IRecordMillingService
    {
        public int AddRecordMilling(DcRecordMilling model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordMilling, RecordMilling>());
                var temp = Mapper.Map<RecordMilling>(model);
                dc.RecordMillings.Add(temp);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordMilling(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetRecordMillingCount()
        {
            using (var dc = new PMSDbContext())
            {
                return dc.RecordMillings.Count();
            }
        }

        public List<DcRecordMilling> GetRecordMillings(int skip, int take)
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                var result = dc.RecordMillings.OrderBy(i => i.CreateTime).Skip(skip).Take(take).ToList();
                return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(result);
            }


        }

        public int UpdateRecordMilling(DcRecordMilling model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordMilling, RecordMilling>());
                var temp = Mapper.Map<RecordMilling>(model);
                dc.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }
    }
}