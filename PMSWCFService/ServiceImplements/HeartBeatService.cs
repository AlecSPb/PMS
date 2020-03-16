using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using System.Data.Entity;

namespace PMSWCFService
{
    public class HeartBeatService : IHeartBeatSerive
    {
        public string Beat()
        {
            return "ok";
        }

        public int GetOperationCallTimes()
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    return db.LogInformations.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }

        public int GetOperationCallTimesToday()
        {
            try
            {
                //今天0时0分0秒：
                DateTime start = Convert.ToDateTime(DateTime.Now.ToString("D").ToString());

                //今天23时59分59秒：
                DateTime end = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D")).AddSeconds(-1);
                using (var db = new PMSDbContext())
                {
                    var query = from i in db.LogInformations
                                where i.CreateTime > start
                                && i.CreateTime < end
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }

        public int GetOperationCallTimesYesterday()
        {
            try
            {
                //昨天0时0分0秒：
                DateTime start = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("D").ToString());

                //昨天23时59分59秒：
                DateTime end = Convert.ToDateTime(DateTime.Now.ToString("D")).AddSeconds(-1);
                using (var db = new PMSDbContext())
                {
                    var query = from i in db.LogInformations
                                where i.CreateTime > start
                                && i.CreateTime < end
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }
    }
}