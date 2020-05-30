using PMSClient.NewService;
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
using System.Windows.Shapes;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// OrderCheckDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OrderCheckDialog : DialogWindowBase, INotifyPropertyChanged
    {
        public OrderCheckDialog()
        {
            InitializeComponent();


            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderState>(OrderStates);

            OrderPriorities = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderPriority>(OrderPriorities);

            PolicyTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderPolicyType>(PolicyTypes);

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetCurrentOrder(Guid id)
        {
            using (var s = new NewServiceClient())
            {
                CurrentOrder = s.GetOrderByID(id);
                RaisePropertyChanged(nameof(DcOrder));
            }
        }
        public DcOrder CurrentOrder { get; set; }
        public List<string> PolicyTypes { get; set; }
        public List<string> OrderStates { get; set; }
        public List<string> OrderPriorities { get; set; }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOrder == null)
                return;
            if (CurrentOrder.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }

            if (CurrentOrder.State == "最终完成")
            {
                CurrentOrder.FinishTime = DateTime.Now;
            }

            string user = PMSHelper.CurrentSession.CurrentUser.UserName;
            CurrentOrder.Reviewer = user;
            CurrentOrder.ReviewTime = DateTime.Now;
            using (var service = new NewServiceClient())
            {
                service.UpdateOrder(CurrentOrder, user);
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
