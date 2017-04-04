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
            InitializeProperties();
            InitialCommands();
        }
        public void  SetKeyProperties(ModelObject msg)
        {
            IsNew = msg.IsNew;
            CurrentMaterialNeed = msg.Model as DcMaterialNeed;
        }

        private void InitializeProperties()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => States.Add(s));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialNeed }));
            Save = new RelayCommand(ActionSave);
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
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialNeed });
                NavigationService.Refresh(VToken.MaterialNeedRefresh);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public DcMaterialNeed CurrentMaterialNeed { get; set; }
        public ObservableCollection<string> States { get; set; }
    }
}
