using PMSDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSIRepository
{
    [ServiceContract]
    public interface IOrder:IRepositoryBase<PMSOrder>
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
        IList<PMSOrder> GetBySearchInPaging(int skip, int take, string compostionstd, string customer, int state);
    }
}
