using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class MaterialNeedEditVM : ViewModelBase
    {
        private bool isNew;
        public MaterialNeedEditVM(MessageObject msg)
        {
            isNew = msg.IsAdd;
            CurrentMaterialNeed = msg.ModelObject as DcMaterialNeed;
            InitializeProperties();
            InitialCommands();
        }

        private void InitializeProperties()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.NonOrderState));
            states.ToList().ForEach(s => States.Add(s));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialNeed));
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
            NavigationService.GoTo(VNCollection.MaterialNeed);

        }

        public DcMaterialNeed CurrentMaterialNeed { get; set; }
        public ObservableCollection<string> States { get; set; }
        public RelayCommand GiveUp { get; private set; }
        public RelayCommand Save { get; private set; }
    }
}
