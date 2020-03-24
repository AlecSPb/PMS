using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel.VMHelper
{
    public class RecordTestVMHelper
    {
        public static string CreateLabel(DcRecordTest model)
        {
            if (model == null) return "";
            var lb = new StringBuilder();
            lb.AppendLine(model.ProductID);
            lb.AppendLine(model.Composition);
            lb.AppendLine(model.Dimension);
            lb.AppendLine(model.Customer);
            lb.AppendLine(model.PO);
            lb.AppendLine("=====  产品标签-空行↓  =====");
            lb.AppendLine(model.ProductID);
            lb.AppendLine();
            lb.AppendLine(model.Composition);
            lb.AppendLine();
            lb.AppendLine(model.Dimension);
            lb.AppendLine("=====  二维码标签↓  =====");
            lb.AppendLine(model.ProductID);
            lb.AppendLine(model.Composition);
            lb.AppendLine(Helpers.CompositionHelper.ConvertToAtmDescend(model.Composition));
            lb.AppendLine(Helpers.CompositionHelper.RemoveNumbers(model.Composition));
            lb.AppendLine(model.Customer);
            lb.AppendLine("=====  简成分样品标签↓  =====");
            lb.AppendLine(Helpers.CompositionHelper.RemoveNumbers(model.Composition));
            lb.AppendLine("Weight      g");
            lb.AppendLine(model.ProductID);
            lb.AppendLine("=====  全成分样品标签↓  =====");
            lb.AppendLine(model.Composition);
            lb.AppendLine("Weight      g");
            lb.AppendLine(model.ProductID);
            lb.AppendLine();
            lb.AppendLine("************************************");
            var mainContent = lb.ToString();

            return lb.ToString();

        }
    }
}
