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
    public class NoticeDisplayVM : ViewModelBase
    {
        public NoticeDisplayVM()
        {
            Notices = new ObservableCollection<Notice>();

            NoticeTypes = new ObservableCollection<string>();
            NoticeTypes.Clear();
            NoticeTypes.Add("All");
            var noticeTypes = Enum.GetNames(typeof(NoticeType));
            noticeTypes.ToList().ForEach(n => NoticeTypes.Add(n));

            SearchContent = "";

            LoadNotices();



            IntitialCommands();
        }

        private void IntitialCommands()
        {
            Add = new RelayCommand<Notice>(ActionAdd);
            Update = new RelayCommand<Notice>(ActionUpdate);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
        }

        private bool CanSearch()
        {
            return string.IsNullOrEmpty(SearchContent);
        }

        private void ActionUpdate(Notice obj)
        {
            Messenger.Default.Send<Notice>(obj, NavigateToken.Navigate);
        }

        private void ActionAdd(Notice obj)
        {
            Messenger.Default.Send<Notice>(null, NavigateToken.Navigate);
        }

        private void ActionAll()
        {
            LoadNotices();
        }

        private void ActionSearch()
        {
            LoadNotices();
        }

        private void LoadNotices()
        {
            using (var dc = new NoticeDataContext())
            {
                var notice = dc.Notices.ToList();
                Notices.Clear();
                notice.ForEach(n => Notices.Add(n));
            }
        }

        #region Properties
        public ObservableCollection<string> NoticeTypes { get; set; }
        public ObservableCollection<Notice> Notices { get; set; }
        private string searchContent;
        public string SearchContent
        {
            get { return searchContent; }
            set
            {
                if (searchContent == value)
                    return;
                searchContent = value;
                RaisePropertyChanged(() => SearchContent);
            }
        }

        #endregion



        #region Commands
        public RelayCommand<Notice> Add { get; set; }
        public RelayCommand<Notice> Update { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand All { get; set; }
        #endregion
    }
}
