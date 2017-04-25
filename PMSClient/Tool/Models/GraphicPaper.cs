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
    public class GraphicPaper
    {
        public double Diameter { get; set; }
        public double Weight { get; set; }
    }

    public static   class GraphicPaperCollections
    {
        public static List<GraphicPaper> Papers
        {
           get
            {
                var papers = new List<GraphicPaper>();
                papers.Add(new GraphicPaper { Diameter=0,Weight=0});
                papers.Add(new GraphicPaper { Diameter = 80, Weight = 4.4 });
                papers.Add(new GraphicPaper { Diameter = 105, Weight = 6.4 });
                papers.Add(new GraphicPaper { Diameter = 128, Weight = 11 });
                papers.Add(new GraphicPaper { Diameter = 155, Weight = 14.8 });
                papers.Add(new GraphicPaper { Diameter = 160, Weight = 19 });
                papers.Add(new GraphicPaper { Diameter = 206, Weight = 24.9 });
                papers.Add(new GraphicPaper { Diameter = 233, Weight =33 });
                papers.Add(new GraphicPaper { Diameter = 255, Weight = 43 });
                papers.Add(new GraphicPaper { Diameter = 306, Weight = 65.4 });
                papers.Add(new GraphicPaper { Diameter = 319, Weight = 70.2 });
                papers.Add(new GraphicPaper { Diameter = 336, Weight = 70.2 });
                papers.Add(new GraphicPaper { Diameter = 450, Weight = 77 });
                return papers;
            }
        }
    }

}
