﻿using PMSClient.ToolWindow;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMSClient.View
{
    /// <summary>
    /// MaterialNeedEditView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialInventoryInEditView : UserControl
    {
        public MaterialInventoryInEditView()
        {
            InitializeComponent();
        }

        private void btnToOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PMSClient.ToolWindow.CompositionToOne cto = new ToolWindow.CompositionToOne();
                cto.Show();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void BtnAddRemark_Click(object sender, RoutedEventArgs e)
        {
            var quick = new QuickRemark();
            if (quick.ShowDialog() == true)
            {
                string str = TxtQuickRemark.Text.Trim() + quick.Date.ToString("yyMMdd") + "出" + quick.Value + "kg;";
                PMSMethods.SetTextBox(TxtQuickRemark, str);
            }
        }

        private void BtnMaterialSource_Click(object sender, RoutedEventArgs e)
        {
            string s = $"{TxtMaterialSource.Text}元素=来源;";
            PMSMethods.SetTextBox(TxtMaterialSource, s);
        }
    }
}
