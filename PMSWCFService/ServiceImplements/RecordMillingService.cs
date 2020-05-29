using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;
using PMSWCFService.ServiceImplements.Helpers;

namespace PMSWCFService
{
    public partial class PMSService : IRecordMillingService
    {
        public int AddRecordMilling(DcRecordMilling model)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int AddRecordMillingByUID(DcRecordMilling model, string uid)
        {
            try
            {
                XS.RunLog();
                SaveHistory(model, uid);
                return AddRecordMilling(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordMilling(Guid id)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int GetRecordMillingCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordMillings.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }


        public List<DcRecordMilling> GetRecordMillings(int skip, int take)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMilling> GetRecordMillingsByVHPPlanLot(int skip, int take, string vhpplanlot, string composition)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.VHPPlanLot.Contains(vhpplanlot)
                                && r.Composition.Contains(searchItem.Item1)
                                && r.Composition.Contains(searchItem.Item2)
                                && r.Composition.Contains(searchItem.Item3)
                                && r.Composition.Contains(searchItem.Item4)
                                && r.State != PMSCommon.SimpleState.作废.ToString()
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetRecordMillingCountByVHPPlanLot(string vhpplanlot, string composition)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from r in dc.RecordMillings
                                where r.VHPPlanLot.Contains(vhpplanlot)
                                && r.Composition.Contains(searchItem.Item1)
                                && r.Composition.Contains(searchItem.Item2)
                                && r.Composition.Contains(searchItem.Item3)
                                && r.Composition.Contains(searchItem.Item4)
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

        public int UpdateRecordMilling(DcRecordMilling model)
        {
            try
            {
                XS.RunLog();
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
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int UpdateRecordMillingByUID(DcRecordMilling model, string uid)
        {
            try
            {
                XS.RunLog();
                SaveHistory(model, uid);
                return UpdateRecordMilling(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        private void SaveHistory(DcRecordMilling model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRecordMilling, RecordMillingHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordMillingHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordMillingHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcRecordMilling> GetRecordMillingByMaterialType(string materialType, int topCount)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.State != PMSCommon.SimpleState.作废.ToString()
                                && r.MaterialType == materialType
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.Take(topCount).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public double GetAllPowderWeight()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from r in dc.RecordMillings
                                where r.State != PMSCommon.SimpleState.作废.ToString()
                                select r;
                    return query.Sum(i => i.WeightOut);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMilling> GetRecordMillingsByPMINumber(string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.State != PMSCommon.SimpleState.作废.ToString()
                                && r.PMINumber == pminumber
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}