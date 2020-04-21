using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcAnlysis
    {
        public DcAnlysis()
        {
            Group = Key = Value = Remark = "";
        }
        /// <summary>
        /// 分组
        /// </summary>
        [DataMember]
        public string Group { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [DataMember]
        public string Key { get; set; }
        /// <summary>
        /// 值-字符串形式
        /// </summary>
        [DataMember]
        public string Value { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}