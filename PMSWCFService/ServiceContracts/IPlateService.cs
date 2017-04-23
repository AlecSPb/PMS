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
    public interface IPlateService
    {

        [OperationContract]
        List<DcPlate> GetPlates(int skip, int take, string platelot, string supplier);

        [OperationContract]
        int GetPlateCount(string platelot, string supplier);
        [OperationContract]
        int AddPlate(DcPlate model);
        [OperationContract]
        int UpdatePlate(DcPlate model);
        [OperationContract]
        int AddPlateByUID(DcPlate model,string uid);
        [OperationContract]
        int UpdatePlateByUID(DcPlate model,string uid);

        [OperationContract]
        int DeletePlate(Guid id);

    }
}
