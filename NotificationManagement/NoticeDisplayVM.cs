using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using NotificationDAL;

namespace NotificationManagement
{
    public class NoticeDisplayVM : ViewModelBase
    {
        public NoticeDisplayVM()
        {
            Notices = new ObservableCollection<Notice>();

            using (var dc = new NoticeDataContext())
            {
                var notice = dc.Notices.ToList();
                Notices.Clear();
                notice.ForEach(n => Notices.Add(n));
            }



        }



        public ObservableCollection<Notice> Notices { get; set; }
    }
}
