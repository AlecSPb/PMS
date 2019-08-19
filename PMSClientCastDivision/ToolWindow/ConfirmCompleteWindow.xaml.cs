﻿using System;
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
    /// ConfirmCompleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmCompleteWindow : Window
    {
        public ConfirmCompleteWindow()
        {
            InitializeComponent();
        }

        public ConfirmModel Model
        {
            get
            {
                ConfirmModel model = new ConfirmModel()
                {
                    MaterialItemLot = txtMaterialLot.Text,
                    Composition = txtComposition.Text,
                    PMINumber = txtPMINumber.Text,
                    Weight = double.Parse(txtWeight.Text),
                    ActualWeight = double.Parse(txtActualWeight.Text),
                    MeltingPoint = txtMeltingPoint.Text,
                    Remark = txtRemark.Text,
                    MaterialSource = txtMaterialSource.Text
                };
                return model;
            }
            set
            {
                txtMaterialLot.Text = value.MaterialItemLot;
                txtComposition.Text = value.Composition;
                txtPMINumber.Text = value.PMINumber;
                txtWeight.Text = value.Weight.ToString();
                txtActualWeight.Text = value.ActualWeight.ToString();
                txtMeltingPoint.Text = value.MeltingPoint;
                txtRemark.Text = value.Remark;
                txtMaterialSource.Text = value.MaterialSource;
            }

        }
        public string SureType { get; set; } = "All";
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSurePart_Click(object sender, RoutedEventArgs e)
        {
            SureType = "Part";
            this.DialogResult = true;
            this.Close();
        }
        private void btnSureAll_Click(object sender, RoutedEventArgs e)
        {
            SureType = "All";
            this.DialogResult = true;
            this.Close();
        }
    }
}
