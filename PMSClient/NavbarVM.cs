using System;
using System.Collections.Generic;
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
        }

        public RelayCommand IntergratedSearch { get; set; }
        public RelayCommand MaterialNeedCalculator { get; set; }
        public RelayCommand ToOne { get; set; }
        public RelayCommand PressureTool { get; set; }
    }
}
