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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SimplePager : UserControl, ICommandSource
    {
        public SimplePager()
        {
            InitializeComponent();
        }

        #region 分页属性

        public int PageIndex
        {
            get
            {
                return (int)GetValue(PageIndexProperty);
            }
            set
            {
                SetValue(PageIndexProperty, value);
            }
        }

        public static readonly DependencyProperty PageIndexProperty =
              DependencyProperty.Register("PageIndex", typeof(int), typeof(SimplePager), new PropertyMetadata(1));


        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        public static readonly DependencyProperty PageSizeProperty =
              DependencyProperty.Register("PageSize", typeof(int), typeof(SimplePager), new PropertyMetadata(20, PageSizePropertyChanged,CoercePageSize));
        private static object CoercePageSize(DependencyObject d, object baseValue)
        {
            int newvalue = (int)baseValue;
            if (newvalue<5)
            {
                newvalue = 5;
            }
            return newvalue;
        }

        private static void PageSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimplePager pager)
            {
                //pager.CalulatePagingParameter();
            }
        }

        /// <summary>
        ///控件外部不能访问
        /// </summary>
        private int PageCount
        {
            get; set;
        }

        public int RecordCount
        {
            get
            {
                return (int)GetValue(RecordCountProperty);
            }
            set
            {
                SetValue(RecordCountProperty, value);
            }
        }

        public static readonly DependencyProperty RecordCountProperty =
              DependencyProperty.Register("RecordCount", typeof(int), typeof(SimplePager),
                  new PropertyMetadata(0, new PropertyChangedCallback(RecordCountChanged)));

        private static void RecordCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SimplePager pager)
            {
                pager.CalulatePagingParameter();
            }
        }
        /// <summary>
        /// 初始化数据
        /// 只有在PageSize和RecordCount的值发生变化的时候进行
        /// </summary>
        private void CalulatePagingParameter()
        {
            //只要一发生设置，就说明空间需要初始化了,PageSize和RecordCount都是外部设置的
            PageIndex = 1;
            //根据RecordCount计算PageCount
            //当记录数少于页大小的时候
            //记录数少于
            if (RecordCount <= PageSize)
            {
                PageCount = 1;
            }
            else
            {
                PageCount = RecordCount % PageSize > 0 ? (RecordCount / PageSize) + 1 : RecordCount / PageSize;
            }
            SetStatus();
            SetButtonEnable();
        }
        #endregion

        #region 设置控件文字和按钮
        private void SetButtonEnable()
        {
            this.btnFirst.IsEnabled = CanGoFirstOrPrev();
            this.btnPrev.IsEnabled = CanGoFirstOrPrev();

            this.btnNext.IsEnabled = CanGoNextOrLast();
            this.btnLast.IsEnabled = CanGoNextOrLast();
        }


        private bool CanGoFirstOrPrev()
        {
            return PageIndex > 1;
        }

        private bool CanGoNextOrLast()
        {
            return PageIndex < PageCount;
        }

        private void SetStatus()
        {
            txtPageInformation.Text = $"{LabelPageIndex}={PageIndex},{LabelPageCount}={PageCount},{LabelRecordCount}={RecordCount},{LabelPageSize}={PageSize}";
        }
        #endregion

        #region CommandSource部分
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty =
              DependencyProperty.Register("Command", typeof(ICommand), typeof(SimplePager), new PropertyMetadata((ICommand)null));



        public object CommandParameter
        {
            get
            {
                return (object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public static readonly DependencyProperty CommandParameterProperty =
              DependencyProperty.Register("CommandParameter", typeof(object), typeof(SimplePager), new PropertyMetadata(null));


        public IInputElement CommandTarget
        {
            get
            {
                return this;
            }
        }

        private void OnCommandExecute()
        {
            if (Command != null)
            {
                Command.Execute(CommandParameter);
                SetButtonEnable();
                SetStatus();
            }
        }
        private void OnCommandCanExecute()
        {
            if (Command != null)
            {
                this.IsEnabled = Command.CanExecute(CommandParameter);
            }
        }

        #endregion

        #region 按钮事件
   private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
            OnCommandExecute();
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex > 0)
            {
                PageIndex--;
            }
            OnCommandExecute();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex < PageCount)
            {
                PageIndex++;
            }
            OnCommandExecute();
        }

        private void BtnLast_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = PageCount;
            OnCommandExecute();
        }
        #endregion
     
        #region 标签属性，用于中英文内容设置

        public string LabelPageIndex
        {
            get
            {
                return (string)GetValue(LabelPageIndexProperty);
            }
            set
            {
                SetValue(LabelPageIndexProperty, value);
            }
        }

        public static readonly DependencyProperty LabelPageIndexProperty =
              DependencyProperty.Register("LabelPageIndex", typeof(string), typeof(SimplePager), new PropertyMetadata("Current Page Index"));

        public string LabelPageCount
        {
            get
            {
                return (string)GetValue(LabelPageCountProperty);
            }
            set
            {
                SetValue(LabelPageCountProperty, value);
            }
        }

        public static readonly DependencyProperty LabelPageCountProperty =
              DependencyProperty.Register("LabelPageCount", typeof(string), typeof(SimplePager), new PropertyMetadata("Total Pages"));


        public string LabelRecordCount
        {
            get
            {
                return (string)GetValue(LabelRecordCountProperty);
            }
            set
            {
                SetValue(LabelRecordCountProperty, value);
            }
        }

        public static readonly DependencyProperty LabelRecordCountProperty =
              DependencyProperty.Register("LabelRecordCount", typeof(string), typeof(SimplePager), new PropertyMetadata("Total Record Count"));


        public string LabelPageSize
        {
            get
            {
                return (string)GetValue(LabelPageSizeProperty);
            }
            set
            {
                SetValue(LabelPageSizeProperty, value);
            }
        }

        public static readonly DependencyProperty LabelPageSizeProperty =
              DependencyProperty.Register("LabelPageSize", typeof(string), typeof(SimplePager), new PropertyMetadata("Page Size"));

        #endregion


    }
}
