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
            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public OrderDc FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<OrderDc> GetAll()
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSDAL.PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<IList<PMSDAL.PMSOrder>, IList<OrderDc>>(dc.Orders.ToList());
                return result;
            }
        }

        public IList<OrderDc> GetAllInPaging(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IList<OrderDc> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSDAL.PMSOrder, OrderDc>());
                var mapper = config.CreateMapper();
                var result = mapper.Map<IList<PMSDAL.PMSOrder>, IList<OrderDc>>(
                    dc.Orders.Where(o=>o.CustomerName.Contains(customer)&&o.State==state&&o.CompositionStandard==compostionstd).Skip(skip).Take(take).ToList());
                return result;
            }
        }

        public int GetRecordCount()
        {
            using (var dc=new PMSDbContext())
            {
                return dc.Orders.Count();
            }
        }

        public int Update(OrderDc model)
        {
            throw new NotImplementedException();
        }
    }
}
