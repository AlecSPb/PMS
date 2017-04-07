using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;


namespace PMSClient.ViewModel
{
    public class MaterialInventoryOutEditVM : BaseViewModelEdit
    {
        public MaterialInventoryOutEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialInventoryOut();
            empty.Id = Guid.NewGuid();
            empty.MaterialLot = DateTime.Now.ToString("yyMMdd") + "A";
            empty.Composition = "成分";
            empty.State = PMSCommon.SimpleState.UnDeleted.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Receiver = "B.Lu";
            empty.Weight = 1;
            empty.Purity = "5N";
            empty.Remark = "无";

            IsNew = true;
            CurrentMaterialInventoryOut = empty;
        }

        public void SetEdit(DcMaterialInventoryOut model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialInventoryOut = model;
            }
        }

        public void SetBySelect(DcOrder order)
        {
            if (order != null)
            {
                //TODO:完成选择填充
                //CurrentMaterialInventoryOut.Composition = order.CompositionStandard;
                //CurrentMaterialInventoryOut.PMINumber = order.PMINumber;
                //RaisePropertyChanged(nameof(CurrentMaterialInventoryOut));
            }
        }


        private void InitializeProperties()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => States.Add(s));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryOut));
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            //PMSHelper.ViewModels.MaterialNeedSelect.SetRequestView(PMSViews.MaterialNeedEdit);
            //NavigationService.GoTo(PMSViews.MaterialNeedSelect);
        }

        private void ActionSave()
        {

            try
            {
                var service = new MaterialInventoryServiceClient();
                if (IsNew)
                {
                    service.AddMaterialInventoryOut(CurrentMaterialInventoryOut);
                }
                else
                {
                    service.UpdateMaterialInventoryOut(CurrentMaterialInventoryOut);
                }
                NavigationService.GoTo(PMSViews.MaterialInventoryOut);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaterialInventoryOut currentMaterialInventoryOut;
        public DcMaterialInventoryOut CurrentMaterialInventoryOut
        {
            get { return currentMaterialInventoryOut; }
            set
            {
                currentMaterialInventoryOut = value;
                RaisePropertyChanged(nameof(CurrentMaterialInventoryOut));
            }
        }
        public ObservableCollection<string> States { get; set; }

        public RelayCommand Select { get; set; }
    }
}
