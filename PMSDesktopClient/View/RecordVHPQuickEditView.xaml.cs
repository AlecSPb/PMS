using GalaSoft.MvvmLight.Messaging;
using PMSClient.PMSMainService;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMSClient.View
{
    /// <summary>
    /// RecordVHPQuickEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RecordVHPQuickEditView : UserControl
    {
        public RecordVHPQuickEditView()
        {
            InitializeComponent();
            Refresh();
            //view = (ListCollectionView)CollectionViewSource.GetDefaultView(dgitem.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("CurrentTime", ListSortDirection.Descending));
        }

        public ICollectionView view { get; set; }

        public void Refresh()
        {

        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row.DataContext as DcRecordVHP;
            if (row != null)
            {
                var today = DateTime.Now.Date;
                if (row.PlanDate.Date == today.Date)
                {
                    e.Row.Background = this.FindResource("TodayBrush") as SolidColorBrush;
                }
                else if (row.PlanDate.Date == today.Date.AddDays(-1))
                {
                    e.Row.Background = this.FindResource("YesterdayBrush") as SolidColorBrush;
                }
                else if (row.PlanDate.Date == today.Date.AddDays(1))
                {
                    e.Row.Background = this.FindResource("TomorrowBrush") as SolidColorBrush;
                }
            }


        }

    }
}
