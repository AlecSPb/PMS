using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Service
{
    public class ParameterDict
    {
        private Dictionary<string, BasicData> parameters;
        public ParameterDict()
        {
            parameters = new Dictionary<string, BasicData>();
            Init();
        }

        private void Init()
        {
            parameters.Add("Selenium", new BasicData { ShortName = "ZSE", LCL = 0.5025, UCL = 0.5133 });
            parameters.Add("Arsenic", new BasicData { ShortName = "ZAS", LCL = 0.3012, UCL = 0.3108 });
            parameters.Add("Germanium", new BasicData { ShortName = "ZGE", LCL = 0.1229, UCL = 0.1316 });
            parameters.Add("Silicon", new BasicData { ShortName = "ZSI", LCL = 0.0537, UCL = 0.0621 });
            parameters.Add("Target Blank OD", new BasicData { ShortName = "TAR_BOD", LCL = 439.74, UCL = 440.17 });
            parameters.Add("Target Blank Thickness", new BasicData { ShortName = "TAR_BTHK", LCL = 7.701, UCL = 7.899 });
            parameters.Add("Target Blank 2mm Top Rad", new BasicData { ShortName = "TAR_BTOP", LCL = 0, UCL = 100 });
            parameters.Add("BP Diameter", new BasicData { ShortName = "BP_DIA", LCL = 573.5, UCL = 576.5 });
            parameters.Add("BP Flange Thickness", new BasicData { ShortName = "BP_FTHK", LCL = 15.5, UCL = 16.5 });
            parameters.Add("BP Sidewall Height", new BasicData { ShortName = "BP_SH", LCL = 81, UCL = 84 });
            parameters.Add("BP Pocket Depth", new BasicData { ShortName = "BP_PDE", LCL = 75.0, UCL = 78.0 });
            parameters.Add("BP Target Blank Step", new BasicData { ShortName = "BP_TBS", LCL = 0.50, UCL = 1.50 });
            parameters.Add("BP Side Wall Dia", new BasicData { ShortName = "BP_SDIA", LCL = 484.5, UCL = 487.5 });
            parameters.Add("BP Pocket Dia", new BasicData { ShortName = "BP_PDIA", LCL = 456.5, UCL = 459.5 });
            parameters.Add("BP Bonded Surface Flatness", new BasicData { ShortName = "BP_BSF", LCL = 0, UCL = 1.0 });
            parameters.Add("Density", new BasicData { ShortName = "DEN", LCL = 4.3011, UCL = 4.3937 });
            parameters.Add("Weight", new BasicData { ShortName = "Weight", LCL = 5077.2, UCL = 5232.3 });
            parameters.Add("Target Blank Flatness", new BasicData { ShortName = "TAR_BFLA", LCL = 0, UCL = 1.0 });
            parameters.Add("Target Blank Parallism", new BasicData { ShortName = "TAR_BPAR", LCL = 0, UCL = 1.0 });
            parameters.Add("Target Blank Surface Roughness", new BasicData { ShortName = "TAR_BSUR", LCL = 0, UCL = 1.0 });

            parameters.Add("BP Backside Flatness", new BasicData { ShortName = "BP_BF", LCL = 0, UCL = 0 });
            parameters.Add("BP Overall Height", new BasicData { ShortName = "BP_OH", LCL = 0, UCL = 0 });

            parameters.Add("Aluminum", new BasicData { ShortName = "ZAL", LCL = 0, UCL = 2.8 });
            parameters.Add("Bismuth", new BasicData { ShortName = "ZBI", LCL = 0, UCL = 5.0 });
            parameters.Add("Chromium", new BasicData { ShortName = "ZCR", LCL = 0, UCL = 10.0 });
            parameters.Add("Copper", new BasicData { ShortName = "ZCU", LCL = 0, UCL = 10 });
            parameters.Add("Iron", new BasicData { ShortName = "IA", LCL = 0, UCL = 4.2 });
            parameters.Add("Gallium", new BasicData { ShortName = "GH", LCL = 0, UCL = 5.0 });
            parameters.Add("Magnesium", new BasicData { ShortName = "ZMG", LCL = 0, UCL = 5.0 });
            parameters.Add("Manganese", new BasicData { ShortName = "ZMN", LCL = 0, UCL = 5.0 });
            parameters.Add("Nickel", new BasicData { ShortName = "ZNI", LCL = 0, UCL = 10 });
            parameters.Add("Lead", new BasicData { ShortName = "ZPB", LCL = 0, UCL = 5.0 });
            parameters.Add("Scandium", new BasicData { ShortName = "SCA", LCL = 0, UCL = 5.0 });
            parameters.Add("Sulfur", new BasicData { ShortName = "ZS", LCL = 0, UCL = 10 });
            parameters.Add("Titanium", new BasicData { ShortName = "ZTI", LCL = 0, UCL = 5.4 });
            parameters.Add("Tungsten", new BasicData { ShortName = "TUNG", LCL = 0, UCL = 5.0 });
            parameters.Add("Yttrium", new BasicData { ShortName = "YTR", LCL = 0, UCL = 5.0 });
            parameters.Add("Total Metallic Impurities", new BasicData { ShortName = "TMI", LCL = 0, UCL = 50.0 });
            parameters.Add("Oxygen", new BasicData { ShortName = "ZO", LCL = 214.3, UCL = 452.90 });
            parameters.Add("Nitrogen", new BasicData { ShortName = "ZN", LCL = 0, UCL = 30 });
        }


        public BasicData GetShortName(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            else
            {
                return new BasicData() { ShortName = "UnKnown", LCL = 0, UCL = 100 };
            }
        }

    }
}
