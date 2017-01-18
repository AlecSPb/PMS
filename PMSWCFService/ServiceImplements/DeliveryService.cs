using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService.ServiceImplements
{
    public partial class PMSService : IRecordDeliveryService
    {
        public int AddRecordDelivery(DcRecordDelivery model)
        {
            throw new NotImplementedException();
        }

        public int AddRecordDeliveryItem(DcRecordDeliveryItem model)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordDelivery(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordDeliveryItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordDelivery> GetDeliveryBySearchInPage(int skip, int take, string searchDeliveryID)
        {
            throw new NotImplementedException();
        }

        public int UpdateReocrdDelivery(DcRecordDelivery model)
        {
            throw new NotImplementedException();
        }

        public int UpdateReocrdDeliveryItem(DcRecordDeliveryItem model)
        {
            throw new NotImplementedException();
        }
    }
}