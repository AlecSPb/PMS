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
using System.IO;
using PMSClient.Components.CscanImageProcess;

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
                    case "CoA200324":
                        CreateCoA200324();
                        break;
                    case "CoA200324BL":
                        CreateCoA200324BL();
                        break;
                    default:
                        break;
                }
                //TODO:这里设置生成完毕后打开对应文件夹

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void CreateCoA200324BL()
        {
            if (CurrentRecordTest.State != PMSCommon.CommonState.已核验.ToString())
            {
                PMSDialogService.ShowWarning($"[{CurrentRecordTest.ProductID}]还没有核验，请慎重使用当前数据");
            }
            try
            {
                var fileName = $"BL_COA_{StringUtil.RemoveSlash(CurrentRecordTest.Customer)}_{StringUtil.RemoveSlash(CurrentRecordTest.CompositionAbbr)}_{CurrentRecordTest.ProductID}.docx".Replace('-', '_');

                var dialog = new ImageTypeSelectionDialog();
                dialog.ShowDialog();
                if (dialog.DialogResult == false)
                {
                    return;
                }


                var word = new ReportsHelperNew.ReportCOABridgeLine();
                word.SetParameters(CurrentRecordTest, dialog.SelectedImageType);
                word.Intialize(fileName);
                word.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void CreateCoA200324()
        {
            if (CurrentRecordTest.State != PMSCommon.CommonState.已核验.ToString())
            {
                PMSDialogService.ShowWarning($"[{CurrentRecordTest.ProductID}]还没有核验，请慎重使用当前数据");
            }
            try
            {
                var fileName = $"PMI_COA_{StringUtil.RemoveSlash(CurrentRecordTest.Customer)}_{StringUtil.RemoveSlash(CurrentRecordTest.CompositionAbbr)}_{CurrentRecordTest.ProductID}.docx".Replace('-', '_');

                var dialog = new ImageTypeSelectionDialog();
                dialog.ShowDialog();
                if (dialog.DialogResult == false)
                {
                    return;
                }


                var word = new ReportsHelperNew.ReportCOA();
                word.SetParameters(CurrentRecordTest, dialog.SelectedImageType);
                word.Intialize(fileName);
                word.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void ShowMessageAfterCreateDoc(string reportName)
        {
            PMSDialogService.Show("提示", $"{reportName}创建完毕，请打开报告仔细检查内容是否正确");
            NavigationService.Status($"{reportName}创建完毕！");
        }

        #region 创建报告

        private void CreateDefects()
        {
            try
            {
                //TODO:完成创建缺陷报告
                PMSDialogService.Show("Defects");
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
                WordCOANew report = new WordCOANew();
                report.SetModel(CurrentRecordTest);
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
                WordCOABridgeLineNew report = new WordCOABridgeLineNew();
                report.SetModel(CurrentRecordTest);
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
