using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaintenancePlan
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }//Run,Stop,Paused


        [DataMember]
        public string VHPMachineCode { get; set; }//计划
        [DataMember]
        public string PlanItem { get; set; }//计划
        [DataMember]
        public string PlanType { get; set; }//计划类型，一级保养，二级保养，三级保养,
        [DataMember]
        public string PlanInterval { get; set; }//计划周期
        [DataMember]
        public string Content { get; set; }//计划内容

        [DataMember]
        public string Standard { get; set; }//完成标准
        [DataMember]
        public string CommonFailure { get; set; }//常见问题
        [DataMember]
        public string ProcessMethod { get; set; }//处理方法

        [DataMember]
        public string Remark { get; set; }//备注

    }
}
