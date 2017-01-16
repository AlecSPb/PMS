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
        private bool IsNew;
        public NoticeEditVM(Notice model)
        {
            NoticeTypes = new ObservableCollection<string>();
            NoticeTypes.Clear();
            var noticeTypes = Enum.GetNames(typeof(NoticeType));
            noticeTypes.ToList().ForEach(n => NoticeTypes.Add(n));

            NoticeState = new ObservableCollection<int>();
            NoticeState.Clear();
            var states = Enum.GetValues(typeof(NoticeState));
            foreach (var item in states)
            {
                NoticeState.Add((int)item);
            }

            NoticePriority = new ObservableCollection<int>();
            NoticePriority.Clear();
            var priority = Enum.GetValues(typeof(NoticePriority));
            foreach (var item in priority)
            {
                NoticePriority.Add((int)item);
            }

            if (model != null)
            {
                CurrentNotice = model;
                IsNew = false;
            }
            else
            {

                IsNew = true;
                var notice = new Notice();
                notice.ID = Guid.NewGuid();
                notice.StartTime = DateTime.Now;
                notice.EndTime = DateTime.Now.AddDays(7);
                notice.Content = "";
                notice.CreateTime = DateTime.Now;
                notice.Creator = "xs.zhou";
                notice.State = 1;
                notice.Priority = 1;
                notice.Type = Enum.GetName(typeof(NoticeType), 0);

                CurrentNotice = notice;
            }


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
            int result = 0;
            if (IsNew)
            {
                using (var dc = new NoticeDataContext())
                {
                    dc.Notices.Add(CurrentNotice);
                    result = dc.SaveChanges();
                }
            }
            else
            {
                using (var dc = new NoticeDataContext())
                {
                    dc.Entry(CurrentNotice).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                }
            }

            if (result > 0)
            {
                Messenger.Default.Send<string>("NoticeDisplayView", NavigateToken.Navigate);
            }

        }

        private void ActionGiveUp()
        {
            Messenger.Default.Send<string>("NoticeDisplayView", NavigateToken.Navigate);
        }

        public ObservableCollection<string> NoticeTypes { get; set; }
        public ObservableCollection<int> NoticeState { get; set; }
        public ObservableCollection<int> NoticePriority { get; set; }
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
