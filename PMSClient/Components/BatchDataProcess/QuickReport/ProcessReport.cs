using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.DataProcess.ScanInput;
using PMSClient.MainService;
using PMSClient.ReportsHelper;
using PMSClient.ReportsHelperNew;

namespace PMSClient.DataProcess.QuickReport
{
    public class ProcessReport : ProcessBase
    {
        public ProcessReport()
        {
            CurrentReportType = "TEST";
        }
        public string CurrentReportType { get; set; }

        public override void Check(Action<double> DoSomething)
        {
            try
            {
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //默认有效，多次否决
                    CheckIsStandard(item);
                    CheckInRecordTest(item);

                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        System.Threading.Thread.Sleep(50);
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public override void Process(Action<double> DoSomething)
        {
            try
            {
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //检查
                    CheckIsStandard(item);
                    CheckInRecordTest(item);

                    //有效继续
                    if (item.IsValid)
                    {
                        DcRecordTest model = null;

                        using (var service = new RecordTestServiceClient())
                        {
                            model = service.GetRecordTestByProductID(item.Lot).FirstOrDefault();
                        }

                        if (model != null)
                        {
                            #region 生成报告

                            switch (CurrentReportType)
                            {
                                case "COA":
                                    WordCOANew report1 = new WordCOANew();
                                    report1.SetModel(model);
                                    report1.Output();
                                    break;
                                case "COA-BL":
                                    WordCOABridgeLineNew report2 = new WordCOABridgeLineNew();
                                    report2.SetModel(model);
                                    report2.Output();
                                    break;
                                case "TEST":
                                    WordRecordTest report3 = new WordRecordTest();
                                    report3.SetModel(model);
                                    report3.Output();
                                    break;
                                case "COA200324":
                                    var fileName = $"PMI_COA_{StringUtil.RemoveSlash(model.Customer)}_{StringUtil.RemoveSlash(model.CompositionAbbr)}" +
                                        $"_{model.ProductID}.docx".Replace('-', '_');
                                    var report4 = new ReportCOA(model);
                                    report4.Intialize(fileName);
                                    report4.Output();
                                    break;
                                case "COA200324-BL":

                                    break;
                                default:
                                    break;
                            }

                            item.HasProcessed = true;
                            #endregion
                        }

                    }
                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        System.Threading.Thread.Sleep(50);
                    }


                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void CheckInRecordTest(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new RecordTestServiceClient())
                {
                    //这里增加一个服务？
                    int count = service.GetRecordTestByProductID(item.Lot).Count();
                    if (count == 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[测试]记录中不存在");
                    }
                }
            }
        }
    }
}
