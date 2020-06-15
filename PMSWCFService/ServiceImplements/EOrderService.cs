using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    public class EOrderService : IEOrderService
    {
        public bool CheckEOrderGuid(string guidid)
        {
            XS.RunLog();
            try
            {
                using (var db = new PMSDbContext())
                {
                    Guid id = Guid.Parse(guidid);
                    var query = from o in db.Orders
                                where o.ID == id
                                select o;
                    return query.Count() > 0;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}