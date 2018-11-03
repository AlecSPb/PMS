using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace TargetCutterSimulator
{
    public enum ShapeType
    {
        Rectangle,
        Ellipse
    }
    public class DrawShape
    {
        public Shape Shape { get; set; }
        public ShapeType CurrentType { get; set; }
    }
}
