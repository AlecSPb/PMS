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
            NavigationService.Status("开始创建报告……");
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
                    case "Defect":
                        CreateDefects();
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
            NavigationService.Status($"{reportName}创建完毕！");
            //PMSDialogService.ShowYes("提示",$"{reportName}创建完毕，请打开报告检查内容是否正确");
        }

        #region 创建报告

        private void CreateDefects()
        {
            try
            {
                //TODO:完成创建缺陷报告
                PMSDialogService.ShowYes("Defects");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void CreateRecordTest()
        {
            try
            {
                WordRecordTest report = new WordRecordTest();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();

                ShowMessageAfterCreateDoc("测试记录报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void CreateCOA()
        {
            try
            {
                WordCOA report = new WordCOA();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();

                ShowMessageAfterCreateDoc("COA报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void CreateCOABridgeLine()
        {
            try
            {
                WordCOABridgeLine report = new WordCOABridgeLine();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("COABridgeLine报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void CreateReportGASElastomer440Blank()
        {
            try
            {
                WordTCB440 report = new WordTCB440();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("TCB440绑定报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
            }
        }

        private void CreateReportGASOpticraftGrinding()
        {
            try
            {
                WordOpticraft440 report = new WordOpticraft440();
                report.SetModel(CurrentRecordTest);
                report.SetTargetFolder(CurrentFolder);
                report.Output();
                ShowMessageAfterCreateDoc("Opticraft440绑定报告");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                NavigationService.Status(ex.Message);
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
