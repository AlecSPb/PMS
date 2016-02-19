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

        [OperationContract]
        bool AddMaterialData(MaterialData materialData);
        [OperationContract]
        bool UpdateMaterialData(MaterialData materialData);
        [OperationContract]
        bool DeleteMaterialData(MaterialData materialData);

    }
}
