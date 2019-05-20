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
using PMSClient.UserSimpleService;

namespace PMSClient.View
{
    /// <summary>
    /// LogInView.xaml 的交互逻辑
    /// </summary>
    public partial class LogInViewCenter : UserControl
    {
        public LogInViewCenter()
        {
            InitializeComponent();
        }
        public void ClearLog()
        {
            //txtUserName.Text = "";
            txtPassword.Password = "";
            chkRemember.IsChecked = false;
            //forget uid and pwd
            SaveUIDandPWD("", "", false);
        }
        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            //记住密码功能
            if (chkRemember.IsChecked == true)
            {
                SaveUIDandPWD(txtUserName.Text.Trim(), txtPassword.Password, true);
            }
            else
            {
                SaveUIDandPWD("", "", false);
            }

            var uid = txtUserName.Text.Trim();
            var pwd = txtPassword.Password.Trim();
            var userModel = new DcUser() { UserName = uid, Password = pwd };
            if (string.IsNullOrEmpty(uid))
            {
                txtLogInStatus.Text = "请输入用户名";
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                txtLogInStatus.Text = "请输入密码";
                return;
            }
            try
            {
                PMSHelper.CurrentSession.LogIn(uid, pwd);

                if (PMSHelper.CurrentSession.CurrentUser != null)
                {
                    //PMSHelper.CurrentLog.Log("登录成功");
                    NavigationService.GoTo(PMSViews.Navigation);
                }
                else
                {
                    txtLogInStatus.Text = "用户名或者密码错误";
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void SaveUIDandPWD(string uid, string pwd, bool isremember)
        {
            Properties.Settings.Default.UID = uid;
            Properties.Settings.Default.PWD = pwd;
            Properties.Settings.Default.IsUIDPWDRemembered = isremember;
            Properties.Settings.Default.Save();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReadRememberedUIDPWD();
            ReadMessageFromFiles();
        }

        private void ReadMessageFromFiles()
        {
            try
            {
                var updateFile = System.IO.Path.Combine(PMSFolderPath.Roots, "updates.txt");
                if (System.IO.File.Exists(updateFile))
                {
                    txtUpdate.Text = System.IO.File.ReadAllText(updateFile);
                }

                var noticeFile = System.IO.Path.Combine(PMSFolderPath.Documents, "notices.txt");
                if (System.IO.File.Exists(noticeFile))
                {
                    txtNotice.Text = System.IO.File.ReadAllText(noticeFile);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ReadRememberedUIDPWD()
        {
            if (Properties.Settings.Default.IsUIDPWDRemembered)
            {
                txtUserName.Text = Properties.Settings.Default.UID;
                txtPassword.Password = Properties.Settings.Default.PWD;
                chkRemember.IsChecked = Properties.Settings.Default.IsUIDPWDRemembered;
            }
        }

        private void ChkRemember_Click(object sender, RoutedEventArgs e)
        {
            if (chkRemember.IsChecked == false)
            {
                SaveUIDandPWD("", "", false);
            }
        }
    }
}
