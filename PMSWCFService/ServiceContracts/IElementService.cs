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
    public interface IElementService
    {
        [OperationContract]
        List<DcBDElement> GetElements();
        [OperationContract]
        int AddElement(DcBDElement model);
        [OperationContract]
        int UpdateElement(DcBDElement model);
        [OperationContract]
        int DeleteElement(Guid id);

        [OperationContract]
        List<DcBDElementGroup> GetElementGroup();

        [OperationContract]
        List<DcBDElementGroupItem> GetElementGroupItem(Guid id);

        [OperationContract]
        int AddElementGroupAndItems(string groupName,List<DcBDElementGroupItem> elements);

    }
}
