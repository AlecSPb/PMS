using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// Intel提供的项缩写和上下限关系集合
    /// 按照其提供的spec
    /// </summary>
    public class IntelSpecs
    {
        private Dictionary<string, IntelSpecModel> parameters;
        public IntelSpecs()
        {
            parameters = new Dictionary<string, IntelSpecModel>();
            Init();
        }

        private void Init()
        {
            parameters.Add("Selenium", new IntelSpecModel { ShortName = "ZSE", LCL = 50.25, UCL = 51.37, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Arsenic", new IntelSpecModel { ShortName = "ZAS", LCL = 30.12, UCL = 31.08, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Germanium", new IntelSpecModel { ShortName = "ZGE", LCL = 12.29, UCL = 13.16, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Silicon", new IntelSpecModel { ShortName = "ZSI", LCL = 5.37, UCL = 6.21, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Target Blank OD", new IntelSpecModel { ShortName = "TAR_BOD", LCL = 439.74, UCL = 440.17, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Thickness", new IntelSpecModel { ShortName = "TAR_BTHK", LCL = 7.701, UCL = 7.899, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank 2mm Top Rad", new IntelSpecModel { ShortName = "TAR_BTOP", LCL = 0, UCL = 100, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Diameter", new IntelSpecModel { ShortName = "BP_DIA", LCL = 573.5, UCL = 576.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Flange Thickness", new IntelSpecModel { ShortName = "BP_FTHK", LCL = 15.5, UCL = 16.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Sidewall Height", new IntelSpecModel { ShortName = "BP_SH", LCL = 81, UCL = 84, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Depth", new IntelSpecModel { ShortName = "BP_PDE", LCL = 75.0, UCL = 78.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Target Blank Step", new IntelSpecModel { ShortName = "BP_TBS", LCL = 0.50, UCL = 1.50, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Side Wall Dia", new IntelSpecModel { ShortName = "BP_SDIA", LCL = 484.5, UCL = 487.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Dia", new IntelSpecModel { ShortName = "BP_PDIA", LCL = 456.5, UCL = 459.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Bonded Surface Flatness", new IntelSpecModel { ShortName = "BP_BSF", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Backside Flatness", new IntelSpecModel { ShortName = "BP_BF", LCL = 0, UCL = 0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Overall Height", new IntelSpecModel { ShortName = "BP_OH", LCL = 0, UCL = 0, UnitOfMeasure = ParameterUnit.MM });

            parameters.Add("Density", new IntelSpecModel { ShortName = "DEN", LCL = 4.3011, UCL = 4.3937, UnitOfMeasure = ParameterUnit.Density });
            parameters.Add("Weight", new IntelSpecModel { ShortName = "Weight", LCL = 5077.2, UCL = 5232.3, UnitOfMeasure = ParameterUnit.Weight });
            parameters.Add("Target Blank Flatness", new IntelSpecModel { ShortName = "TAR_BFLA", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Parallism", new IntelSpecModel { ShortName = "TAR_BPAR", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Surface Roughness", new IntelSpecModel { ShortName = "TAR_BSUR", LCL = 3, UCL = 4, UnitOfMeasure = ParameterUnit.UM });

            parameters.Add("Aluminum", new IntelSpecModel { ShortName = "ZAL", LCL = 0, UCL = 2.8, UnitOfMeasure = ParameterUnit.PPM});
            parameters.Add("Bismuth", new IntelSpecModel { ShortName = "ZBI", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Chromium", new IntelSpecModel { ShortName = "ZCR", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Copper", new IntelSpecModel { ShortName = "ZCU", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Iron", new IntelSpecModel { ShortName = "IA", LCL = 0, UCL = 4.2, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Gallium", new IntelSpecModel { ShortName = "GH", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Magnesium", new IntelSpecModel { ShortName = "ZMG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Manganese", new IntelSpecModel { ShortName = "ZMN", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nickel", new IntelSpecModel { ShortName = "ZNI", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Lead", new IntelSpecModel { ShortName = "ZPB", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Scandium", new IntelSpecModel { ShortName = "SCA", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Sulfur", new IntelSpecModel { ShortName = "ZS", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Titanium", new IntelSpecModel { ShortName = "ZTI", LCL = 0, UCL = 5.4, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Tungsten", new IntelSpecModel { ShortName = "TUNG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Yttrium", new IntelSpecModel { ShortName = "YTR", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Total Metallic Impurities", new IntelSpecModel { ShortName = "TMI", LCL = 0, UCL = 50.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Oxygen", new IntelSpecModel { ShortName = "ZO", LCL = 214.3, UCL = 452.90, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nitrogen", new IntelSpecModel { ShortName = "ZN", LCL = 0, UCL = 30, UnitOfMeasure = ParameterUnit.PPM });
        }


        public IntelSpecModel GetShortName(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            else
            {
                return new IntelSpecModel() { ShortName = "UnKnown", LCL = 0, UCL = 100 };
            }
        }

    }
}
