using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using System.Data.Entity;
using PMSCommon;

namespace PMSWCFService
{
    /// <summary>
    /// 2020-3-21 全新设计的新服务
    /// </summary>
    public class NewService : INewService
    {
        public int AddOutsideProcess(DcOutsideProcess model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOutsideProcess, OutsideProcess>());
                    var entity = Mapper.Map<OutsideProcess>(model);
                    dc.OutsideProcesses.Add(entity);
                    dc.SaveChanges();
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public void AddOrder(DcOrder model, string user)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOrder, PMSOrder>());
                    var entity = Mapper.Map<PMSOrder>(model);
                    entity.LastUpdateTime = DateTime.Now;
                    db.Orders.Add(entity);
                    db.SaveChanges();
                    SaveOrderHistory(model, user);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void AddPlan(DcPlanVHP model, string user)
        {
            throw new NotImplementedException();
        }

        public List<DcOrder> GetMisson(int s, int t, string composition, string pminumber, string state)
        {
            throw new NotImplementedException();
        }

        public int GetMissonCount(string composition, string pminumber, string state)
        {
            throw new NotImplementedException();
        }

        public List<DcOrder> GetOrder(int s, int t, string customer, string composition, string pminumber, string state)
        {
            XS.RunLog();

            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from o in dc.Orders
                                where o.CustomerName.Contains(customer)
                                && o.CompositionStandard.Contains(composition)
                                && o.PMINumber.Contains(pminumber)
                                && o.State.Contains(state)
                                orderby o.CreateTime descending
                                select o;

                    var result = mapper.Map<List<PMSOrder>, List<DcOrder>>(query.Skip(s).Take(t).ToList());
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcOrder GetOrderByID(Guid id)
        {

            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSOrder, DcOrder>());
                    return Mapper.Map<DcOrder>(db.Orders.Find(id));
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetOrderCount(string customer, string composition, string pminumber, string state)
        {
            XS.RunLog();

            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.Orders.Where(o => o.CustomerName.Contains(customer)
                    && o.CompositionStandard.Contains(composition)
                    && o.PMINumber.Contains(pminumber)
                    && o.State.Contains(state)).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DateTime GetOrderLastUpdateTime(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var r = db.Orders.Find(id);
                    return r.LastUpdateTime;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return DateTime.Now;
            }
        }



        public List<DcPlanExtra> GetPlan(int s, int t, string composition, string pminumber)
        {
            throw new NotImplementedException();
        }

        public int GetPlanCount(string composition, string pminumber)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordTest> GetRecordTest(int s, int t, string composition, string customer, string pminumber)
        {
            throw new NotImplementedException();
        }

        public int GetRecordTestCount(string composition, string customer, string pminumber)
        {
            throw new NotImplementedException();
        }

        public DateTime GetRecordTestLastUpdateTime(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var r = db.RecordTests.Find(id);
                    return r.LastUpdateTime;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return DateTime.Now;
            }
        }

        public void LockTodayPlans()
        {
            if (DateTime.Now > DateTime.Today.AddHours(16).AddMinutes(0))
            {
                using (var db = new PMSDbContext())
                {
                    var today = DateTime.Today;
                    var today_unlocked_plans = from p in db.VHPPlans
                                               where DbFunctions.TruncateTime(p.PlanDate) == today
                                               && p.State == "已核验"
                                               && p.IsLocked == false
                                               select p;
                    //如果存在没有锁定的计划
                    if (today_unlocked_plans.Count() > 0)
                    {
                        foreach (var p in today_unlocked_plans)
                        {
                            p.IsLocked = true;
                            db.Entry(p).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                        XS.Current.Debug("成功触发LockAllTodaysPlan锁定事件");
                    }
                }
            }
        }

        public int UpdateOutsideProcess(DcOutsideProcess model)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(DcOrder model, string user)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcOrder, PMSOrder>();
                    });
                    var mapper = config.CreateMapper();
                    PMSOrder pmsOrder = mapper.Map<PMSOrder>(model);
                    dc.Entry(pmsOrder).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();

                    SaveOrderHistory(model, user);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdatePlan(DcPlanVHP model, string user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录订单历史
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uid"></param>
        private void SaveOrderHistory(DcOrder model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOrder, PMSOrderHistory>());
                    var history = Mapper.Map<PMSOrderHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    db.OrderHistorys.Add(history);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcPlateUsedStatistic> GetPlateUsedStatistics(int s, int t)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from p in db.RecordBondings
                                where p.State != PMSCommon.BondingState.作废.ToString()
                                group p by p.PlateLot.Replace("A", "") into g
                                orderby g.Count() descending
                                select new DcPlateUsedStatistic { PlateLot = g.Key, Count = g.Count() };

                    return query.Skip(s).Take(t).ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlateUsedStatisticsCount()
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from p in db.RecordBondings
                                where p.State != PMSCommon.BondingState.作废.ToString()
                                group p by p.PlateLot.Replace("A", "") into g
                                select new DcPlateUsedStatistic { PlateLot = g.Key, Count = g.Count() };

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlateUsedTimesByPlateID(string plateid)
        {
            try
            {
                string new_plateid = plateid.Replace("A", "");
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from p in db.RecordBondings
                                where p.State != PMSCommon.BondingState.作废.ToString()
                                && p.PlateLot.Replace("A", "") == new_plateid
                                select p;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public bool CheckOrderPMINumberExist(string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.PMINumber == pminumber && i.State != "作废"
                                select i;
                    return query.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetOrderUnFinishedCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.State == OrderState.未完成.ToString()
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public double GetOrderUnFinishedTargetCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.ProductType == "靶材" && (o.State == OrderState.未完成.ToString()
                                 || o.State == OrderState.未核验.ToString())
                                select o;
                    return query.Sum(i => i.Quantity);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcOrder GetOrderByPMINumber(string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var result = mapper.Map<PMSOrder, DcOrder>(
                        dc.Orders.Where(i => i.PMINumber == pminumber).FirstOrDefault());
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcOutsideProcess> GetOutsideProcess(int s, int t, string productid, string composition, string provider, string state)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OutsideProcess, DcOutsideProcess>());
                    var query = from i in dc.OutsideProcesses
                                where i.State.Contains(state)
                                && i.ProductID.Contains(productid)
                                && i.Composition.Contains(composition)
                                orderby i.CreateTime descending, i.ProductID descending
                                select i;
                    return Mapper.Map<List<OutsideProcess>, List<DcOutsideProcess>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int GetOutsideProcessCount(string productid, string composition, string provider, string state)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.OutsideProcesses
                                where i.State.Contains(state)
                                && i.ProductID.Contains(productid)
                                && i.Composition.Contains(composition)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }
    }
}