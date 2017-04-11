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
    public class MaterialInventoryInEditVM : BaseViewModelEdit
    {
        public MaterialInventoryInEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialInventoryIn();
            #region 初始化
            empty.Id = Guid.NewGuid();
            empty.MaterialLot = DateTime.Now.ToString("yyMMdd") + "A";
            empty.Composition = "成分";
            empty.State = PMSCommon.SimpleState.UnDeleted.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.Supplier = "Sanjie";
            empty.Weight = 1;
            empty.Purity = "5N";
            empty.Remark = "无";
            #endregion

            IsNew = true;
            CurrentMaterialInventoryIn = empty;
        }

        public void SetEdit(DcMaterialInventoryIn model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialInventoryIn = model;
            }
        }

        public void SetBySelect(DcOrder order)
        {
            if (order != null)
            {
                //TODO:完成选择填充
                //CurrentMaterialInventoryIn.Composition = order.CompositionStandard;
                //CurrentMaterialInventoryIn.PMINumber = order.PMINumber;
                RaisePropertyChanged(nameof(CurrentMaterialInventoryIn));
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
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialInventoryIn);
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
                    service.AddMaterialInventoryIn(CurrentMaterialInventoryIn);
                }
                else
                {
                    service.UpdateMaterialInventoryIn(CurrentMaterialInventoryIn);
                }
                PMSHelper.ViewModels.MaterialInventoryIn.RefreshData():
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaterialInventoryIn currentMaterialInventoryIn;
        public DcMaterialInventoryIn CurrentMaterialInventoryIn
        {
            get { return currentMaterialInventoryIn; }
            set
            {
                currentMaterialInventoryIn = value;
                RaisePropertyChanged(nameof(CurrentMaterialInventoryIn));
            }
        }
        public ObservableCollection<string> States { get; set; }

        public RelayCommand Select { get; set; }
    }
}
