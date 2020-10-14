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
            //ReadDefaultTemplate();
        }

        private void Init()
        {
            DefaultInput = new RelayCommand(ActionDefaultInput);
            Open = new RelayCommand(ActionOpen);
            Save = new RelayCommand(ActionSave);

            Create = new RelayCommand(ActionCreate);
            OutputFolder = new RelayCommand(ActionOutputFolder);
            Log = new RelayCommand(ActionLog);
            ClosingCommand = new RelayCommand(ActionClosingCommand);
            LoadedCommand = new RelayCommand(ActionLoadedCommand);
        }

        private string temp_saved_file = "temp_current_model.txt";
        private void ActionLoadedCommand()
        {
            InputText = FileSaver.LoadText(temp_saved_file);
        }

        private void ActionClosingCommand()
        {
            FileSaver.SaveText(InputText, temp_saved_file);
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
            try
            {
                System.Diagnostics.Process.Start(XSHelper.FileHelper.GetCurrentFolderPath("OutputFile"));
            }
            catch (Exception)
            {

            }
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
            if (XSHelper.MessageHelper.ShowYesNo($"确定使用该条数据[{coa.ProductId}]生成xml文件？"))
            {
                var xmlhelper = new XmlHelper();
                xmlhelper.CreateECOA(coa);
            }

        }

        private void ActionSave()
        {
            string initialDirectory = XSHelper.FileHelper.GetCurrentFolderPath("SavedFile");
            var service = new Analyzer();
            Micon_COA coa = service.Resolve(InputText);

            string saved_file = Path.Combine(initialDirectory, $"{coa.ProductId}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");
            FileSaver.SaveText(InputText, saved_file);

            XSHelper.MessageHelper.ShowInfo($"[{coa.ProductId}] Saved Success");
        }

        private void ActionOpen()
        {
            string initialDirectory = XSHelper.FileHelper.GetCurrentFolderPath("SavedFile");
            string filter = "Data|*.txt";
            XSDialogResult savePath = XSHelper.DialogHelper.ShowOpenDialog(initialDirectory, filter);

            if (savePath.HasSelected == true)
            {
                InputText = FileSaver.LoadText(savePath.SelectPath);
                XSHelper.MessageHelper.ShowInfo("Loaded Success");
            }
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
        public RelayCommand ClosingCommand { get; set; }
        public RelayCommand LoadedCommand { get; set; }

    }
}
