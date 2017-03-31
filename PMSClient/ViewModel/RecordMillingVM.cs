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
            RecordMillings = new ObservableCollection<MainService.DcRecordMilling>();
            SetPageParametersWhenConditionChange();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordMilling>(ActionEdit);
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

        private void ActionEdit(DcRecordMilling model)
        {
            if (model != null)
            {
                NavigationService.GoTo(VToken.RecordMillingEdit, new ModelObject() { IsNew = false, Model = model });
            }
        }

        private void ActionAdd()
        {
            var model = new DcRecordMilling();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentLogInformation.CurrentUser.UserName;
            model.Composition = "Cu22.8In20Ga7.0Se50.2";
            model.GasProtection = "Ar";
            model.MillingTool = "行星球磨机";
            model.WeightIn = 0;
            model.WeightOut = 0;
            model.WeightRemain = 0;

            NavigationService.GoTo(VToken.RecordMillingEdit, new ModelObject() { IsNew = true, Model = model });
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
