using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordMachineVM : BaseViewModelPage
    {
        public RecordMachineVM()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordMachineServiceClient();
            RecordCount = service.GetRecordMachineCount();
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordMachineServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetRecordMachines(skip, take);
            RecordMachines.Clear();
            models.ToList<DcRecordMachine>().ForEach(o => RecordMachines.Add(o));
        }



        public ObservableCollection<DcRecordMachine> RecordMachines { get; set; }
    }
}
