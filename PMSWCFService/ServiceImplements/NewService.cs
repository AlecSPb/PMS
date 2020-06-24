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
using AuthorizationChecker;
using PMSWCFService.ServiceImplements.Helpers;

namespace PMSWCFService
{
    /// <summary>
    /// 2020-3-21 全新设计的新服务
    /// </summary>
    public class NewService : INewService
    {
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
            try
            {   //自动生成搜索号
                XS.RunLog();
                model.SearchCode = $"{model.PlanDate.ToString("yyMMdd")}-{model.VHPDeviceCode}";
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.VHPPlans.Add(plan);
                    dc.SaveChanges();
                    SavePlanHistory(model, user);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcOrder> GetMisson(int s, int t, string composition, string pminumber, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);

                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.State.Contains(state)
                                 && o.State != PMSCommon.OrderState.作废.ToString()
                                 && o.State != PMSCommon.OrderState.未核验.ToString()
                                 && o.State != PMSCommon.OrderState.取消.ToString()
                                 && o.CompositionStandard.Contains(searchItem.Item1)
                                 && o.CompositionStandard.Contains(searchItem.Item2)
                                 && o.CompositionStandard.Contains(searchItem.Item3)
                                 && o.CompositionStandard.Contains(searchItem.Item4)
                                 && o.PMINumber.Contains(pminumber)
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(s).Take(t).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMissonCount(string composition, string pminumber, string state)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.CompositionStandard.Contains(searchItem.Item1)
                                 && o.CompositionStandard.Contains(searchItem.Item2)
                                 && o.CompositionStandard.Contains(searchItem.Item3)
                                 && o.CompositionStandard.Contains(searchItem.Item4)
                                 && o.State.Contains(state)
                                 && o.State != PMSCommon.OrderState.作废.ToString()
                                 && o.State != PMSCommon.OrderState.未核验.ToString()
                                 && o.State != PMSCommon.OrderState.取消.ToString()
                                 && o.PMINumber.Contains(pminumber)
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

        public List<DcOrder> GetOrder(int s, int t, string customer, string composition, string pminumber, string state)
        {
            XS.RunLog();

            try
            {
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from o in dc.Orders
                                where o.CustomerName.Contains(customer)
                                && o.CompositionStandard.Contains(searchItem.Item1)
                                && o.CompositionStandard.Contains(searchItem.Item2)
                                && o.CompositionStandard.Contains(searchItem.Item3)
                                && o.CompositionStandard.Contains(searchItem.Item4)
                                && o.PMINumber.Contains(pminumber)
                                && o.State.Contains(state)
                                && o.State != PMSCommon.OrderState.作废.ToString()
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

        public int GetOrderCount(string customer, string composition, string pminumber, string state)
        {
            XS.RunLog();

            try
            {
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.CustomerName.Contains(customer)
                                && o.CompositionStandard.Contains(searchItem.Item1)
                                && o.CompositionStandard.Contains(searchItem.Item2)
                                && o.CompositionStandard.Contains(searchItem.Item3)
                                && o.CompositionStandard.Contains(searchItem.Item4)
                                && o.PMINumber.Contains(pminumber)
                                && o.State.Contains(state)
                                && o.State != PMSCommon.OrderState.作废.ToString()
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



        public List<DcPlanExtra> GetPlanExtra(int s, int t, string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(searchItem.Item1)
                                     && o.CompositionStandard.Contains(searchItem.Item2)
                                     && o.CompositionStandard.Contains(searchItem.Item3)
                                     && o.CompositionStandard.Contains(searchItem.Item4)
                                     && o.PMINumber.Contains(pminumber)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanExtraModel() { Plan = p, Misson = o };

                    var final = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanExtraModel, DcPlanExtra>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanExtraModel>, List<DcPlanExtra>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraCount(string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(searchItem.Item1)
                                     && o.CompositionStandard.Contains(searchItem.Item2)
                                     && o.CompositionStandard.Contains(searchItem.Item3)
                                     && o.CompositionStandard.Contains(searchItem.Item4)
                                     && o.PMINumber.Contains(pminumber)
                                select new PMSPlanExtraModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
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


        public void UpdateOrder(DcOrder model, string user)
        {
            try
            {
                XS.RunLog();

                model.LastUpdateTime = DateTime.Now;

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
            try
            {
                XS.RunLog();
                //自动生成搜索号
                model.SearchCode = $"{model.PlanDate.ToString("yyMMdd")}-{model.VHPDeviceCode}";
                model.UpdateTime = DateTime.Now;

                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();

                    SavePlanHistory(model, user);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

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

        private void SavePlanHistory(DcPlanVHP model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHPHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<PMSPlanVHPHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.VHPPlanHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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

        public int GetOrderUnFinishedTargetCount()
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
                    double quantity = query.Sum(i => i.Quantity);
                    return (int)quantity;
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

        public int GetMissonUnCompletedCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.State == OrderState.未完成.ToString()
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

        public int GetMissonUnVHPTargetCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType == PMSCommon.OrderPolicyType.VHP.ToString()
                                 && o.State == OrderState.未完成.ToString()
                                 && o.ProductType == "靶材"
                                select o;
                    return (int)query.Sum(i => i.Quantity);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetEmergencyOrderCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    return dc.Orders.Where(o => o.Priority.Contains("紧急")
                    && o.State == OrderState.未完成.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanVHP> GetPlansByOrderID(Guid id)
        {
            Checker.CheckIfCanRun();
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                    var result = from p in dc.VHPPlans
                                 where p.OrderID == id && p.State != VHPPlanState.作废.ToString()
                                 orderby p.PlanDate descending, p.PlanLot, p.VHPDeviceCode
                                 select p;
                    var plans = Mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(result.ToList());
                    return plans;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanExtra> GetPlanExtraForProduct(int skip, int take, string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他"
                                        || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
                                select new PMSPlanExtraModel() { Plan = p, Misson = o };

                    var final = query.Skip(skip).Take(take).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanExtraModel, DcPlanExtra>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanExtraModel>, List<DcPlanExtra>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanExtraForProductCount(string searchCode, string composition, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                & (p.PlanType == "加工" || p.PlanType == "其他"
                                || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                     && p.SearchCode.Contains(searchCode)
                                     && o.CompositionStandard.Contains(composition)
                                     && o.PMINumber.Contains(pminumber)
                                select new PMSPlanExtraModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetProductPlanCountByOrderID(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var result = from p in dc.VHPPlans
                                 where p.OrderID == id 
                                 && (p.PlanType==VHPPlanType.加工.ToString()
                                 || p.PlanType == VHPPlanType.外协.ToString() 
                                 || p.PlanType == VHPPlanType.发货.ToString())
                                 && p.State != VHPPlanState.作废.ToString()
                                 orderby p.PlanDate descending, p.PlanLot, p.VHPDeviceCode
                                 select p;
                    return result.Count();
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