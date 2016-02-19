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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddProductProcessData(ProductProcessData data);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProductProcessData(ProductProcessData data);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteProductProcessData(ProductProcessData data);

    }
}
