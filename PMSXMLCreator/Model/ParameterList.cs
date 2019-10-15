using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Model
{
    public class ParameterList
    {
        public ParameterList()
        {
            Parameters = new List<Parameter>();

            var ZSE = new Parameter() { Characteristic = "Selenium", ShortName = "ZSE", UnitOfMeasure = "%" };
            Parameters.Add(ZSE);




        }
        public List<Parameter> Parameters { get; set; }
    }
}
