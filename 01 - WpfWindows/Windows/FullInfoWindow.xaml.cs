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

namespace _01___WpfWindows.Windows
{
    /// <summary>
    /// Interaction logic for FullInfoWindow.xaml
    /// </summary>
    public partial class FullInfoWindow : Window
    {
        public FullInfoWindow()
        {
            InitializeComponent();
            tblPlace.Text = "56";
            tblClient.Text = "Ложкин Жора\nПаспорт: 1234546\nТел: 911\nДата регистрации: 01.09.2017\nInfo:Сайт рыбатекст поможет дизайнеру, верстальщику, вебмастеру сгенерировать";
            tblCar.Text = "BMW Red\nномер: AA345678\nномер кузова: 123456789";
            tblTarriff.Text = "Суточный 500грн.\nДепозит - 1000грн.\nЗадолженность: 200грн.\nДата оплаты: 10.07.2017";
        }
    }
}
