using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PersonalWindow.xaml
    /// </summary>
    public partial class PersonalWindow : Window
    {
        private DialogName name;
        private EFUserOp efUser= new EFUserOp(); 
        public UserBuffer BuffUser;
        public PersonalWindow(DialogName name)
        {
            InitializeComponent();
            this.name = name;
            this.Title = this.Caption;
            BuffUser = (UserBuffer)App.Current.Resources["userBuff"];
            this.LoadComboBoxes();
            this.SetterWnd();
            btnCancel.Click += Cancel;
            btnOk.Click += Ok;
        }
       
        #region Properties
        private bool Flag
        {
            get
            {
                return name == DialogName.EditData ? true : false;
            }
        }
        public string Caption
        {
            get
            {
                if (name == DialogName.AddData)
                    return "Добавить Пользователя";
                else
                    return "Редактировать Пользователя";
            }
        }

        #endregion

        #region Methods
        private List<string> PositionNames
        {
            get
            {
                List<string> positions = new List<string>();
                foreach (var item in efUser.Positions)
                    positions.Add(item.Name);

                return positions;
            }
        }

        private void LoadComboBoxes()
        {          
            BuffUser.Positions = new ObservableCollection<string>(this.PositionNames);
        }

        private void SetterWnd()
        {
            if (this.Flag)
            {
                tbLogin.IsEnabled = false;
                tbName.IsEnabled = false;
                tbSurname.IsEnabled = false;
            }
            else
            {
                BuffUser.PositionName = BuffUser.Positions[1];
                BuffUser.Login = null;
                BuffUser.Password = null;
                BuffUser.LastName = null;
                BuffUser.FirstName = null;          
            }
        }
        #endregion

        #region Events
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            if (BuffUser.FirstName != "" && BuffUser.LastName != "" && BuffUser.Login != "" &&
                BuffUser.Password != "")
            {

                if (Flag)
                {
                    string messege = String.Format("Вы действительно хотите изменить данные пользователя {0}", BuffUser.Login);
                    if (MessageBox.Show(messege, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        this.DialogResult = true;
                }
                else
                {
                    bool flag = false;
                    foreach (var item in efUser.GetUsers)
                    {
                        if (BuffUser.Login == item.Login)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                        this.DialogResult = true;
                    else
                        MessageBox.Show("Заданный логин уже занят");                   
                }                  
            }
            else
                MessageBox.Show("Не все поля заполнены");
        }
        #endregion
    }
}
