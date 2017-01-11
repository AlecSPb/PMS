using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using System.ServiceModel;
using System.Linq.Expressions;

namespace PMSWCFService
{
    /// <summary>
    /// 通用的泛型类接口用于WCF服务
    /// </summary>
    /// <typeparam name="CommonModel"></typeparam>
    [ServiceContract(Namespace ="http://www.newlifechou.com")]
    public interface IServiceBase<CommonModel>
    {
        [OperationContract]
        List<CommonModel> GetAll();

        [OperationContract]
        CommonModel FindById(Guid id);

        [OperationContract]
        int GetCount();

        [OperationContract]
        int Add(CommonModel model);

        [OperationContract]
        int Update(CommonModel model);

        [OperationContract]
        int Delete(Guid id);
    }
}
