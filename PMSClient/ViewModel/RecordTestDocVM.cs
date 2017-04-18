using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.ReportsHelper;

namespace PMSClient.ViewModel
{
    public class RecordTestDocVM : ViewModelBase
    {
        public RecordTestDocVM()
        {
            GiveUp = new RelayCommand(GoBack);
            CreateDoc = new RelayCommand<string>(ActionCreateDoc);
            currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        /// <summary>
        /// 生成报告
        /// </summary>
        /// <param name="arg"></param>
        private void ActionCreateDoc(string arg)
        {
            NavigationService.ShowStatusMessage("开始创建报告……");
            try
            {
                switch (arg)
                {
                    case "Test":
                        CreateRecordTest();
                        break;
                    case "CoA":
                        CreateCOA();
                        break;
                    case "CoABridgeLine":
                        CreateCOABridgeLine();
                        break;
                    case "Opticraft":
                        CreateReportGASOpticraftGrinding();
                        break;
                    case "TCB":
                        CreateReportGASElastomer440Blank();
                        break;
                    default:
                        break;
                }



            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ShowMessageAfterCreateDoc(string reportName)
        {
            NavigationService.ShowStatusMessage($"{reportName}创建完毕！");
            PMSDialogService.ShowYes("提示",$"{reportName}创建完毕，请打开报告检查内容是否正确");
        }

        #region 创建报告
        private void CreateRecordTest()
        {
            try
            {
                ReportRecordTest report = new ReportRecordTest();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();

                ShowMessageAfterCreateDoc("测试记录报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }

        private void CreateCOA()
        {
            try
            {
                ReportCOA report = new ReportCOA();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();

                ShowMessageAfterCreateDoc("COA报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }

        private void CreateCOABridgeLine()
        {
            try
            {
                ReportCOABridgeLine report = new ReportCOABridgeLine();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("COABridgeLine报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }

        private void CreateReportGASElastomer440Blank()
        {
            try
            {
                ReportTCB440 report = new ReportTCB440();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("TCB440绑定报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }

        private void CreateReportGASOpticraftGrinding()
        {
            try
            {
                ReportOpticraft440 report = new ReportOpticraft440();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("Opticraft440绑定报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.ShowStatusMessage(ex.Message);
            }
        }


        #endregion




        public void SetModel(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentRecordTest = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordTest);
        }
        private DcRecordTest currentRecordTest;
        public DcRecordTest CurrentRecordTest
        {
            get { return currentRecordTest; }
            set
            {
                Set(nameof(CurrentRecordTest), ref currentRecordTest, value);
            }
        }

        private string currentFolder;
        public string CurrentFolder
        {
            get { return currentFolder; }
            set { currentFolder = value; RaisePropertyChanged(nameof(CurrentFolder)); }
        }



        public RelayCommand Select { get; set; }
        public RelayCommand GiveUp { get; set; }

        public RelayCommand<string> CreateDoc { get; set; }

    }
}
