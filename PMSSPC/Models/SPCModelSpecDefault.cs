using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSSPC.Models
{
    public class SPCModelSpecDefault
    {
        public SPCModel Build(string composition, string spctype)
        {
            SPCModel model = new SPCModel();

            bool isCIGS = composition.Contains("Cu") && composition.Contains("In") && composition.Contains("Ga") && composition.Contains("Se");
            if (isCIGS && spctype == "Density")
            {
                model.SPCType = spctype;
                model.USL = 5.9;
                model.LSL = 5.4;
                model.SL = 5.75;
                return model;

            }
;
            if (composition == "In40Se60" && spctype == "Density")
            {
                model.SPCType = spctype;
                model.USL = 5.7;
                model.LSL = 5.3;
                model.SL = 5.4;
                return model;
            }

            if (composition == "In40S60" && spctype == "Density")
            {
                model.SPCType = spctype;
                model.USL = 4.7;
                model.LSL = 4.2;
                model.SL = 4.4;
                return model;
            }


            if (composition == "Cu25Ga25Se50" && spctype == "Density")
            {
                model.SPCType = spctype;
                model.USL = 5.8;
                model.LSL = 5.5;
                model.SL = 5.3;
                return model;
            }

            return model;
        }
    }
}
