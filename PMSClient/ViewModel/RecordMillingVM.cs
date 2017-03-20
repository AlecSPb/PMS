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
            InitializeProperties();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPageChanged);
        }

        private void ActionPageChanged()
        {
            throw new NotImplementedException();
        }

        private void InitializeProperties()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<DcRecordMilling> RecordMillings { get; set; }

        #region DerivedCommands

        #endregion
    }
}
