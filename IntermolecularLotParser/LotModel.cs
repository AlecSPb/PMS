using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermolecularLotParser
{
    public class LotModel
    {
        public LotModel()
        {
            Composition = "";
            Group = "";
            Items = new List<string>();
        }
        public string Composition { get; set; }
        public string Group { get; set; }
        public List<string> Items { get; set; }
    }
}
