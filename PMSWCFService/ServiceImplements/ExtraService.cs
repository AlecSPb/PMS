using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public class ExtraService : ICheckListService, IItemDebitService
    {
        public int AddCheckList(DcCheckList model, string uid)
        {
            throw new NotImplementedException();
        }

        public int AddItemDebit(DcItemDebit model, string uid)
        {
            throw new NotImplementedException();
        }

        public int DeleteCheckList(Guid id, string uid)
        {
            throw new NotImplementedException();
        }

        public int DeleteItemDebit(Guid id, string uid)
        {
            throw new NotImplementedException();
        }

        public List<DcCheckList> GetCheckList(int s, int t, string title)
        {
            throw new NotImplementedException();
        }

        public int GetCheckListCount(string title)
        {
            throw new NotImplementedException();
        }

        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creaditor)
        {
            throw new NotImplementedException();
        }

        public int GetItemDebitCount(string itemType, string itemName, string creaditor)
        {
            throw new NotImplementedException();
        }

        public int UpdateCheckList(DcCheckList model, string uid)
        {
            throw new NotImplementedException();
        }

        public int UpdateItemDebit(DcItemDebit model, string uid)
        {
            throw new NotImplementedException();
        }
    }
}