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
    public class AMATSpecs : ISpecs
    {
        private Dictionary<string, SpecModel> parameters;
        public string SpecName { get => "AMATSpecs"; }
        public AMATSpecs()
        {
            parameters = new Dictionary<string, SpecModel>();
            Init();
        }

        private void Init()
        {
            parameters.Add("Selenium", new SpecModel { ShortName = "ZSE", LCL = 50.25, UCL = 51.37, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Arsenic", new SpecModel { ShortName = "ZAS", LCL = 30.12, UCL = 31.08, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Germanium", new SpecModel { ShortName = "ZGE", LCL = 12.29, UCL = 13.16, UnitOfMeasure = ParameterUnit.Percent });
            parameters.Add("Silicon", new SpecModel { ShortName = "ZSI", LCL = 5.37, UCL = 6.21, UnitOfMeasure = ParameterUnit.Percent });

            //AMAT特有
            parameters.Add("Target Diameter", new SpecModel { ShortName = "DIA_TAR", LCL = 444.57, UCL = 444.83, UnitOfMeasure = ParameterUnit.Density });//后面要改成mm
            parameters.Add("Target Thickness", new SpecModel { ShortName = "THK_TAR", LCL = 5.87, UCL = 6.07, UnitOfMeasure = ParameterUnit.MM });

            parameters.Add("Target Blank OD", new SpecModel { ShortName = "TAR_BOD", LCL = 444.57, UCL = 444.83, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Thickness", new SpecModel { ShortName = "TAR_BTHK", LCL =5.87, UCL = 6.07, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank 2mm Top Rad", new SpecModel { ShortName = "TAR_BTOP", LCL = 0, UCL = 100, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Diameter", new SpecModel { ShortName = "BP_DIA", LCL = 523.75, UCL = 524.01, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Flange Thickness", new SpecModel { ShortName = "BP_FTHK", LCL = 12.57, UCL = 12.83, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Sidewall Height", new SpecModel { ShortName = "BP_SH", LCL = 81, UCL = 84, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Depth", new SpecModel { ShortName = "BP_PDE", LCL = 75.0, UCL = 78.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Target Blank Step", new SpecModel { ShortName = "BP_TBS", LCL = 0.50, UCL = 1.50, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Side Wall Dia", new SpecModel { ShortName = "BP_SDIA", LCL = 484.5, UCL = 487.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Pocket Dia", new SpecModel { ShortName = "BP_PDIA", LCL = 456.5, UCL = 459.5, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Bonded Surface Flatness", new SpecModel { ShortName = "BP_BSF", LCL = 0, UCL = 0.13, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Backside Flatness", new SpecModel { ShortName = "BP_BF", LCL = 0, UCL = 0.13, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("BP Overall Height", new SpecModel { ShortName = "BP_OH", LCL = 17.52, UCL = 17.78, UnitOfMeasure = ParameterUnit.MM });

            parameters.Add("Density", new SpecModel { ShortName = "DEN", LCL = 4.3011, UCL = 4.3937, UnitOfMeasure = ParameterUnit.Density });
            parameters.Add("Weight", new SpecModel { ShortName = "Weight", LCL = 3898.1, UCL = 4168.9, UnitOfMeasure = ParameterUnit.Weight });
            parameters.Add("Target Blank Flatness", new SpecModel { ShortName = "TAR_BFLA", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Parallism", new SpecModel { ShortName = "TAR_BPAR", LCL = 0, UCL = 1.0, UnitOfMeasure = ParameterUnit.MM });
            parameters.Add("Target Blank Surface Roughness", new SpecModel { ShortName = "TAR_BSUR", LCL = 3, UCL = 4, UnitOfMeasure = ParameterUnit.UM });

            parameters.Add("Aluminum", new SpecModel { ShortName = "ZAL", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Bismuth", new SpecModel { ShortName = "ZBI", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Chromium", new SpecModel { ShortName = "ZCR", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Copper", new SpecModel { ShortName = "ZCU", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Iron", new SpecModel { ShortName = "IA", LCL = 0, UCL = 5, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Gallium", new SpecModel { ShortName = "GH", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Magnesium", new SpecModel { ShortName = "ZMG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Manganese", new SpecModel { ShortName = "ZMN", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nickel", new SpecModel { ShortName = "ZNI", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Lead", new SpecModel { ShortName = "ZPB", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Scandium", new SpecModel { ShortName = "SCA", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Sulfur", new SpecModel { ShortName = "ZS", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Titanium", new SpecModel { ShortName = "ZTI", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Tungsten", new SpecModel { ShortName = "TUNG", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Yttrium", new SpecModel { ShortName = "YTR", LCL = 0, UCL = 5.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Zirconium", new SpecModel { ShortName = "ZZR", LCL = 0, UCL = 10.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Total Metallic Impurities", new SpecModel { ShortName = "TMI", LCL = 0, UCL = 50.0, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Oxygen", new SpecModel { ShortName = "ZO", LCL = 0, UCL = 700, UnitOfMeasure = ParameterUnit.PPM });
            parameters.Add("Nitrogen", new SpecModel { ShortName = "ZN", LCL = 0, UCL = 10, UnitOfMeasure = ParameterUnit.PPM });
        }


        public SpecModel GetShortName(string key)
        {
            if (parameters.ContainsKey(key))
            {
                return parameters[key];
            }
            else
            {
                return new SpecModel() { ShortName = "UnKnown", LCL = 0, UCL = 100 };
            }
        }

    }
}
