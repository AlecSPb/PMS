using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator_Micron.Model
{
    public class Micon_COA
    {
        public Micon_COA()
        {
            Header = new List<BasicInfoField>();
            InspectionItems = new List<InspectionItem>();
        }

        public string ProductId { get; set; }
        public string COANumber { get; set; }


        public List<BasicInfoField> Header { get; set; }

        //Content/UnitId
        public string UnitIdValue { get; set; }
        public List<InspectionItem> InspectionItems { get; set; }
    }
}
