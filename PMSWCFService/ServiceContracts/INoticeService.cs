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
    public interface INoticeService
    {
        DcNotice GetCurrentNotice();
        int UpdateNotice(DcNotice model);
    }
}
