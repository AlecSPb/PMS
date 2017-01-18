using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 热压工艺表
    /// </summary>
    [DataContract]
    public class DcBDVHPProcess
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string CodeName { get; set; }
        [DataMember]
        public string CodeMeaning { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
