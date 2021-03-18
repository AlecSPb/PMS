using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ReportsHelperNew
{
    public class ReportDefectPPTX
    {
        public void Create(DcRecordTest model)
        {
            if (model == null) return;

            string templatePath = Path.Combine(Environment.CurrentDirectory, "StandardDocs", "Target Defects Notification.pptx");

            string targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Defects Report {model.ProductID}.pptx");

            try
            {
                File.Copy(templatePath, targetPath, true);



                PMSDialogService.Show("请到桌面查看对应的PPTX文件");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
