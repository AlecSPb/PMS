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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int AddRecordDeMoldByUID(DcRecordDeMold model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddRecordDeMold(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordDeMold(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var model = dc.RecordDeMolds.Find(id);
                    dc.RecordDeMolds.Remove(model);
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

        public List<DcRecordDeMold> GetRecordDeMolds(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordDeMolds.Where(r => r.State != PMSCommon.SimpleState.作废.ToString())
                        .OrderByDescending(i => i.CreateTime).Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordDeMold, DcRecordDeMold>());
                    return Mapper.Map<List<RecordDeMold>, List<DcRecordDeMold>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return null;
            }

        }

        public List<DcRecordDeMold> GetRecordDeMoldsByVHPPlanLot(int skip, int take, string vhpplanlot, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordDeMold, DcRecordDeMold>());
                    var query = from r in dc.RecordDeMolds
                                where r.VHPPlanLot.Contains(vhpplanlot)
                                && r.Composition.Contains(composition)
                                && r.State != PMSCommon.SimpleState.作废.ToString()
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordDeMold>, List<DcRecordDeMold>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return null;

            }
        }
        public int GetRecordDeMoldsCountByVHPPlanLot(string vhpplanlot, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from r in dc.RecordDeMolds
                                where r.VHPPlanLot.Contains(vhpplanlot)
                                && r.Composition.Contains(composition)
                                && r.State != PMSCommon.SimpleState.作废.ToString()
                                select r;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetRecordDeMoldsCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordDeMolds.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int UpdateRecordDeMoldByUID(DcRecordDeMold model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateRecordDeMold(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }


        private void SaveHistory(DcRecordDeMold model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRecordDeMold, RecordDeMoldHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordDeMoldHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordDeMoldHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcRecordDeMold> GetRecordDeMoldsByPMINumber(string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordDeMolds.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()
                    && r.PMINumber == pminumber)
                        .OrderByDescending(i => i.CreateTime).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordDeMold, DcRecordDeMold>());
                    return Mapper.Map<List<RecordDeMold>, List<DcRecordDeMold>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return null;
            }
        }
    }
}