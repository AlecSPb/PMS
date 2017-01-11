using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.Models;

namespace PMSWCFService
{
    [ServiceContract(Namespace = "http://www.newlifechou.com")]
    public interface IOrderSerivce:IServiceBase<OrderDc>
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
        List<OrderDc> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state);
        [OperationContract]
        List<OrderDc> GetAllInPaging(int skip, int take, int state);
    }
}
