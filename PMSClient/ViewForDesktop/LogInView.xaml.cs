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
using PMSClient.UserService;

namespace PMSClient.ViewForDesktop
{
    /// <summary>
    /// LogInView.xaml 的交互逻辑
    /// </summary>
    public partial class LogInView : UserControl
    {
        public LogInView()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
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
                var logInformation = PMSHelper.CurrentLogInformation;
                logInformation.LogIn(uid, pwd);

                if (logInformation.CurrentUser != null)
                {
                    NavigationService.GoTo(VToken.Navigation);
                    PMSHelper.CurrentLog.Log("登录成功");
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
    }
}
