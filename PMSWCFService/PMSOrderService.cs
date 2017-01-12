using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public partial class PMSService : IOrderService
    {
        public int AddOrder(DcOrder order)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcOrder, PMSOrder>());
                var mapper = config.CreateMapper();
                var pmsOrder = mapper.Map<PMSOrder>(order);
                dc.Orders.Add(pmsOrder);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int DeleteOrder(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var pmsOrder = dc.Orders.Find(id);
                if (pmsOrder != null)
                {
                    pmsOrder.State = (int)ModelState.Deleted;
                    dc.SaveChanges();
                }
                return result;
            }
        }

        public List<DcOrder> GetAllOrderInPage(int skip, int take)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, DcOrder>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<PMSOrder>, List<DcOrder>>(
                    dc.Orders.OrderByDescending(o => o.CreateTime).Skip(skip).Take(take).ToList());
                return result;
            }
        }

        public List<DcOrder> GetOrderBySearchInPage(int skip, int take, string customer, string compositionstd)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, DcOrder>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<PMSOrder>, List<DcOrder>>(
                    dc.Orders.Where(o => o.CustomerName.StartsWith(customer)  && o.CompositionStandard.Contains(compositionstd))
                    .OrderByDescending(o => o.CreateTime).Skip(skip).Take(take).ToList());
                return result;
            }
        }

        public int GetOrderCountBySearch(string customer, string compositionstd)
        {
            using (var dc = new PMSDbContext())
            {
                return dc.Orders.Where(o => o.CustomerName.StartsWith(customer) && o.CompositionStandard.Contains(compositionstd)).Count();
            }
        }

        public int UpdateOrder(DcOrder order)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcOrder, PMSOrder>());
                var mapper = config.CreateMapper();
                PMSOrder pmsOrder = mapper.Map<PMSOrder>(order);
                dc.Entry(pmsOrder).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return result;
            }
        }
    }
}