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
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;


namespace The_CSCP
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ObservableCollection<UserExtend> listUsers;
        private UserExtend SelectUser { get; set; }
        private UserBuffer userBuff;
        private EFUserOp efUser = new EFUserOp();
        private EFClientOp efClient = new EFClientOp();
        private decimal dailyPrice, monthPrice;
        private int dailyID, monthID;
        private RentValueEditor rvEditor= new RentValueEditor();

        public AdminWindow()
        {
            InitializeComponent();
            this.DGLoad();
            this.SetClickEvents();
            this.BtnDisabled();
            this.GetTariffs();
        }

        #region User_Events
        private void DgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectUser = dgEmployee.SelectedItem as UserExtend;
            if (SelectUser != null)
                this.LoadBuffer();

            this.BtnEnabled();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PersonalWindow pw = new PersonalWindow(DialogName.AddData);
            if (pw.ShowDialog() == true)
            {
                User user = new User()
                {
                    FirstName = pw.BuffUser.FirstName,
                    LastName = pw.BuffUser.LastName,
                    Login = pw.BuffUser.Login,
                    Password = pw.BuffUser.Password,
                    Position = this.SetPosition(pw.BuffUser.PositionName)
                };              
                efUser.Add(user);
                this.CreateUserExetend(user);
                this.BtnDisabled();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            PersonalWindow pw = new PersonalWindow(DialogName.EditData);
            pw.BuffUser.LastName = userBuff.LastName;
            pw.BuffUser.FirstName = userBuff.FirstName;
            pw.BuffUser.Login = userBuff.Login;
            pw.BuffUser.Password = userBuff.Password;
            pw.BuffUser.PositionName = userBuff.PositionName;

            if (pw.ShowDialog() == true)
            {
                User user = new User()
                {
                    FirstName = pw.BuffUser.FirstName,
                    LastName = pw.BuffUser.LastName,
                    Login = pw.BuffUser.Login,
                    Password = pw.BuffUser.Password,
                    Position = this.SetPosition(pw.BuffUser.PositionName)
                };

                efUser.Edit(user);
                this.UpdateBuffer(user);
                this.UpdateRecord();
                userBuff = null;
            }
            dgEmployee.SelectedItem = SelectUser;
                    
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            efUser.Remove(SelectUser);
            listUsers.Remove(SelectUser);
            this.BtnDisabled();
        }
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region Tariff_Events
       
        private void BtnDaily_Click(object sender, RoutedEventArgs e)
        {
            dailyPrice = Convert.ToDecimal(tbDaily.Text);
            if (dailyPrice != efClient.RentTypes[0].Price)
            {
                string messege = String.Format("Установить сумму {0} грн. на тариф {1}?", tbDaily.Text, efClient.RentTypes[0].Name);
                if (MessageBox.Show(messege, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    rvEditor.SetPrice(dailyID, dailyPrice);
                }
            }
            else
                MessageBox.Show("Сумма уже установленна, введите другое значение");
        }

        private void BtnMonth_Click(object sender, RoutedEventArgs e)
        {
            monthPrice = Convert.ToDecimal(tbMonth.Text);
            if (monthPrice != efClient.RentTypes[1].Price)
            {
                string messege = String.Format("Установить сумму {0} грн. на тариф {1}?", tbMonth.Text, efClient.RentTypes[1].Name);
                if (MessageBox.Show(messege, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    rvEditor.SetPrice(monthID, monthPrice);
                }
            }
            else
                MessageBox.Show("Сумма уже установленна, введите другое значение");
            
        }
        #endregion

        #region Methods
        private void SetClickEvents()
        {
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            dgEdit.Click += btnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            dgDelete.Click += BtnDelete_Click;

            btnDaily.Click += BtnDaily_Click;
            btnMonth.Click += BtnMonth_Click;           
        }
        private void BtnDisabled()
        {
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
        private void BtnEnabled()
        {
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
        }
        private void DGLoad()
        {
            listUsers = new ObservableCollection<UserExtend>(efUser.UserList);
            dgEmployee.ItemsSource = listUsers;
            dgEmployee.SelectionChanged += DgEmployee_SelectionChanged;
        }
        private void LoadBuffer()
        {
            foreach (var item in efUser.GetUsers)
            {
                if (item.Login == SelectUser.Login)
                {
                    userBuff = new UserBuffer()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Login = item.Login,
                        Password = item.Password,
                        Position = item.Position.Id,
                        PositionName = item.Position.Name
                    };
                    break;
                }
            }
        }
        private void UpdateBuffer(User user)
        {
            foreach (var item in efUser.GetUsers)
            {
                if (item.Login == user.Login)
                {
                    userBuff = new UserBuffer()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Login = user.Login,
                        Password = user.Password,
                        Position = user.Position.Id,
                        PositionName = user.Position.Name
                    };
                    break;
                }
            }
        }
        private void UpdateRecord()
        {
            UserExtend userEx = new UserExtend()
            {
                FirstName = userBuff.FirstName,
                LastName = userBuff.LastName,
                Login = userBuff.Login,
                Password = userBuff.Password,
                Position = userBuff.PositionName
            };

            foreach (var item in listUsers)
            {
                if (item.Login == userEx.Login)
                {
                    listUsers.Remove(item);
                    listUsers.Add(userEx);
                    break;
                }
            }
            SelectUser = userEx;
        }
        private void CreateUserExetend(User user)
        {
            UserExtend userEx = new UserExtend()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Position = user.Position.Name
            };
            listUsers.Add(userEx);
            SelectUser = userEx;

        }
        private Position SetPosition(string positionName)
        {
            foreach (var item in efUser.Positions)
            {
                if (item.Name == positionName)
                    return item;
            }
            return null;
        }
        private void GetTariffs()
        {
            tbDaily.Text = efClient.RentTypes[0].Price.ToString();
            dailyID = efClient.RentTypes[0].Id;
            tbMonth.Text = efClient.RentTypes[1].Price.ToString();
            monthID = efClient.RentTypes[1].Id;
        }
        #endregion
    }
}
