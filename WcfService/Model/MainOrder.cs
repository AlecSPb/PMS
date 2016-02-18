using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class MainOrder
    {
        [DataMember]
        public Guid MainOrderId { get; set; }
        [DataMember]
        public DateTime? OrderDate { get; set; }
        [DataMember]
        public string  CustomerName { get; set; }
        [DataMember]
        public string ProductName { get; set; }


    }
}