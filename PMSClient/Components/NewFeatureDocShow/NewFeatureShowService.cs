using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PMSClient.Components.NewFeatureDocShow
{
    /// <summary>
    /// 用于启动的时候显示新的功能
    /// </summary>
    public class NewFeatureShowService
    {
        private string baseFolder;
        public NewFeatureShowService()
        {
            baseFolder = Path.Combine(Environment.CurrentDirectory, "HelpDocs");
            config = new NewFeatureShowConfig();
        }

        private NewFeatureShowConfig config;
        public void Run()
        {
            if (config.IsWorking)
            {
                string[] users = config.HelpGroupNames;
                Show(config.HelpFileName, users, config.HelpFileEdition);
            }
        }

        public void ShowAnyWay()
        {
            string filepath = Path.Combine(baseFolder, config.HelpFileName);
            if (File.Exists(filepath))
            {
                System.Diagnostics.Process.Start(filepath);
            }
        }

        private void Show(string filename, string[] groups, int silenceCode = 0)
        {
            try
            {

                if (!PMSHelper.CurrentSession.IsInGroup(groups))
                {
                    return;
                }

                //每次同样的文件不再查看，确保只查看一次
                if (Properties.Settings.Default.ShowHelpDocSilenceCode != silenceCode)
                {
                    if (PMSDialogService.ShowYesNo("请问", "要查看本次新功能说明文件吗？Yes=查看 No=此次跳过"))
                    {
                        string filepath = Path.Combine(baseFolder, filename);
                        if (File.Exists(filepath))
                        {
                            System.Diagnostics.Process.Start(filepath);
                        }
                    }
                    Properties.Settings.Default.ShowHelpDocSilenceCode = silenceCode;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
