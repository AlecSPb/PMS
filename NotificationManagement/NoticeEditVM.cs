using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using NotificationDAL;
using GalaSoft.MvvmLight.Messaging;


namespace NotificationManagement
{
    public class NoticeEditVM : ViewModelBase
    {
        public NoticeEditVM()
        {
            NoticeTypes = new ObservableCollection<string>();
            NoticeTypes.Clear();
            NoticeTypes.Add("All");
            var noticeTypes = Enum.GetNames(typeof(NoticeType));
            NoticeTypes.ToList().ForEach(n => NoticeTypes.Add(n));

            CurrentNotice = new Notice();


            IntitialCommands();
        }

        private void IntitialCommands()
        {
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave, CanSave);
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            throw new NotImplementedException();
        }

        private void ActionGiveUp()
        {
            Messenger.Default.Send<string>("NoticeDisplayView", NavigateToken.Navigate);
        }

        public ObservableCollection<string> NoticeTypes { get; set; }
        private Notice currentNotice;
        public Notice CurrentNotice
        {
            get { return currentNotice; }
            set
            {
                if (currentNotice == value)
                    return;
                currentNotice = value;
                RaisePropertyChanged(() => CurrentNotice);
            }
        }

        #region Commands
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        #endregion


    }
}
