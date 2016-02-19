using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MaterialDataService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MaterialDataService.svc 或 MaterialDataService.svc.cs，然后开始调试。
    public class MaterialDataService : IMaterialDataService
    {
        public bool AddMaterialData(MaterialData materialData)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMaterialData(MaterialData materialData)
        {
            throw new NotImplementedException();
        }

        public List<MaterialData> GetAllMaterialData()
        {
            throw new NotImplementedException();
        }

        public bool UpdateMaterialData(MaterialData materialData)
        {
            throw new NotImplementedException();
        }
    }
}
