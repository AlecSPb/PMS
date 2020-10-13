using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator_Micron.Model
{
    public class InspectionItem
    {
        public InspectionItem()
        {
            ResultItems = new List<ResultItem>();
        }
        public string ItemName { get; set; }
        public List<ResultItem> ResultItems { get; set; }
    }
}
