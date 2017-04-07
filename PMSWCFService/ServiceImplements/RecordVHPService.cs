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
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int DeleteRecordVHP(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }


        public List<DcRecordVHP> GetRecordVHP(Guid planVHPId)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordVHPs.Where(p => p.PlanVHPID == planVHPId&&p.State!=PMSCommon.SimpleState.Deleted.ToString())
                        .OrderByDescending(v => v.CurrentTime).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<RecordVHP, DcRecordVHP>();
                    });

                    var final = Mapper.Map<List<RecordVHP>, List<DcRecordVHP>>(result);
                    return final;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetRecordVHPCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordVHPs.Count();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }
        public int UpdateReocrdVHP(DcRecordVHP model)
        {
            try
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
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }


    }
}