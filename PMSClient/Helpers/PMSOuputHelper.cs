using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient
{
    public static class PMSOuputHelper
    {
        public static string GetPlanOutput(IList<DcPlanWithMisson> models)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            models.ToList().ForEach(o =>
            {
                #region 需要导出的数据列
                sb.Append(o.Plan.SearchCode);
                sb.Append(",");
                sb.Append(o.Misson.CompositionStandard);
                sb.Append(",");
                sb.Append(o.Plan.MoldDiameter);
                sb.Append(",");
                sb.Append(o.Plan.Thickness);
                sb.Append(",");
                sb.Append(o.Plan.Quantity);
                sb.Append(",");
                sb.Append(o.Plan.CalculationDensity);
                sb.Append(",");
                sb.Append(o.Plan.SingleWeight);
                sb.Append(",");
                sb.Append(o.Plan.Temperature);
                sb.Append(",");
                sb.Append(o.Plan.Pressure);
                sb.Append(",");
                sb.Append(o.Plan.FillingRequirement);
                sb.Append(",");
                sb.Append(o.Plan.MillingRequirement);
                sb.AppendLine();
                #endregion
            });
            return sb.ToString();
        }
    }
}
