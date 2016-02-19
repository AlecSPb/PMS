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
    public interface IMaterialDataService
    {
        /// <summary>
        /// 获取所有的材料数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MaterialData> GetAllMaterialData();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="materialData"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMaterialData(MaterialData materialData);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="materialData"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMaterialData(MaterialData materialData);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="materialData"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMaterialData(MaterialData materialData);

    }
}
