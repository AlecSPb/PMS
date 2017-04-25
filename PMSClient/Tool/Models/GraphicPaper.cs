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
                papers.Add(new GraphicPaper { Diameter=80,Weight=4});
                papers.Add(new GraphicPaper { Diameter = 80, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 105, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 128, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 205, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 206, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 233, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 255, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 306, Weight = 4 });
                papers.Add(new GraphicPaper { Diameter = 455, Weight = 4 });
                return papers;
            }
        }
    }

}
