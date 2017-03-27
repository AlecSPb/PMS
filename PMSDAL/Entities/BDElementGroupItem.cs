using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class BDElementGroupItem
    {
        public Guid Id { get; set; }
        public Guid GroupElementID { get; set; }
        public string Name { get; set; }
        public int GroupNumber { get; set; }
        public double MolWeight { get; set; }
        public double At { get; set; }
    }
}
