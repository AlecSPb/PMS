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
        public List<DcStatistic> GetDeliveryStatisticByMonth(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetDeliveryStatisticBySeaon(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetDeliveryStatisticByCountry(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetDeliveryStatisticByYear()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetMissonStatistic()
        {
            throw new NotImplementedException();
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
                                select new DcStatistic { Key = g.Key, Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
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
                                group i by  i.CreateTime.Month  into g
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
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
                                group i by i.CreateTime.Year into g
                                select new DcStatistic { Key = g.Key.ToString(), Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatisticByDevice(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticByMonth(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticBySeaon(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticByYear()
        {
            throw new NotImplementedException();
        }
    }
}