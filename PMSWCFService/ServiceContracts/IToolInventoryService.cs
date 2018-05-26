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
    public interface IToolInventoryService
    {
        [OperationContract]
        IList<DcToolFilling> GetToolFillings(int s, int t, string elementA, string elementB);
        [OperationContract]
        int GetToolFillingsCount(string elementA, string elementB);

        [OperationContract]
        int AddToolFilling(DcToolFilling model);
        [OperationContract]
        int UpdateToolFilling(DcToolFilling model);
        [OperationContract]
        int DeleteToolFilling(Guid id);



        [OperationContract]
        IList<DcToolMilling> GetToolMillings(int s,int t,string elementA, string elementB);
        [OperationContract]
        int GetToolMillingsCount(string elementA, string elementB);
        [OperationContract]
        int AddToolMilling(DcToolMilling model);
        [OperationContract]
        int UpdateToolMilling(DcToolMilling model);
        [OperationContract]
        int DeleteToolMilling(Guid id);
    }
}
