using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using AutoMapper;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;

namespace PMSWCFService
{
    public partial class PMSService : IRecordDeMoldService
    {
        public int AddRecordDeMold(DcRecordDeMold model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeMold, RecordDeMold>());
                var temp = Mapper.Map<RecordDeMold>(model);
                dc.RecordDeMolds.Add(temp);
                result=dc.SaveChanges();
                return result;
            }
        }

        public int DeleteRecordDeMold(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordDeMold> GetRecordDeMolds(int skip, int take)
        {
            using (var dc=new PMSDbContext())
            {
                var result = dc.RecordDeMolds.OrderByDescending(i => i.CreateTime).Skip(skip).Take(take).ToList();
                Mapper.Initialize(cfg => cfg.CreateMap<RecordDeMold, DcRecordDeMold>());
                return Mapper.Map<List<RecordDeMold>, List<DcRecordDeMold>>(result);
            }
        }

        public int GetRecordDeMoldsCount()
        {
            using (var dc=new PMSDbContext())
            {
                return dc.RecordDeMolds.Count();
            }
        }

        public int UpdateRecordDeMold(DcRecordDeMold model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeMold, RecordDeMold>());
                var temp = Mapper.Map<RecordDeMold>(model);
                dc.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }
    }
}