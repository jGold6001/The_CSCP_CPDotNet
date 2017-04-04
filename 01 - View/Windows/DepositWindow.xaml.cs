using System;
using System.Collections.Generic;
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
    /// Interaction logic for DepositWindow.xaml
    /// </summary>
    public partial class DepositWindow : Window
    {
        public InfoSource InfoSource
        {
            get; set;
        }

        public DepositWindow()
        {
            InitializeComponent();
            btnCancel.Click += Cancel;
            btnOk.Click +=Ok;
            InfoSource = (InfoSource)App.Current.Resources["infoSource"];
        }

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
        #endregion

    }
}
