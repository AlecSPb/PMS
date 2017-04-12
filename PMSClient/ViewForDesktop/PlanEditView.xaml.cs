using PMSClient.MainService;
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
using PMSClient.BasicService;

namespace PMSClient.ViewForDesktop
{
    /// <summary>
    /// PlanEditView.xaml 的交互逻辑
    /// </summary>
    public partial class PlanEditView : UserControl
    {
        public PlanEditView()
        {
            InitializeComponent();
        }

        private void cboMolds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMold =cboMolds.SelectedItem as DcBDVHPMold;
            if (selectedMold!=null)
            {
                this.moldDiameter.Text = selectedMold.InnerDiameter.ToString();
                this.moldType.Text = selectedMold.MoldType;
            }
        }

        private void cboCompounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCompound = cboCompounds.SelectedItem as DcBDCompound;
            if (selectedCompound!=null)
            {
                txtCalculationDensity.Text = selectedCompound.Density.ToString();
            }
        }

        private void cboGrainSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            if (cbo.SelectedItem!=null)
            {
                txtGrainSize.Text = cbo.SelectedItem.ToString();
            }
        }
    }
}
