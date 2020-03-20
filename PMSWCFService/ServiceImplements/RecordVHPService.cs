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
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int AddRecordVHPByUID(DcRecordVHP model, string uid)
        {
            try
            {
                XS.RunLog();
                SaveHistory(model, uid);
                return AddRecordVHP(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordVHP(Guid id)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }


        public List<DcRecordVHP> GetRecordVHP(Guid planVHPId)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordVHPs.Where(p => p.PlanVHPID == planVHPId&&p.State!=PMSCommon.SimpleState.作废.ToString())
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int GetRecordVHPCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordVHPs.Count();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }
        //TODO:以后的时候再修改这个错误拼写
        public int UpdateReocrdVHP(DcRecordVHP model)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int UpdateRecordVHPByUID(DcRecordVHP model, string uid)
        {
            try
            {
                XS.RunLog();
                SaveHistory(model, uid);
                return UpdateReocrdVHP(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcRecordVHP model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRecordVHP, RecordVHPHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordVHPHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordVHPHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

    }
}