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

namespace The_CSCP
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private UserBuffer userBuff;
        private EFUserOp efUser = new EFUserOp();
        private MainWndAccess access;
        public AuthorizationWindow()
        {
            InitializeComponent();
            btnCancel.Click += Cancel;
            btnOk.Click += Ok;
            tbLogin.KeyUp += LogInKey;
            pbPassword.KeyUp += LogInKey;
            userBuff = (UserBuffer)App.Current.Resources["userBuff"];
        }

       
        #region Methods
        private bool Flag()
        {
            foreach (var item in efUser.GetUsers)
            {
                if((userBuff.Login == item.Login) && (pbPassword.Password == item.Password))
                {
                    userBuff.Position = item.Position.Id;
                    return true;
                }                   
            }
            return false;
        }

        private MainWndAccess SetAccess()
        {
            return (userBuff.Position == 1 ? MainWndAccess.Admin : MainWndAccess.User);
        }

        private void LogIN()
        {
            if (tbLogin.Text == "" || pbPassword.Password == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            { 
                if (Flag())
                {
                    MainWindow mw = new MainWindow(this.SetAccess());
                    mw.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль");
            }
            
        }

        #endregion


        #region Events

        private void LogInKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.LogIN();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.LogIN();
        }
        #endregion
    }

    public enum MainWndAccess
    {
        Admin,
        User
    }
}
