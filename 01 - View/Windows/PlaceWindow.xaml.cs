using _02___ViewModel;
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

namespace _01___View
{
    /// <summary>
    /// Interaction logic for PlaceWindow.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        public PlaceWindow()
        {
            InitializeComponent();
            this.DataContext = new PlaceWndVM(gridSection1, gridSection2, gridSection3, gridSection4, gridSection5, gridSection6, gridSection7);
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            
        }
    }
}
