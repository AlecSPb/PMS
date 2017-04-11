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
            #region 初始化
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
            #endregion

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

        public void SetBySelect(DcMaterialInventoryIn model)
        {
            if (model != null)
            {
                CurrentMaterialInventoryOut.Composition = model.Composition;
                CurrentMaterialInventoryOut.MaterialLot = model.MaterialLot;
                CurrentMaterialInventoryOut.Weight = model.Weight;
                CurrentMaterialInventoryOut.Remark = model.Supplier;
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
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.MaterialInventoryInSelect.SetRequestView(PMSViews.MaterialInventoryOutEdit);
            NavigationService.GoTo(PMSViews.MaterialInventoryInSelect);
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
                PMSHelper.ViewModels.MaterialInventoryOut.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialInventoryOut);
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
