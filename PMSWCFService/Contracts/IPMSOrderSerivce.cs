using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.Models;

namespace PMSWCFService.Contracts
{
    [ServiceContract(Namespace = "http://www.newlifechou.com")]
    public interface IPMSOrderSerivce:IServiceBase<PMSOrderDc>
    {
        /// <summary>
        /// Get PMSOrder By Search and Paging it.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="compostionstd"></param>
        /// <param name="customer"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        IList<PMSOrderDc> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state);
    }
}
