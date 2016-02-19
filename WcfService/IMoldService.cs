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
    public interface IMoldService
    {
        /// <summary>
        /// 得到所有的模具
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Mold> GetAllMolds();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mold"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMold(Mold mold);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="mold"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMold(Mold mold);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="mold"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMold(Mold mold);

    }
}
