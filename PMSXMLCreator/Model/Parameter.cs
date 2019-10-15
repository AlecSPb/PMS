using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator
{
    /// <summary>
    /// COA Parameter
    /// </summary>
    public class Parameter
    {

        public Parameter()
        {
            Measurements = new List<Measurement>();
        }
        public string LotId { get; set; }
        //TestCode,匹配SPEED
        public string ShortName { get; set; }
        //可选的
        public string SourceComponent { get; set; }
        //ParameterName，可选的
        public string Characteristic { get; set; }
        //匹配SPEED
        public string UnitOfMeasure { get; set; }

        public List<Measurement> Measurements { get; set; }
    }
}
