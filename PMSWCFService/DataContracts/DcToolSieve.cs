using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcToolSieve
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }

        [DataMember]
        public string State { get; set; }


        [DataMember]
        public string SearchID { get; set; }//给人看的编号  S-1
        [DataMember]
        public string Manufacture { get; set; }//制造商
        [DataMember]
        public string Specification { get; set; }//规格
        [DataMember]
        public string MaterialGroup { get; set; }//材料组
        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }//开始使用时间
        [DataMember]
        public DateTime StopTime { get; set; }//结束使用时间
    }
}
