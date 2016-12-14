using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Misson
{
    /// <summary>
    /// 任务
    /// 相当于没有客户和价格等敏感信息的其他订单数据，对车间的生产任务安排
    /// 生产计算各种原料需求的重要依据
    /// 可以自行添加任务上去
    /// </summary>
    public class Misson
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }


    }
}
