//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBTransferFromOldToNew
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeliveryItems
    {
        public System.Guid ID { get; set; }
        public string ProductType { get; set; }
        public string DetailRecord { get; set; }
        public string Position { get; set; }
        public string Remark { get; set; }
        public System.Guid DeliveryID { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Weight { get; set; }
        public string State { get; set; }
        public string Abbr { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int PackNumber { get; set; }
    
        public virtual Deliveries Deliveries { get; set; }
    }
}
