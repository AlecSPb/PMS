using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordTestDocVM : ViewModelBase
    {
        public RecordTestDocVM()
        {
            GiveUp = new RelayCommand(GoBack);
            Doc = new RelayCommand<string>(ActionDoc);
            currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        /// <summary>
        /// 生成报告
        /// </summary>
        /// <param name="arg"></param>
        private void ActionDoc(string arg)
        {
            //TODO:完成这里的报告生成
            try
            {
                switch (arg)
                {
                    case "Test":
                        break;
                    case "CoA":
                        break;
                    case "CoABridgeLine":
                        break;
                    case "Opticraft":
                        break;
                    case "TCB":
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

        public RelayCommand<string> Doc { get; set; }

    }
}
