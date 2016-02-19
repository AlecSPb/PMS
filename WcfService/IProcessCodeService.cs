using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IProcessCodeService”。
    [ServiceContract]
    public interface IProcessCodeService
    {
        /// <summary>
        /// 得到所有的工艺代码
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ProcessCode> GetAllProcessCodes();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddProcessCode(ProcessCode code);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProcessCode(ProcessCode code);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteProcessCode(ProcessCode code);

    }
}
