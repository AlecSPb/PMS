using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PMSDAL;
using PMSWCFService.Contracts;
using PMSWCFService.Models;

namespace PMSWCFService
{
    public class PMSOrderService : IPMSOrderSerivce
    {
        public int Add(PMSOrderDc model)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public PMSOrderDc FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<PMSOrderDc> GetAll()
        {
            using (var dc=new PMSDbContext())
            {
                //return dc.Orders.ToList<>
            }
        }

        public IList<PMSOrderDc> GetAllInPaging(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IList<PMSOrderDc> GetAllInPaging(Expression<Func<PMSOrderDc, bool>> condition, int skip, int take)
        {
            using (var dc=new PMSDbContext())
            {

            }
        }

        public IList<PMSOrderDc> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state)
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            using (var dc=new PMSDbContext())
            {
                return dc.Orders.Count();
            }
        }

        public int Update(PMSOrderDc model)
        {
            throw new NotImplementedException();
        }
    }
}
