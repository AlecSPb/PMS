using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.XMLGenerator
{
    public class ParameterDict
    {
        private Dictionary<string, string> parameters;
        public ParameterDict()
        {
            parameters = new Dictionary<string, string>();
            Init();
        }

        private void Init()
        {
            parameters.Add("Selenium", "ZSE");
            parameters.Add("Arsenic", "ZAS");
            parameters.Add("Germanium", "ZGE");
            parameters.Add("Silicon", "ZSI");
            parameters.Add("Target Blank OD", "TAR_BOD");
            parameters.Add("Target Blank Thickness", "TAR_BTH");
            parameters.Add("Density", "DEN");
            parameters.Add("Weight", "Weight");
            parameters.Add("Target Blank Flatness", "TAR_BFLA");
            parameters.Add("Target Blank Parallism", "TAR_BPAR");
            parameters.Add("Target Blank Surface Roughness", "TAR_BSUR");
            parameters.Add("BP Diameter", "BP_DIA");
            parameters.Add("BP Flange Thickness", "BP_FTHK");
            parameters.Add("BP Sidewall Height", "BP_SH");
            parameters.Add("BP Pocket Depth", "BP_PDE");
            parameters.Add("BP Target Blank Step", "BP_TBS");
            parameters.Add("BP Side Wall Dia", "BP_SDIA");
            parameters.Add("BP Pocket Dia", "BP_PDIA");
            parameters.Add("BP Bonded Surface Flatness", "BP_BSF");
            parameters.Add("Aluminum", "ZAL");
            parameters.Add("Bismuth", "ZBI");
            parameters.Add("Chromium", "ZCR");
            parameters.Add("Copper", "ZCU");
            parameters.Add("Iron", "IA");
            parameters.Add("Gallium", "GH");
            parameters.Add("Magnesium", "ZMG");
            parameters.Add("Manganese", "ZMN");
            parameters.Add("Nickel", "ZNI");
            parameters.Add("Lead", "ZPB");
            parameters.Add("Scandium", "SCA");
            parameters.Add("Sulfur", "ZS");
            parameters.Add("Titanium", "ZTI");
            parameters.Add("Tungsten", "TUNG");
            parameters.Add("Yttrium", "YTR");
            parameters.Add("Total Metallic Impurities", "TMI");
            parameters.Add("Oxygen", "ZO");
            parameters.Add("Nitrogen", "ZN");
        }


        public string GetShortName(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            else
            {
                return "No Such Parameter";
            }
        }

    }
}
