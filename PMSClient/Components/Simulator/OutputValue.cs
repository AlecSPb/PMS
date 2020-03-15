using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Simulator
{
    /// <summary>
    /// 成分组
    /// </summary>
    public class OutputValue
    {
        public OutputValue()
        {
            RowNo="";
            Values = new List<double>();
        }
        public string RowNo { get; set; }
        public List<double> Values { get; set; }
        public List<double> Average { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Values)
            {
                sb.Append(item);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
