using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// Intel提供的项缩写和上下限关系
    /// 按照其提供的spec
    /// </summary>
    public class ParameterDict
    {
        private Dictionary<string, IntelData> parameters;
        public ParameterDict()
        {
            parameters = new Dictionary<string, IntelData>();
            Init();
        }

        private void Init()
        {
            parameters.Add("Selenium", new IntelData { ShortName = "ZSE", LCL = 50.25, UCL = 51.37, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Arsenic", new IntelData { ShortName = "ZAS", LCL = 30.12, UCL = 31.08, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Germanium", new IntelData { ShortName = "ZGE", LCL = 12.29, UCL = 13.16, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Silicon", new IntelData { ShortName = "ZSI", LCL = 5.37, UCL = 6.21, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Target Blank OD", new IntelData { ShortName = "TAR_BOD", LCL = 439.74, UCL = 440.17, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Thickness", new IntelData { ShortName = "TAR_BTHK", LCL = 7.701, UCL = 7.899, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank 2mm Top Rad", new IntelData { ShortName = "TAR_BTOP", LCL = 0, UCL = 100, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Diameter", new IntelData { ShortName = "BP_DIA", LCL = 573.5, UCL = 576.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Flange Thickness", new IntelData { ShortName = "BP_FTHK", LCL = 15.5, UCL = 16.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Sidewall Height", new IntelData { ShortName = "BP_SH", LCL = 81, UCL = 84, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Depth", new IntelData { ShortName = "BP_PDE", LCL = 75.0, UCL = 78.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Target Blank Step", new IntelData { ShortName = "BP_TBS", LCL = 0.50, UCL = 1.50, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Side Wall Dia", new IntelData { ShortName = "BP_SDIA", LCL = 484.5, UCL = 487.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Dia", new IntelData { ShortName = "BP_PDIA", LCL = 456.5, UCL = 459.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Bonded Surface Flatness", new IntelData { ShortName = "BP_BSF", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Density", new IntelData { ShortName = "DEN", LCL = 4.3011, UCL = 4.3937, UnitOfMeasure = ParameterUnit.Density });
            parameters.Add("Weight", new IntelData { ShortName = "Weight", LCL = 5077.2, UCL = 5232.3, UnitOfMeasure = ParameterUnit.Weight });
            parameters.Add("Target Blank Flatness", new IntelData { ShortName = "TAR_BFLA", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Parallism", new IntelData { ShortName = "TAR_BPAR", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Surface Roughness", new IntelData { ShortName = "TAR_BSUR", LCL = 3, UCL = 4, UnitOfMeasure = ParameterUnit.UM });

            parameters.Add("BP Backside Flatness", new IntelData { ShortName = "BP_BF", LCL = 0, UCL = 0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Overall Height", new IntelData { ShortName = "BP_OH", LCL = 0, UCL = 0, UnitOfMeasure = ParameterUnit.MM });

            parameters.Add("Aluminum", new IntelData { ShortName = "ZAL", LCL = 0, UCL = 2.8, UnitOfMeasure = ParameterUnit.PPM});
            parameters.Add("Bismuth", new IntelData { ShortName = "ZBI", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Chromium", new IntelData { ShortName = "ZCR", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Copper", new IntelData { ShortName = "ZCU", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Iron", new IntelData { ShortName = "IA", LCL = 0, UCL = 4.2, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Gallium", new IntelData { ShortName = "GH", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Magnesium", new IntelData { ShortName = "ZMG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Manganese", new IntelData { ShortName = "ZMN", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nickel", new IntelData { ShortName = "ZNI", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Lead", new IntelData { ShortName = "ZPB", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Scandium", new IntelData { ShortName = "SCA", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Sulfur", new IntelData { ShortName = "ZS", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Titanium", new IntelData { ShortName = "ZTI", LCL = 0, UCL = 5.4, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Tungsten", new IntelData { ShortName = "TUNG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Yttrium", new IntelData { ShortName = "YTR", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Total Metallic Impurities", new IntelData { ShortName = "TMI", LCL = 0, UCL = 50.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Oxygen", new IntelData { ShortName = "ZO", LCL = 214.3, UCL = 452.90, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nitrogen", new IntelData { ShortName = "ZN", LCL = 0, UCL = 30, UnitOfMeasure = ParameterUnit.PPM });
        }


        public IntelData GetShortName(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            else
            {
                return new IntelData() { ShortName = "UnKnown", LCL = 0, UCL = 100 };
            }
        }

    }
}
