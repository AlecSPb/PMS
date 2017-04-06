using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldVM : BaseViewModelPage
    {
        public RecordDeMoldVM()
        {

            PageChanged = new RelayCommand(ActionPaging);

            RecordDeMolds = new ObservableCollection<DcRecordDeMold>();
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordDeMold>(ActionEdit);
            SetPageParametersWhenConditionChange();
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);


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
            NavigationService.GoTo(VToken.RecordDeMoldEdit, new ModelObject() { IsNew = false, Model = model });
        }

        private void ActionAdd()
        {
            var model = new DcRecordDeMold();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.CommonState.UnChecked.ToString();
            model.Temperature1 = "10";
            model.Temperature2 = "20";
            model.VHPPlanLot = DateTime.Now.ToString("yyMMdd");
            model.Composition = "成分";
            model.Weight = 0;
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            NavigationService.GoTo(VToken.RecordDeMoldEdit, new ModelObject() { IsNew = true, Model = model });
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordDeMoldServiceClient();
            RecordCount = service.GetRecordDeMoldsCount();
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordDeMoldServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetRecordDeMolds(skip, take);
            RecordDeMolds.Clear();
            models.ToList<DcRecordDeMold>().ForEach(o => RecordDeMolds.Add(o));
        }



        public ObservableCollection<DcRecordDeMold> RecordDeMolds { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDeMold> Edit { get; set; }
    }
}
