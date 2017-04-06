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
    public class MaterialNeedEditVM : BaseViewModelEdit
    {
        public MaterialNeedEditVM()
        {
            CurrentMaterialNeed = new MainService.DcMaterialNeed() { Id = Guid.NewGuid() };
            InitializeProperties();
            InitialCommands();
        }
        public void SetKeyProperties(ModelObject msg)
        {
            IsNew = msg.IsNew;
            CurrentMaterialNeed = msg.Model as DcMaterialNeed;
        }

        public void SetKeyProperties(DcOrder selectOrder)
        {
            if (selectOrder != null)
            {
                CurrentMaterialNeed.Composition = selectOrder.CompositionStandard;
                CurrentMaterialNeed.PMINumber = selectOrder.PMINumber;
                CurrentMaterialNeed.Purity = selectOrder.Purity;

                RaisePropertyChanged(nameof(CurrentMaterialNeed));
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
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialNeed }));
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            var msg = new MsgObject();
            msg.NavigateTo = VToken.OrderSelect;
            msg.NavigateFrom = VToken.MaterialNeedEdit2;
            NavigationService.GoTo(msg);
        }

        private void ActionSave()
        {

            try
            {
                var service = new MaterialNeedServiceClient();
                if (IsNew)
                {
                    service.AddMaterialNeed(CurrentMaterialNeed);
                }
                else
                {
                    service.UpdateMaterialNeed(CurrentMaterialNeed);
                }
                NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialNeed });
                NavigationService.Refresh(VToken.MaterialNeedRefresh);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaterialNeed currentMaterialNeed;
        public DcMaterialNeed CurrentMaterialNeed
        {
            get { return currentMaterialNeed; }
            set
            {
                value = currentMaterialNeed;
                RaisePropertyChanged(nameof(CurrentMaterialNeed));
            }
        }
        public ObservableCollection<string> States { get; set; }

        public RelayCommand Select { get; set; }
    }
}
