using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;



namespace PMSClient.ViewModel
{
    public class RecordMillingVM : BaseViewModelPage
    {
        public RecordMillingVM()
        {
            SetPageParametersWhenConditionChange();
            
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordMilling>(ActionEdit);
        }

        private void ActionEdit(DcRecordMilling obj)
        {
            throw new NotImplementedException();
        }

        private void ActionAdd()
        {
            NavigationService.GoTo(VToken.RecordMillingEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordMillingServiceClient();
            RecordCount = service.GetRecordMillingCount();
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordMillingServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetRecordMillings(skip, take);
            RecordMillings.Clear();
            models.ToList<DcRecordMilling>().ForEach(o => RecordMillings.Add(o));
        }



        #region DerivedPart
        public ObservableCollection<DcRecordMilling> RecordMillings { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMilling> Edit { get; set; }
        #endregion
    }
}
