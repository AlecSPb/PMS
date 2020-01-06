using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSWCFService.DataContracts;
using PMSCommon;
using System.Data.Entity;

namespace PMSWCFService
{
    public partial class PMSService : IRecordBondingService
    {
        public int AddRecordBongding(DcRecordBonding model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordBonding, RecordBonding>());
                    var product = Mapper.Map<RecordBonding>(model);
                    dc.RecordBondings.Add(product);
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

        public int AddRecordBongdingByUID(DcRecordBonding model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddRecordBongding(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordBongding(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.RecordBondings.Find(id);
                    if (product != null)
                    {
                        dc.RecordBondings.Remove(product);
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondingByProductID(string productid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.TargetProductID.Trim() == productid.Trim()
                                && p.State != BondingState.作废.ToString()
                                orderby DbFunctions.TruncateTime(p.CreateTime) descending,
                                    p.PlanBatchNumber descending, p.TargetProductID descending
                                select p;
                    var result = query.ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetRecordBondingCount(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordBondings.Where(p => p.TargetProductID.Contains(productid) && p.TargetComposition.Contains(composition)
                      && p.State != BondingState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondings(int skip, int take, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.TargetProductID.Contains(productid)
                                && p.TargetComposition.Contains(composition)
                                && p.State != BondingState.作废.ToString()
                                orderby DbFunctions.TruncateTime(p.CreateTime) descending,
                                    p.PlanBatchNumber descending, p.TargetProductID descending
                                select p;
                    var result = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        public int GetRecordBondingCountNew(string productid, string composition, string platelot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordBondings.Where(p => p.TargetProductID.Contains(productid)
                    && p.TargetComposition.Contains(composition)
                    && p.PlateLot.Contains(platelot)
                      && p.State != BondingState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondingsNew(int skip, int take, string productid, string composition, string platelot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.TargetProductID.Contains(productid)
                                && p.TargetComposition.Contains(composition)
                                && p.PlateLot.Contains(platelot)
                                && p.State != BondingState.作废.ToString()
                                orderby DbFunctions.TruncateTime(p.CreateTime) descending,
                                    p.PlanBatchNumber descending, p.TargetProductID descending
                                select p;
                    var result = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetUnFinishedRecordBondings()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordBondings.Where(p => p.State == BondingState.未完成.ToString())
                        .OrderByDescending(p => p.CreateTime).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordBongding(DcRecordBonding model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordBonding, RecordBonding>());
                    var product = Mapper.Map<RecordBonding>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
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

        public int UpdateRecordBongdingByUID(DcRecordBonding model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateRecordBongding(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcRecordBonding model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRecordBonding, RecordBondingHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordBondingHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordBondingHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
        }

        public int SetAllUnFinsihToTempFinish()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.State == BondingState.未完成.ToString()
                                select p;

                    int result = 0;
                    foreach (var item in query)
                    {
                        item.State = BondingState.未录完.ToString();
                        dc.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int CheckPlateUsedTimes(string platelot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.State != CommonState.作废.ToString()
                                && p.PlateLot.TrimEnd(new char[] { 'A' }) == platelot
                                select p;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public List<DcRecordBonding> GetRecordBondingsByPMINumber(string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.TargetPMINumber == pminumber
                                && p.State != BondingState.作废.ToString()
                                orderby DbFunctions.TruncateTime(p.CreateTime) descending,
                                    p.PlanBatchNumber descending, p.TargetProductID descending
                                select p;
                    var result = query.ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondingsByDateTime(DateTime start, DateTime end)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RecordBondings
                                where p.CreateTime >= DbFunctions.TruncateTime(start)
                                && p.CreateTime <= DbFunctions.TruncateTime(end)
                                && p.State != BondingState.作废.ToString()
                                orderby p.CreateTime descending,
                                    p.PlanBatchNumber descending, p.TargetProductID descending
                                select p;
                    var result = query.ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
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