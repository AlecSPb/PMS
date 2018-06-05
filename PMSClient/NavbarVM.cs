using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient
{
    public class NavbarVM : ViewModelBase
    {
        public NavbarVM()
        {
            initializeCommands();
        }

        private void initializeCommands()
        {
            #region Tool
            IntergratedSearch = new RelayCommand(() =>
            {
                var tool = new ToolWindow.ComplexQueryTool();
                tool.Show();
            });
            MaterialNeedCalculator = new RelayCommand(() =>
            {
                var tool = new Tool.MaterialNeedCalculationWindow();
                tool.Show();
            });
            ToOne = new RelayCommand(() =>
            {
                var tool = new ToolWindow.CompositionToOne();
                tool.Show();
            });
            PressureTool = new RelayCommand(() =>
            {
                var tool = new ToolWindow.PressureChangeTool();
                tool.Show();
            });
            #endregion
            #region Info
            HelpInfo = new RelayCommand(() =>
            {
                try
                {
                    string helpFileName = "";
                    if (PMSHelper.Language == "zh-cn")
                    {
                        helpFileName = "pmshelp_ch.pptx";
                    }
                    else
                    {
                        helpFileName = "pmshelp_en.pptx";
                    }

                    string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                    if (File.Exists(helpFile))
                    {
                        System.Diagnostics.Process.Start(helpFile);
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }

            });
            CodeRule = new RelayCommand(() =>
              {
                  try
                  {
                      string helpFileName = "CodeRule.docx";
                      string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                      if (File.Exists(helpFile))
                      {
                          System.Diagnostics.Process.Start(helpFile);
                      }
                  }
                  catch (Exception ex)
                  {
                      PMSHelper.CurrentLog.Error(ex);
                  }
              });
            LaserRule = new RelayCommand(() =>
              {
                  try
                  {
                      string helpFileName = "LaserRule.docx";
                      string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                      if (File.Exists(helpFile))
                      {
                          System.Diagnostics.Process.Start(helpFile);
                      }
                  }
                  catch (Exception ex)
                  {
                      PMSHelper.CurrentLog.Error(ex);
                  }
              });
            UpdateInfo = new RelayCommand(() =>
              {
                  var updateFile = System.IO.Path.Combine(PMSFolderPath.Roots, "updates.txt");
                  if (System.IO.File.Exists(updateFile))
                  {
                      var win = new ToolWindow.PlainTextWindow();
                      win.Title = "更新";
                      win.ContentText = System.IO.File.ReadAllText(updateFile);
                      win.ShowDialog();
                  }
              });
            FillingTool = new RelayCommand(() =>
              {
                  var tool = new PMSClient.View.FillingToolWindow();
                  tool.Show();
              });
            FillingToolRule = new RelayCommand(() =>
              {
                  try
                  {
                      string helpFileName = "FillingToolRule.pptx";
                      string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                      if (File.Exists(helpFile))
                      {
                          System.Diagnostics.Process.Start(helpFile);
                      }
                  }
                  catch (Exception ex)
                  {
                      PMSHelper.CurrentLog.Error(ex);
                  }
              });
            Symbol = new RelayCommand(()=>
            {
                var tool = new ToolWindow.SymbolWindow();
                tool.Show();
            });
            #endregion
        }

        public RelayCommand IntergratedSearch { get; set; }
        public RelayCommand MaterialNeedCalculator { get; set; }
        public RelayCommand ToOne { get; set; }
        public RelayCommand PressureTool { get; set; }
        public RelayCommand HelpInfo { get; set; }
        public RelayCommand UpdateInfo { get; set; }
        public RelayCommand LaserRule { get; set; }
        public RelayCommand CodeRule { get; set; }
        public RelayCommand FillingTool { get; set; }
        public RelayCommand FillingToolRule { get; set; }
        public RelayCommand Symbol { get; set; }
    }
}
