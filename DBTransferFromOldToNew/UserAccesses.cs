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
    
    public partial class UserAccesses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccesses()
        {
            this.UserRoles = new HashSet<UserRoles>();
        }
    
        public System.Guid ID { get; set; }
        public string AccessName { get; set; }
        public string AccessCode { get; set; }
        public string State { get; set; }
        public string ExtraInformation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
