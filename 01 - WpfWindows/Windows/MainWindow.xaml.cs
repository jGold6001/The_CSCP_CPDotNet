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
using ViewModels.MainWnd;

namespace _01___WpfWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {          
            InitializeComponent();
        }

      
        private void dgRecords_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "NumberPLace":
                    e.Column.Header = "Место";
                    break;
                case "ClientLastName":
                    e.Column.Header = "Клиент";
                    break;
                case "CarBrand":
                    e.Column.Header = "Автомобиль";
                    break;
                case "DateRegistred":
                    e.Column.Header = "Дата Регистрации";
                    break;
                case "DatePayment":
                    e.Column.Header = "Дата Оплаты";
                    break;
                case "Deposit":
                    e.Column.Header = "Сумма Депозита";
                    break;
                case "Debt":
                    e.Column.Header = "Задолжность";
                    break;


            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dw = new DataWindow();
            dw.ShowDialog();
        }

        private void FreePlace_Click(object sender, RoutedEventArgs e)
        {
            PlaceWindow pw = new PlaceWindow();
            pw.ShowDialog();
        }

        private void btnFullInfo_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
