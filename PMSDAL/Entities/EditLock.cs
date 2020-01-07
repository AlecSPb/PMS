using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 全局锁定辅助
    /// </summary>
    public class EditLock
    {
        public Guid ID { get; set; }
        public string Operator { get; set; }//锁定人
        public string LockTime { get; set; }//锁定时间
        public string ComputerInfo { get; set; }//执行锁定的计算机或者IP

        public string ItemName { get; set; }//锁定描述
        public string FingerPrint { get; set; }//锁定指纹-用于实际检查锁定情况 编辑内容的ID和创建时间字符串

    }
}
