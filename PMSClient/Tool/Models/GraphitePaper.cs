using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Tool.Models
{
    /// <summary>
    /// 石墨纸直径和重量 双层
    /// </summary>
    public class GraphitePaper
    {
        public GraphitePaper()
        {
            Diameter = 0;
            Weight = 0;
            Thickness = 0.5;
        }
        public double Diameter { get; set; }
        public double Weight { get; set; }
        public double Thickness { get; set; }
    }

    public static   class GraphicPaperCollections
    {
        public static List<GraphitePaper> Papers
        {
           get
            {
                var papers = new List<GraphitePaper>();
                papers.Add(new GraphitePaper { Diameter=0,Weight=0,Thickness=0});
                papers.Add(new GraphitePaper { Diameter = 80, Weight = 4.4 });
                papers.Add(new GraphitePaper { Diameter = 105, Weight = 6.4 });
                papers.Add(new GraphitePaper { Diameter = 128, Weight = 11 });
                papers.Add(new GraphitePaper { Diameter = 155, Weight = 14.8 });
                papers.Add(new GraphitePaper { Diameter = 168, Weight = 19 });
                papers.Add(new GraphitePaper { Diameter = 206, Weight = 24.9 });
                papers.Add(new GraphitePaper { Diameter = 233, Weight =33 });
                papers.Add(new GraphitePaper { Diameter = 255, Weight = 38.85 });
                papers.Add(new GraphitePaper { Diameter = 306, Weight = 55.9 });
                papers.Add(new GraphitePaper { Diameter = 319, Weight = 60.8 });
                papers.Add(new GraphitePaper { Diameter = 336, Weight = 67.5});
                papers.Add(new GraphitePaper { Diameter = 396, Weight = 90.5 });
                papers.Add(new GraphitePaper { Diameter = 450, Weight = 121 });
                return papers;
            }
        }
    }

}
