using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Misson
{
    /// <summary>
    /// 计划
    /// 计划包含热压计划，维护计划，绑定计划，自定义计划等
    /// 这里存放所有计划的公共信息
    /// </summary>
    public class Plan
    {
        public Guid ID { get; set; }
        public DateTime PlanDate { get; set; }

        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
