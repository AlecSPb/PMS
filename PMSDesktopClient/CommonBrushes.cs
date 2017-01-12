using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PMSDesktopClient
{
    public class CommonBrushes
    {
        public CommonBrushes()
        {
            StopBrush = new SolidColorBrush(Colors.LightBlue);
            UnCompletedBrush = new SolidColorBrush(Colors.White);
            CompletedBrush = new SolidColorBrush(Colors.LightSalmon);
        }

        public Brush StopBrush { get; set; }
        public Brush UnCompletedBrush { get; set; }
        public Brush CompletedBrush { get; set; }
    }
}
