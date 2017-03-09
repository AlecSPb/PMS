using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class MaterialNeedEditVM : ViewModelBase
    {
        private bool isNew;
        public MaterialNeedEditVM(ModelObject msg)
        {
            isNew = msg.IsNew;
            CurrentMaterialNeed = msg.Model as DcMaterialNeed;
            InitializeProperties();
            InitialCommands();
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

            var service = new MaterialNeedServiceClient();
            if (isNew)
            {
                service.AddMaterialNeed(CurrentMaterialNeed);
            }
            else
            {
                service.UpdateMaterialNeed(CurrentMaterialNeed);
            }
            NavigationService.GoTo(new MsgObject() { MsgToken=VToken.MaterialNeed});
            NavigationService.Refresh(VToken.MaterialNeedRefresh);
        }

        public DcMaterialNeed CurrentMaterialNeed { get; set; }
        public ObservableCollection<string> States { get; set; }
        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }
    }
}
