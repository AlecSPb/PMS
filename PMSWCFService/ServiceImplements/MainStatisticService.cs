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
        public List<DcStatistic> GetDeliveryByMonth()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetDeliveryBySeaon()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetDeliveryByYear()
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

        public List<DcStatistic> GetOrderStatisticByCustomer()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
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

        public List<DcStatistic> GetOrderStatisticByMonth()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                group i by new { i.CreateTime.Year, i.CreateTime.Month } into g
                                select new DcStatistic { Key = $"{g.Key.Year}-{g.Key.Month}", Value = g.Count() };
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetOrderStatisticBySeason()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.Orders
                                where i.State != PMSCommon.OrderState.作废.ToString()
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

        public List<DcStatistic> GetPlanStatisticByDevice()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticByMonth()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticBySeaon()
        {
            throw new NotImplementedException();
        }

        public List<DcStatistic> GetPlanStatisticByYear()
        {
            throw new NotImplementedException();
        }
    }
}