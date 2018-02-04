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

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// ComplextQueryTool.xaml 的交互逻辑
    /// </summary>
    public partial class ComplexQueryTool : Window
    {
        public ComplexQueryTool()
        {
            InitializeComponent();
            SetAll();
        }

        private void SetAll()
        {
            //TODO:设置好权限
            CheckTheAutherize();
        }

        private void BtnMoveRight_Click(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.WorkArea.Width - this.ActualWidth - 50;
            this.Top = SystemParameters.WorkArea.Top + 80;
        }

        private void CheckTheAutherize()
        {
            var session = PMSHelper.CurrentSession;
            CheckSingle(session, BtnOrder, PMSAccess.ReadOrder);
            CheckSingle(session, BtnMaterialOrderItem, PMSAccess.ReadMaterialOrder);
            CheckSingle(session, BtnMaterialIn, PMSAccess.ReadMaterialInventoryIn);
            CheckSingle(session, BtnMaterialOut, PMSAccess.ReadMaterialInventoryOut);
            CheckSingle(session, BtnMisson, PMSAccess.ReadMisson);
            CheckSingle(session, BtnPlan, PMSAccess.ReadPlan);
            CheckSingle(session, BtnRecordMilling, PMSAccess.ReadRecordMilling);
            CheckSingle(session, BtnRecordVHP, PMSAccess.ReadRecordVHP);
            CheckSingle(session, BtnRecordDeMold, PMSAccess.ReadRecordDeMold);
            CheckSingle(session, BtnRecordMachine, PMSAccess.ReadRecordMachine);
            CheckSingle(session, BtnRecordTest, PMSAccess.ReadRecordTest);
            CheckSingle(session, BtnRecordBonding, PMSAccess.ReadRecordBonding);
            CheckSingle(session, BtnDeliveryItemList, PMSAccess.ReadDelivery);
        }
        private void CheckSingle(Helper.LogInformation session, Button btn, string accesscode)
        {
            btn.IsEnabled = session.IsAuthorized(accesscode);
        }

        private string pminumber;
        private string vhpnumber;

        private void GetTextBox()
        {
            pminumber = TxtPMINumber.Text.Trim();
            vhpnumber = TxtVHPNumber.Text.Trim();
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            GetTextBox();

            Button btn = sender as Button;
            if (btn == null) return;
            string btnName = btn.Name;
            #region Switch区域
            switch (btnName)
            {
                case "BtnOrder":
                    PMSHelper.ViewModels.Order.SetSearch(pminumber);
                    NavigationService.GoTo(PMSViews.Order);
                    break;
                case "BtnMaterialOrderItem":
                    PMSHelper.ViewModels.MaterialOrderItemList.SetSearch(pminumber);
                    NavigationService.GoTo(PMSViews.MaterialOrderItemList);
                    break;
                case "BtnMaterialIn":

                    break;
                case "BtnMaterialOut":

                    break;
                case "BtnMisson":

                    break;
                case "BtnPlan":

                    break;
                case "BtnRecordMilling":

                    break;
                case "BtnRecordVHP":

                    break;
                case "BtnRecordDeMold":

                    break;
                case "BtnRecordMachine":

                    break;
                case "BtnRecordTest":

                    break;
                case "BtnRecordBonding":

                    break;
                case "BtnDeliveryItemList":

                    break;
                default:
                    NavigationService.GoTo(PMSViews.Navigation);
                    break;
            }
            #endregion
        }


    }
}
