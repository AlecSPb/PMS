using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface IProductProcessDataService
    {
        /// <summary>
        /// 查找所有产品工艺数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ProductProcessData> GetAllProductProcessData();
        /// <summary>
        /// 按照Id查找产品工艺数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ProductProcessData GetProductProcessDataById(Guid id);
        [OperationContract]
        bool AddProductProcessData(ProductProcessData data);
        [OperationContract]
        bool UpdateProductProcessData(ProductProcessData data);
        [OperationContract]
        bool DeleteProductProcessData(ProductProcessData data);

    }
}
