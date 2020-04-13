using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService.ServiceContracts
{
    /// <summary>
    /// 用于过程阶段查询追踪
    /// </summary>
    [ServiceContract]
    public interface IRecordSearchService
    {
        [OperationContract]
        List<DcRecordMilling> GetRecordMillingBySearchLot(string searchLot);
        [OperationContract]
        List<DcRecordDeMold> GetRecordDeMoldBySearchLot(string searchLot);
        [OperationContract]
        List<DcRecordMachine> GetRecordMachineBySearchLot(string searchLot);
        [OperationContract]
        List<DcRecordTest> GetRecordTestBySearchLot(string searchLot);
        [OperationContract]
        List<DcRecordBonding> GetRecordBondingBySearchLot(string searchLot);
        [OperationContract]
        List<DcProduct> GetProductBySearchLot(string searchLot);
        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemBySearchLot(string searchLot);

    }
}
