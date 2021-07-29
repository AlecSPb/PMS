using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAnalysis.Models
{
    public class DataModel
    {
        public DataModel()
        {
            Models = new List<AnalysisModel>();
        }
        public List<AnalysisModel> Models { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
