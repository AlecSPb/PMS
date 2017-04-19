using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PMSClient.MainService;
using Novacode;

namespace PMSClient.ReportsHelper
{
    public class ReportVHP : ReportBase
    {    {
        private string prefix = "";
        public ReportVHP()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "COA.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "COA_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            targetFile = Path.Combine(targetFolder, targetName);
        }
        public void SetModel(DcOrder model)
        {
            if (model != null)
            {
                this.model = model;
            }
        }
        private DcOrder model;
        public override void Output()
        {
            using (DocX document = DocX.Load(tempFile))
            {
               


                document.Save();
            }
        }

    }
}
