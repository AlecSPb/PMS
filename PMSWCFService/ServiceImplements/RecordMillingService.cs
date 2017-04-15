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
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int DeleteRecordMilling(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var model = dc.RecordMillings.Find(id);
                    dc.RecordMillings.Remove(model);
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

        public int GetRecordMillingCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordMillings.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetRecordMillingCountByVHPPlanLot(string vhpplanlot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from r in dc.RecordMillings
                                where r.VHPPlanLot.Contains(vhpplanlot) && r.State != PMSCommon.SimpleState.作废.ToString()
                                select r;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMilling> GetRecordMillings(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.State != PMSCommon.SimpleState.作废.ToString()
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMilling> GetRecordMillingsByVHPPlanLot(int skip, int take, string vhpplanlot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.VHPPlanLot.Contains(vhpplanlot) && r.State != PMSCommon.SimpleState.作废.ToString()
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordMilling(DcRecordMilling model)
        {
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }
    }
}