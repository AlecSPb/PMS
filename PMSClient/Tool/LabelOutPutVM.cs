using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.IO;

namespace PMSClient.Tool
{
    public class LabelOutPutVM : ViewModelBase
    {
        public LabelOutPutVM()
        {
            requestView = PMSViews.RecordDelivery;
            PageTitle = "发货单标签输出结果";
            Tips = "打开BarTender软件，请将下面的标签复制到对应BarTender模板当中";
            TemplateFileName = "发货单.btw";
            MainContent = "标签内容";

            GiveUp = new RelayCommand(ActionGiveUp);
            OpenFile = new RelayCommand(ActionOpenFile);
        }

        public void SetAllParameters(PMSViews requestView, string pageTitle, string tips, string templateFilePath, string mainContent)
        {
            this.requestView = requestView;
            PageTitle = pageTitle;
            Tips = tips;
            TemplateFileName = templateFilePath;
            MainContent = mainContent;
        }


        private void ActionOpenFile()
        {
            try
            {
                var filepath = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "BarTender101", TemplateFileName,".btw");
                var targetpath = Path.Combine(Environment.CurrentDirectory, "DocTemplate", "BarTender101", TemplateFileName,"_temp.btw");

                //复制一下
                if (File.Exists(targetpath))
                {
                    File.Delete(targetpath);
                }
                File.Copy(filepath, targetpath);

                System.Diagnostics.Process.Start(targetpath);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(requestView);
        }

        private PMSViews requestView;

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(PageTitle));
            }
        }

        private string tips;
        public string Tips
        {
            get { return tips; }
            set { tips = value; RaisePropertyChanged(nameof(Tips)); }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateFileName { get; set; }


        private string mainContent;
        public string MainContent
        {
            get { return mainContent; }
            set { mainContent = value; RaisePropertyChanged(nameof(MainContent)); }
        }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand OpenFile { get; set; }
    }
}
