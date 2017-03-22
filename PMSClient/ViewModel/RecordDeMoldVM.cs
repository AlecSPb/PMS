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



        }

        private void ActionEdit(DcRecordDeMold obj)
        {
            throw new NotImplementedException();
        }

        private void ActionAdd()
        {
            var model = new DcRecordDeMold();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = CurrentUserInformation.UserName;
            model.State = PMSCommon.CommonState.UnChecked.ToString();
            NavigationService.GoTo(VToken.RecordBondingEdit, new ModelObject() { IsNew=true,Model=model});
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
