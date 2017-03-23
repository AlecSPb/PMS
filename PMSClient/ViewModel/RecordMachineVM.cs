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
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordDeMold>(ActionEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            SetPageParametersWhenConditionChange();
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcRecordDeMold model)
        {
            NavigationService.GoTo(VToken.RecordMachineEdit, new ModelObject() { IsNew = false, Model = model });
        }

        private void ActionAdd()
        {
            var model = new DcRecordMachine();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = CurrentUserInformation.UserName;
            model.State = PMSCommon.CommonState.Checked.ToString();
            model.VHPPlanLot = DateTime.Now.ToString("yyMMdd");
            model.Composition = "成分";
            model.Diameter1 = 0;
            model.Diameter2 = 0;

            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.ExtraRequirement = "无缺口无划痕";

            NavigationService.GoTo(VToken.RecordMachineEdit, new ModelObject() { IsNew = true, Model = model });
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

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDeMold> Edit { get; set; }
    }
}
