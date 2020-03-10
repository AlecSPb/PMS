using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    public class MainStatisticService : IMainStatisticService
    {

        public List<DcStatistic> GetDeliveryStatisticByCustomer(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.DeliveryItems
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.Customer into g
                                orderby g.Count() descending
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetDeliveryStatisticByMonth(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.DeliveryItems
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.CreateTime.Month into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetDeliveryStatisticBySeason(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.DeliveryItems
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by ((i.CreateTime.Month - 1) / 3 + 1) into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetDeliveryStatisticByProductType(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.DeliveryItems
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.ProductType into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetDeliveryStatisticByYear()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.DeliveryItems
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                group i by i.CreateTime.Year into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetMissonStatistic()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    List<DcStatistic> result = new List<DcStatistic>();
                    var query1 = from i in dc.Orders
                                 where i.State != PMSCommon.OrderState.作废.ToString()
                                 && i.PolicyType.Contains("VHP")
                                 select i;
                    result.Add(new DataContracts.DcStatistic { Key = "All", Value = query1.Count() });

                    var query2 = from i in dc.Orders
                                 where i.State == PMSCommon.OrderState.未完成.ToString()
                                 && i.PolicyType.Contains("VHP")
                                 select i;
                    result.Add(new DataContracts.DcStatistic { Key = "UnCompleted", Value = query2.Count() });

                    var query3 = from i in dc.Orders
                                 where i.State == PMSCommon.OrderState.完成.ToString()
                                 && i.PolicyType.Contains("VHP")
                                 select i;
                    result.Add(new DataContracts.DcStatistic { Key = "Completed", Value = query3.Count() });

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetOrderStatisticByCustomer(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.CustomerName into g
                                orderby g.Count() descending
                                select new DcStatistic { Key = g.Key, Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetOrderStatisticByMonth(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                 && i.CreateTime.Year == year
                                group i by i.CreateTime.Month into g
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetOrderStatisticBySeason(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by ((i.CreateTime.Month - 1) / 3 + 1) into g
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetOrderStatisticByYear()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                group i by i.CreateTime.Year into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatisticByDevice(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.VHPPlans
                                where i.State != PMSCommon.VHPPlanState.作废.ToString()
                                && i.PlanDate.Year == year
                                group i by i.VHPDeviceCode into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatisticByMonth(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.VHPPlans
                                where i.State != PMSCommon.VHPPlanState.作废.ToString()
                                && i.PlanDate.Year == year
                                group i by i.PlanDate.Month into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatisticBySeason(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.VHPPlans
                                where i.State != PMSCommon.VHPPlanState.作废.ToString()
                                &&i.CreateTime.Year==year
                                group i by ((i.PlanDate.Month - 1) / 3 + 1) into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatisticByYear()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.VHPPlans
                                where i.State != PMSCommon.VHPPlanState.作废.ToString()
                                group i by i.PlanDate.Year into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetProductStatisticByYear()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Products
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                group i by i.CreateTime.Year into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetProductStatisticByMonth(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Products
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.CreateTime.Month into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetProductStatisticBySeason(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Products
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by ((i.CreateTime.Month - 1) / 3 + 1) into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetProductStatisticByProductType(int year)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Products
                                where i.State != PMSCommon.CommonState.作废.ToString()
                                && i.CreateTime.Year == year
                                group i by i.ProductType into g
                                orderby g.Key
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
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