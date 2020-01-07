using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 全局锁定辅助
    /// </summary>
    [DataContract]
    public class DcEditLock
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Operator { get; set; }//锁定人
        [DataMember]
        public string LockTime { get; set; }//锁定时间
        [DataMember]
        public string ComputerInfo { get; set; }//执行锁定的计算机或者IP

        [DataMember]
        public string ItemName { get; set; }//锁定描述
        [DataMember]
        public string FingerPrint { get; set; }//锁定指纹-用于实际检查锁定情况 编辑内容的ID和创建时间字符串

    }
}