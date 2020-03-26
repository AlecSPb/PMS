using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using System.Data.Entity;

namespace PMSWCFService
{
    /// <summary>
    /// 2020-3-21 全新设计的新服务
    /// </summary>
    public class NewService : INewService
    {
        public int AddOutsideProcess(DcOutsideProcess model)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public DateTime GetOrderLastUpdateTime(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcOutsideProcess> GetOutsideProcess(int s, int t, string productid, string composition, string provider)
        {
            throw new NotImplementedException();
        }

        public int GetOutsideProcessCount(string productid, string composition, string provider)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOrder, PMSOrder>());
                    var entity = Mapper.Map<PMSOrder>(model);
                    entity.LastUpdateTime = DateTime.Now;
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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

        public List<DcPlateUsedStatistic> GetPlateUsedStatistics()
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

                    return query.ToList();
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
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from p in db.RecordBondings
                                where p.State != PMSCommon.BondingState.作废.ToString()
                                && p.PlateLot.Replace("A", "") == plateid
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
    }
}