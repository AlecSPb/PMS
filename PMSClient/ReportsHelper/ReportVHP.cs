using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PMSClient.MainService;
using Novacode;
using System.Drawing;

namespace PMSClient.ReportsHelper
{
    public class ReportVHP : ReportBase
    {
        private string prefix = "热压报告";
        public ReportVHP()
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "ReportRecordVHP.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "ReportRecordVHP_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }

        public void SetTargetFolder(string targetFolder)
        {
            var targetName = $"{prefix}{ReportHelper.TimeName}";
            targetFile = Path.Combine(targetFolder, targetName);
        }
        public void SetModel(DcPlanWithMisson model)
        {
            if (model != null)
            {
                this.model = model;
            }
        }
        private DcPlanWithMisson model;
        public override void Output()
        {
            try
            {
                if (model == null)
                {
                    return;
                }
                //复制到临时文件
                ReportHelper.FileCopy(sourceFile, tempFile);
                #region 创建报告
                using (DocX document = DocX.Load(tempFile))
                {
                    var plandate = model.Plan.PlanDate.ToString("yyyy-MM-dd");
                    document.ReplaceText("[PlanDate]", plandate??"");
                    document.ReplaceText("[PlanLot]", model.Plan.PlanLot.ToString());
                    document.ReplaceText("[PlanType]", model.Plan.PlanType.ToString());
                    document.ReplaceText("[VHPDeviceCode]", model.Plan.VHPDeviceCode??"");
                    document.ReplaceText("[Composition]", model.Misson.CompositionStandard??"");
                    document.ReplaceText("[Temperature]", model.Plan.Temperature.ToString());
                    document.ReplaceText("[Pressure]", model.Plan.Pressure.ToString());
                    document.ReplaceText("[Vaccum]", model.Plan.Vaccum.ToString("#.##E00"));
                    document.ReplaceText("[KeepTime]", model.Plan.KeepTempTime.ToString());


                    List<DcRecordVHP> RecordVHPs = new List<DcRecordVHP>();
                    RecordVHPs.Clear();
                    using (var service = new RecordVHPServiceClient())
                    {
                        service.GetRecordVHP(model.Plan.ID).OrderBy(i => i.CurrentTime).ToList().ForEach(i => RecordVHPs.Add(i));
                    }

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[0];
                        int rownumber = 1;
                        foreach (var item in RecordVHPs)
                        {
                            var currentTime = item.CurrentTime.ToString("yyyy-MM-dd HH:mm:ss");
                            mainTable.Rows[rownumber].Cells[0].Paragraphs[0].Append(currentTime).FontSize(10).Font(new FontFamily("宋体"));
                            mainTable.Rows[rownumber].Cells[1].Paragraphs[0].Append(item.PV1.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[2].Paragraphs[0].Append(item.PV2.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[3].Paragraphs[0].Append(item.PV3.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[4].Paragraphs[0].Append(item.SV.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[5].Paragraphs[0].Append(item.Ton.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[6].Paragraphs[0].Append(item.Vaccum.ToString("#.##E00")).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[7].Paragraphs[0].Append(item.Omega.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[8].Paragraphs[0].Append(item.Shift1.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[9].Paragraphs[0].Append(item.Shift2.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[10].Paragraphs[0].Append(item.WaterTemperatureIn.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[11].Paragraphs[0].Append(item.WaterTemperatureOut.ToString()).FontSize(10).Font(new FontFamily("宋体")).Alignment = Alignment.right;
                            mainTable.Rows[rownumber].Cells[12].Paragraphs[0].Append(item.ExtraInformation.ToString()).FontSize(10).Font(new FontFamily("宋体"));

                            mainTable.InsertRow();
                            rownumber++;
                        }

                    }


                    document.Save();
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}
