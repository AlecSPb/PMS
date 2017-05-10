using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IOutSourceService
    {
        [OperationContract]
        List<DcOutSource> GetOutSources(int s, int t, string ordername, string supplier);
        [OperationContract]
        int GetOutSourcesCount(string ordername, string supplier);

        [OperationContract]
        int AddOutSource(DcOutSource model,string uid);
        [OperationContract]
        int UpdateOutSource(DcOutSource model,string uid);
        [OperationContract]
        int DeleteOutSource(Guid id,string uid);
    }
}
