using _03___Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        DialogName name;
        public DataBuff Buffer { get; set; }
        private EFClientOp efClient = new EFClientOp();
        private readonly string defaultText = "Выбрать...";

        public DataWindow(DialogName name)
        {
            InitializeComponent();
            this.name = name;
            Buffer = (DataBuff)App.Current.Resources["dataBuff"];
            this.Title = this.Caption;
            btnCancel.Click += Cancel;
            btnOk.Click += Ok;
            this.LoadComboBoxes();
            this.SetterWnd();
            btnPlace.Click += BtnPlace_Click;
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
                    return "Добавить Клиента";
                else 
                    return "Редактировать Клиента";
            }
        }

        #endregion

        #region Methods

        private List<string> RentNames
        {
            get
            {
                List<string> rentNames = new List<string>();
                foreach (var item in efClient.RentTypes)
                    rentNames.Add(item.Name);

                return rentNames;
            }
        }

        private void LoadComboBoxes()
        {
            Buffer.Brands = new ObservableCollection<string>(JSONReader.Brands());
            Buffer.Colors = new ObservableCollection<string>(JSONReader.Colors());
            Buffer.RentTypes = new ObservableCollection<string>(this.RentNames);

        }

        private void SetterWnd()
        {
            if (this.Flag)
            {
                btnPlace.IsEnabled = false;
                tbDeposit.IsEnabled = false;
            }
            else
            {
                Buffer.NumPlace = defaultText;
                Buffer.Brand = Buffer.Brands[0];
                Buffer.Color = Buffer.Colors[0];
                Buffer.RentType = Buffer.RentTypes[0];
            }
                
                         
        }

        #endregion

        #region Events
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            PlaceWindow pw = new PlaceWindow(PlaceWndName.SelectPlace);            
            if(pw.ShowDialog()==true)
            {
                Buffer.NumPlace = pw.InfoSource.NumPlace;
            }            
        }

        #endregion
    }


    public enum DialogName
    {
        AddData,
        EditData
    }
}
