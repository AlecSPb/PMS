using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using CommonHelper;
using PMSXMLCreator_Micron.Model;
using PMSXMLCreator_Micron.Service;
using System.IO;

namespace PMSXMLCreator_Micron
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            Init();
            ReadDefaultTemplate();
        }

        private void Init()
        {
            DefaultInput = new RelayCommand(ActionDefaultInput);
            Open = new RelayCommand(ActionOpen);
            Save = new RelayCommand(ActionSave);

            Create = new RelayCommand(ActionCreate);
            OutputFolder = new RelayCommand(ActionOutputFolder);
            Log = new RelayCommand(ActionLog);
        }

        private void ReadDefaultTemplate()
        {
            try
            {
                string s = XSHelper.FileHelper.ReadText("template_default.txt");
                InputText = s;
            }
            catch (Exception)
            {

            }
        }



        private void ActionLog()
        {
            try
            {
                string logfile = Path.Combine(Environment.CurrentDirectory, "log.txt");
                if (File.Exists(logfile))
                {
                    System.Diagnostics.Process.Start(logfile);
                }
                else
                {
                    XSHelper.MessageHelper.ShowInfo("log file doesn't exist");
                }
            }
            catch (Exception)
            {

            }
        }

        private void ActionOutputFolder()
        {
            throw new NotImplementedException();
        }

        private void ActionCreate()
        {

            if (string.IsNullOrEmpty(InputText))
            {
                XSHelper.MessageHelper.ShowWarning("输入内容为空，请确认");
                return;
            }

            var service = new Analyzer();
            Micon_COA coa = service.Resolve(InputText);
            if (XSHelper.MessageHelper.ShowYesNo($"确定使用该条数据[{coa.ProductId}]生成Docx文件？"))
            {
                var xmlhelper = new XMLHelper();
                xmlhelper.CreateECOA(coa);
            }

        }

        private void ActionSave()
        {
            throw new NotImplementedException();
        }

        private void ActionOpen()
        {
            throw new NotImplementedException();
        }

        private void ActionDefaultInput()
        {
            if (XSHelper.MessageHelper.ShowYesNo("要加载默认模板吗?"))
            {
                ReadDefaultTemplate();
            }
        }

        private string inputText;

        public string InputText
        {
            get { return inputText; }
            set { inputText = value; RaisePropertyChanged(nameof(InputText)); }
        }


        public RelayCommand DefaultInput { get; set; }
        public RelayCommand Open { get; set; }
        public RelayCommand Save { get; set; }

        public RelayCommand Create { get; set; }
        public RelayCommand OutputFolder { get; set; }
        public RelayCommand Log { get; set; }

    }
}
