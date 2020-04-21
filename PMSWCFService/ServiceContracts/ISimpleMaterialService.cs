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
    public interface ISimpleMaterialService
    {
        [OperationContract]
        List<DcSimpleMaterial> GetSimpleMaterial(int s, int t, string composition);

        [OperationContract]
        int GetSimpleMaterialCount(string composition);

        [OperationContract]
        DcSimpleMaterial GetSimpleMaterialByComposition(string composition);

        [OperationContract]
        void AddSimpleMaterial(DcSimpleMaterial model);

        [OperationContract]
        void UpdateSimpleMaterial(DcSimpleMaterial model);

    }
}
