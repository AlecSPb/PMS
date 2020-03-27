using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSClient
{
    public static class PMSDialogService
    {
        public static void UnImplementyet()
        {
            MessageBox.Show("这个功能还没有实现");
        }
        public static bool ShowYesNo(string title, string content)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public static void Show(string title, string content)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Show(string content)
        {
            Show("消息", content);
        }

        public static void ShowWarning(string content)
        {
            MessageBox.Show(content,"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
