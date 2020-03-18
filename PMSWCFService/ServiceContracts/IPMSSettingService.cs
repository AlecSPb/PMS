using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IPMSSettingService
    {
        [OperationContract]
        string GetValueByKey(string key);
        [OperationContract]
        void AddSettings(string key,string value);
        [OperationContract]
        void UpdateSettings(string key, string newValue);
    }
}
