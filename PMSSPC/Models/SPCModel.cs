using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PMSSPC.Models
{
    public class SPCModel
    {
        public SPCModel()
        {
            Items = new List<SPCDataItem>();
        }

        //stored values
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<SPCDataItem> Items { get; set; }
        public string Unit { get; set; } = "g/cm3";
        public string SPCType { get; set; } = "Density";


        //calculate values
        public double UCL { get; set; }
        public double CL { get; set; }
        public double LCL { get; set; }
        public double USL { get; set; }
        public double SL { get; set; }
        public double LSL { get; set; }

        public double Cp { get; set; }
        public double Sigma { get; set; }
        public double Cpk { get; set; }

        /// <summary>
        /// SPC 计算方法
        /// </summary>

        public void Calc()
        {
            if (Items.Count == 0) return;

            //如果USL LSL SL没有设置的话，自动计算生成
            if (USL == 0 || LSL == 0 || SL == 0)
            {
                USL = Items.Max(i => i.Value);
                LSL = Items.Min(i => i.Value);
                SL = (USL + LSL) / 2;
            }

            CL = Items.Average(i => i.Value);

            //与平均数的差值的平方和
            double sum = Items.Sum(i => Math.Pow(i.Value - CL, 2));

            Sigma = Math.Sqrt(sum / (Items.Count() - 1));

            UCL = CL + 3 * Sigma;
            LCL = CL - 3 * Sigma;

            Cp = Math.Abs(USL - LSL) / 6 / Sigma;
            Cpk = Cp * (1 - (2 * Math.Abs(SL - CL) / Math.Abs(USL - LSL)));
        }

    }
}
