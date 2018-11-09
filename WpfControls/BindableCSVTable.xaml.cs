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
    /// BindableCSVTable.xaml 的交互逻辑
    /// </summary>
    public partial class BindableCSVTable : UserControl
    {
        public BindableCSVTable()
        {
            InitializeComponent();
        }


        public string CSVContent
        {
            get
            {
                return (string)GetValue(CSVContentProperty);
            }
            set
            {
                SetValue(CSVContentProperty, value);
            }
        }

        public static readonly DependencyProperty CSVContentProperty =
              DependencyProperty.Register("CSVContent", typeof(string), typeof(BindableCSVTable),
                  new PropertyMetadata(string.Empty, new PropertyChangedCallback(CSVChanged)));

        private static void CSVChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableCSVTable table = d as BindableCSVTable;
            if (table!=null && e.NewValue!=null)
            {
                table.CSVToTable(e.NewValue.ToString());
            }
            else
            {
                table.ClearDoc();
            }
        }
        private void ClearDoc()
        {
            this.doc.Blocks.Clear();
        }

        private void CSVToTable(string csvString)
        {
            ClearDoc();
            if (string.IsNullOrEmpty(csvString))
            {
                return;
            }

            string[] lines = CSVContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Count()<2)
            {
                try
                {
                    Paragraph p1 = new Paragraph();
                    p1.Inlines.Add(new Run(lines.FirstOrDefault()));
                    p1.Foreground = Brushes.Blue;
                    p1.Margin = new Thickness(5);
                    doc.Blocks.Add(p1);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("xrf error");
                }
                return;
            }
            else
            {
                Table table = new Table();
                table.CellSpacing = 0;
                table.BorderBrush = Brushes.Blue;
                table.BorderThickness = new Thickness(0.5);
                table.Margin = new Thickness(5);
                table.RowGroups.Add(new TableRowGroup());

                foreach (var line in lines)
                {
                    TableRow row = new TableRow();
                    foreach (var item in line.Split(new char[] { ',' }))
                    {
                        TableCell cell = new TableCell();
                        cell.BorderBrush = Brushes.Blue;
                        cell.BorderThickness = new Thickness(0.5);
                        Paragraph p = new Paragraph();
                        p.Inlines.Add(item);
                        cell.Blocks.Add(p);
                        row.Cells.Add(cell);
                    }
                    table.RowGroups[0].Rows.Add(row);
                }
                Paragraph p2 = new Paragraph(new Run("成分测试结果"));
                p2.Foreground = Brushes.Blue;
                p2.Margin = new Thickness(5);
                doc.Blocks.Add(p2);
                doc.Blocks.Add(table);
            }

        }


    }
}
