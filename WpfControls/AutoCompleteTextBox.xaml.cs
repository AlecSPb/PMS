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

namespace WPFControls
{
    /// <summary>
    /// AutoCompleteTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class AutoCompleteTextBox : UserControl
    {
        public AutoCompleteTextBox()
        {
            InitializeComponent();
            pop.IsOpen = false;
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Text = lst.SelectedItem.ToString();
            pop.IsOpen = false;
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = true;
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = false;
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoCompleteTextBox), new PropertyMetadata("",new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AutoCompleteTextBox control = d as AutoCompleteTextBox;
            TextBox txt = control.txt;
            if (e.NewValue!=e.OldValue)
            {
                txt.Text = e.NewValue.ToString();
            }
        }





        public List<string> SelectionDs
        {
            get { return (List<string>)GetValue(SelectionDsProperty); }
            set { SetValue(SelectionDsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionDs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionDsProperty =
            DependencyProperty.Register("SelectionDs", typeof(List<string>), typeof(AutoCompleteTextBox), new PropertyMetadata(null,new PropertyChangedCallback(OnSelectionDsChanged)));

        private static void OnSelectionDsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AutoCompleteTextBox control = d as AutoCompleteTextBox;
            ListBox lst = control.lst;
            if (e.NewValue!=e.OldValue)
            {
                lst.ItemsSource = e.NewValue as List<string>;
            }
        }
    }
}
