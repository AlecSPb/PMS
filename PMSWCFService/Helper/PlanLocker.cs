using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSDAL;
using System.Data.Entity;

namespace PMSWCFService.Helper
{
    /// <summary>
    /// 执行完之后当天就不再执行
    /// </summary>
    public class PlanLocker
    {
        private DateTime lockTime;
        //存储执行后的日期
        private DateTime nextRunTime;
        public PlanLocker()
        {
            nextRunTime = DateTime.Now.AddDays(-1);
        }
        /// <summary>
        /// 锁定所有计划
        /// </summary>
        public void LockAllTodaysPlan()
        {
            //获取当天锁定时间
            lockTime = DateTime.Today.AddHours(12);
            //判定是否到了执行时间 并且 是否到了锁定时间
            if (nextRunTime < DateTime.Now && lockTime < DateTime.Now)
            {
                LockPlan();
            }

        }
        private void LockPlan()
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

                    //锁定成功后，将下次运行的时间往后延迟1h
                    nextRunTime = DateTime.Now.AddHours(1);
                }
            }
        }
    }
}