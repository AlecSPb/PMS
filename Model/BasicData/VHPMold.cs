using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VHPMold
    {
        public Guid ID { get; set; }
        public string ModelType { get; set; }
        public string MoldDetails { get; set; }
        public double InnerDiameter { get; set; }
        public double ModelHeight { get; set; }
        public int CurrentState { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }

}
