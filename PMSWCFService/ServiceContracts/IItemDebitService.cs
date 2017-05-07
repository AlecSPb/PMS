using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IItemDebitService
    {
        [OperationContract]
        List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creaditor);
        [OperationContract]
        int GetItemDebitCount(string itemType, string itemName, string creaditor);

        [OperationContract]
        int AddItemDebit(DcItemDebit model, string uid);
        [OperationContract]
        int UpdateItemDebit(DcItemDebit model, string uid);
        [OperationContract]
        int DeleteItemDebit(Guid id, string uid);
    }
}
