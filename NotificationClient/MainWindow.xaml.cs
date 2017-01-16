using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NotificationDAL;
using System.Diagnostics;

namespace NotificationClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            now = DateTime.Now;
            LoadNotices();

            SetLoadNoticeTimer();

            SetChangeNoticeTimer();

            (this.FindResource("story") as Storyboard).Begin();
        }

        private DateTime now;


        private void SetLoadNoticeTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 2, 0);
            timer.Tick += (s, e) =>
            {
                LoadNotices();
            };
            timer.Start();

        }



        private void LoadNotices()
        {
            using (var dc = new NoticeDataContext())
            {
                noticeList = dc.Notices.Where(n => n.StartTime <= now && n.EndTime >= now && n.State == 1)
                    .OrderByDescending(n => n.CreateTime).ToList();
            }
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        private List<Notice> noticeList;

        private int count = 0;
        /// <summary>
        /// 定时器，每60s更新TextBlock到下一个消息
        /// </summary>
        private void SetChangeNoticeTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 20);
            timer.Tick += (s, e) =>
            {
                if (count >= noticeList.Count)
                {
                    count = 0;
                }
                txtInformation.Text = noticeList[count].Content;
                Debug.Print(count.ToString());
                Debug.Print(noticeList[count].Content);
                count++;
            };
            timer.Start();
        }


    }
}
