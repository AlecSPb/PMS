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
    }
}