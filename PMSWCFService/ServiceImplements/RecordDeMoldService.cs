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
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordDeMold, RecordDeMold>());
                    var temp = Mapper.Map<RecordDeMold>(model);
                    dc.RecordDeMolds.Add(temp);
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

        public int DeleteRecordDeMold(Guid id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcRecordDeMold> GetRecordDeMolds(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordDeMolds.OrderByDescending(i => i.CreateTime).Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordDeMold, DcRecordDeMold>());
                    return Mapper.Map<List<RecordDeMold>, List<DcRecordDeMold>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int GetRecordDeMoldsCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordDeMolds.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateRecordDeMold(DcRecordDeMold model)
        {
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }
    }
}