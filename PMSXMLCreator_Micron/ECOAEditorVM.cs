using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSXMLCreator_Micron.Model;
using PMSXMLCreator_Micron.Service;
using GalaSoft.MvvmLight.Messaging;

namespace PMSXMLCreator_Micron
{
    public class ECOAEditorVM : ViewModelBase
    {
        public ECOAEditorVM()
        {
            Headers = new ObservableCollection<BasicInfoField>();
            Contents = new ObservableCollection<InspectionItem>();
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
        {
            var model = new Micon_COA();
            model.Header = Headers.ToList();
            model.InspectionItems = Contents.ToList();
            Messenger.Default.Send<Micon_COA>(model, "SaveString");
        }

        public ObservableCollection<BasicInfoField> Headers { get; set; }
        public ObservableCollection<InspectionItem> Contents { get; set; }


        public RelayCommand Save { get; set; }
    }
}
