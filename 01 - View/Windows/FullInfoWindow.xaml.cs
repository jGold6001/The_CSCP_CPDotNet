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
    /// Interaction logic for FullInfoWindow.xaml
    /// </summary>
    public partial class FullInfoWindow : Window
    {
        public InfoSource InfoSource { get; set; }
        public FullInfoWindow()
        {
            InitializeComponent();
            btnOk.Click += Cancel;
            InfoSource = (InfoSource)App.Current.Resources["infoSource"];
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
