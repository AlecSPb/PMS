using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient.ViewModel
{
    public class MaterialNeedEditVM : ViewModelBase
    {
        private bool isNew;
        public MaterialNeedEditVM(MessageObject msg)
        {
            isNew = msg.IsAdd;
            CurrentMaterialNeed = msg.ModelObject as DcMaterialNeed;
            InitialCommands();
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialNeed));
            Save = new RelayCommand<DcMaterialNeed>(ActionSave);
        }

        private void ActionSave(DcMaterialNeed obj)
        {
            if (obj!=null)
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
        }

        public DcMaterialNeed CurrentMaterialNeed { get; set; }

        public RelayCommand GiveUp { get; private set; }
        public RelayCommand<DcMaterialNeed> Save { get; private set; }
    }
}
