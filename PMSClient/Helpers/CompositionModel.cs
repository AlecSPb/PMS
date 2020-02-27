using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public class CompositionModel : IComparable<CompositionModel>
    {
        public string Element { get; set; }
        public double Value { get; set; }

        public int CompareTo(CompositionModel other)
        {
            int compare = this.Value.CompareTo(other.Value);
            if (compare == 0)
                return this.Element.CompareTo(other.Element);
            return -compare;//这里有一个负号，用于降序排列
        }
    }
}
