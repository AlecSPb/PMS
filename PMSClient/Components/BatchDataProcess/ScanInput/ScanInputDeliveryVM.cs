using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace PMSClient.DataProcess.ScanInput
{
    public class ScanInputDeliveryVM : ScanInputVMBase
    {
        public ScanInputDeliveryVM(DcDelivery delivery)
        {
            if (delivery == null)
                throw new ArgumentNullException();

            SourceTarget = $"产品记录+背板记录 => 发货记录[{delivery.DeliveryName}]";
            process = new ProcessDelivery(delivery);

            Process = new RelayCommand(ActionProcess,CanCheck);
            Check = new RelayCommand(ActionCheck,CanCheck);
            Lots = new ObservableCollection<LotModel>();
        }



        private bool CanCheck()
        {
            return canClick;
        }

        private ProcessDelivery process;

        private void ActionCheck()
        {
            ClearLots();
            Task task = new Task(() =>
            {
                canClick = false;
                process.Intialize(InputText);
                process.Number = CurrentValue;
                process.DeliveryType = CurrentText;
                process.CheckExist = CurrentCheck;


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
            ClearLots();
            Task task = new Task(() =>
            {
                canClick = false;
                process.Intialize(InputText);
                process.Number = CurrentValue;
                process.DeliveryType = CurrentText;
                process.CheckExist = CurrentCheck;


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
        private void ClearLots()
        {
            Lots.Clear();
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
