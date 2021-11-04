using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace PMSQuotation
{
    /// <summary>
    /// DataDictionaryView.xaml 的交互逻辑
    /// </summary>
    public partial class DataDictionaryView : Window
    {
        public DataDictionaryView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "CloseDataDictionaryWindow")
            {
                this.Close();
            }
        }
    }
}
