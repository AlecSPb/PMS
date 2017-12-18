using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Tool.Models
{
    public class Element
    {
        public Element()
        {
            Number = 0;
            Name = "元素";
            At = 0;
        }
        public int Number { get; set; }
        public string Name { get; set; }
        public double At { get; set; }

    }
}
