using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService
{
    /// <summary>
    /// 快照接口，服务内部使用不对外
    /// </summary>
    public interface ISnapShot<T>
    {
        void SaveHistory<T>(T model, string uid);
    }
}
