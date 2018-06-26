using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.DataProcess.ScanInput
{
    public class ScanInputProductVM : ScanInputVMBase
    {
        public ScanInputProductVM()
        {
            SourceTarget = "测试记录 => 产品记录";
            process = new ProcessProduct();

            Process = new RelayCommand(ActionProcess,CanCheck);
            Check = new RelayCommand(ActionCheck,CanCheck);
            Lots = new ObservableCollection<LotModel>();
        }

        private bool CanCheck()
        {
            return canClick;
        }

        private ProcessProduct process;

        private void ActionCheck()
        {
            Task task = new Task(() =>
            {
                canClick = false;
                process.Intialize(InputText);

                process.Check(i =>
                {
                    ProgressValue = i;
                });
                App.Current.Dispatcher.Invoke(() =>
                {
                    RefreshLotsStatus();

                });
                canClick = true;
            });
            task.Start();
        }

        private void ActionProcess()
        {
            if (PMSDialogService.ShowYesNo("请问", "确定继续吗？") == false)
                return;

            Task task = new Task(() =>
            {
                canClick = false;
                process.Intialize(InputText);

                process.Process(i =>
                {
                    ProgressValue = i;
                });
                App.Current.Dispatcher.Invoke(() =>
                {
                    RefreshLotsStatus();
                     
                });
                canClick = true;
            });

            task.Start();
        }


        private void RefreshLotsStatus()
        {
            Lots.Clear();
            process.Lots.ForEach(i =>
            {
                Lots.Add(i);
            });

            PMSDialogService.Show("结束", "处理结束");

        }

    }
}
