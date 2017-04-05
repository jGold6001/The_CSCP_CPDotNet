using _03___Model;
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

namespace _01___View
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
            this.DialogResult = true;
        }
        #endregion
    }
}
