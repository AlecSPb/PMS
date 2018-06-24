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
using System.IO;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// EmptyTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class EmptyTextBox : Window
    {
        public EmptyTextBox()
        {
            InitializeComponent();
            Read();
        }

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Write();
        }

        private string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp", "empty.txt");
        private void Read()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string txt = File.ReadAllText(filePath);
                    txtText.Text = txt;
                }
            }
            catch (Exception)
            {

            }
        }

        private void Write()
        {
            try
            {

                File.WriteAllText(filePath, txtText.Text.Trim());

            }
            catch (Exception)
            {

            }
        }

    }
}
