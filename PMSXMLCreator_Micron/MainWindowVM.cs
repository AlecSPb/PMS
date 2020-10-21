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

            Messenger.Default.Register<Micon_COA>(this, "SaveString", ActionSaveString);
        }

        private void ActionSaveString(Micon_COA obj)
        {
            if (obj != null)
            {
                StringBuilder sb = new StringBuilder();
                obj.Header.ForEach(i => sb.AppendLine($"${i.FieldName}+{i.FieldValue}"));
                sb.AppendLine();
                obj.InspectionItems.ForEach(i => sb.AppendLine($"*{i.ItemName}+{i.ResultItems[0].Value}+" +
                    $"{i.ResultItems[1].Value}+{i.ResultItems[2].Value}+{i.ResultItems[3].Value}"));
                InputText = sb.ToString();
            }
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

            Editor = new RelayCommand(ActionEditor);
        }

        private void ActionEditor()
        {
            try
            {
                var service = new Analyzer();
                Micon_COA coa = service.Resolve(InputText);
                var editor = new ECOAEditor();
                var editorvm = new ECOAEditorVM();
                coa.Header.ForEach(i => editorvm.Headers.Add(i));

                coa.InspectionItems.ForEach(i => editorvm.Contents.Add(i));

                editor.DataContext = editorvm;
                editor.ShowDialog();
            }
            catch (Exception)
            {

            }
        }

        private string temp_saved_file = "SavedFile\\temp_current_model.txt";
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
                string s = XSHelper.FileHelper.ReadText("SavedFile\\template_default.txt");
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
                string lastlogfilename = Properties.Settings.Default.LastLogFileName;
                if (File.Exists(lastlogfilename))
                {
                    System.Diagnostics.Process.Start(lastlogfilename);
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
        public RelayCommand Editor { get; set; }

    }
}
