using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PMSDAL;
using PMSWCFService.Models;
using AutoMapper;

namespace PMSWCFService
{
    public class OrderService : IOrderSerivce
    {
        public int Add(OrderDc model)
        {
            using (var dc=new PMSDbContext())
            {
                int result=0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDc, PMSOrder>());
                var mapper = config.CreateMapper();
                var pmsOrder = mapper.Map<PMSOrder>(model);
                dc.Orders.Add(pmsOrder);
                result = dc.SaveChanges();
                return result;
            }
        }

        public int Delete(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var pmsOrder = dc.Orders.Find(id);
                if (pmsOrder!=null)
                {
                    pmsOrder.State = (int)ModelState.Deleted;
                    dc.SaveChanges();
                }
                return result;
            }
        }

        public int Update(OrderDc model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDc, PMSOrder>());
                var mapper = config.CreateMapper();
                PMSOrder pmsOrder = mapper.Map<PMSOrder>(model);
                dc.Entry(pmsOrder).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return result;
            }
        }

        public OrderDc FindById(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var pmsOrder = dc.Orders.Find(id);
                var orderDc = mapper.Map<OrderDc>(pmsOrder);
                return orderDc;
            }
        }

        public List<OrderDc> GetAll()
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<PMSOrder>, List<OrderDc>>(dc.Orders.ToList());
                return result;
            }
        }

        public List<OrderDc> GetAllInPaging(int skip, int take,int state)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<PMSOrder>, List<OrderDc>>(
                    dc.Orders.Where(o=>o.State==state).OrderByDescending(o=>o.CreateTime).Skip(skip).Take(take).ToList());
                return result;
            }
        }

        public List<OrderDc> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<List<PMSOrder>, List<OrderDc>>(
                    dc.Orders.Where(o => o.CustomerName.StartsWith(customer) && o.State == state && o.CompositionStandard.Contains(compostionstd))
                    .OrderByDescending(o => o.CreateTime).Skip(skip).Take(take).ToList());
                return result;
            }
        }

        public int GetCount()
        {
            using (var dc = new PMSDbContext())
            {
                return dc.Orders.Count();
            }
        }
    }
}
