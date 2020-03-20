using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    /// <summary>
    /// 2020-3-21 全新设计的新服务
    /// </summary>
    public class NewService : INewService
    {
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

        public DateTime GetRecordTestLastUpdateTime(Guid id)
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
                    db.Entry(entity).State=System.Data.Entity.EntityState.Modified;
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



    }
}