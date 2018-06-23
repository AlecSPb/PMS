using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.DataProcess;

namespace PMSClient.DataProcess.ScanInput
{
    public class ScanInputRecordBondingVM : ScanInputVMBase
    {
        public ScanInputRecordBondingVM()
        {
            SourceTarget = "测试记录 => 绑定记录";
            process = new ProcessRecordBonding();

            Process = new RelayCommand(ActionProcess);
            Check = new RelayCommand(ActionCheck);
            Lots = new ObservableCollection<LotModel>();
        }

        private ProcessRecordBonding process;

        private void ActionCheck()
        {
            Task task = new Task(() =>
              {
                  process.Intialize(InputText);

                  process.Check(i =>
                  {
                      ProgressValue = i;
                  });
                  App.Current.Dispatcher.Invoke(() =>
                  {
                      RefreshLotsStatus();

                  });
              });
            task.Start();
        }

        private void ActionProcess()
        {
            if (PMSDialogService.ShowYesNo("请问", "确定继续吗？") == false)
                return;

            Task task = new Task(() =>
              {
                  process.Intialize(InputText);

                  process.Process(i =>
                  {
                      ProgressValue = i;
                  });
                  App.Current.Dispatcher.Invoke(() =>
                  {
                      RefreshLotsStatus();

                  });
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
