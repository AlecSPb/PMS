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
using PMSClient.BasicService;

namespace PMSClient.Tool
{
    /// <summary>
    /// MaterialNeedCalculation.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialNeedCalculationView : UserControl
    {
        public MaterialNeedCalculationView()
        {
            InitializeComponent();
            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(lst1.ItemsSource);
        }

        private bool FilterIt(object obj)
        {
            DcBDCompound compound = (DcBDCompound)obj;
            return compound.MaterialName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
        }

        private ICollectionView _view;

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _view.Filter = new Predicate<object>(FilterIt);
            _view.Refresh();
        }
    }
}
