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

namespace _01___WpfWindows
{
    /// <summary>
    /// Interaction logic for PlaceWindow.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        List<Button> listBtns;
        Style stylePlace;

        public PlaceWindow()
        {
            InitializeComponent();
            listBtns = new List<Button>();
            stylePlace = this.FindResource("btnOrange") as Style;
            UploadGgidVertical(gridSection1, 14, 1);
            UploadGgidVertical(gridSection2, 10, 16);
            UploadGgidVertical(gridSection3, 10, 26);
            UploadGgidVertical(gridSection4, 10, 36);
            UploadGgidVertical(gridSection5, 10, 46);
            UploadGgidVertical(gridSection6, 10, 56);
            UploadGgidHorizontal(gridSection7, 14, 66);       
        }

        private void UploadGgidVertical(Grid grid, int amountRows, int startPosition)
        {
            for (int i = 0; i <amountRows; i++)
            {
                RowDefinition rd = new RowDefinition() { Height = new GridLength(35, GridUnitType.Star) };
                Button btn = new Button() { Content = Convert.ToString(i + startPosition)};
                btn.Style = stylePlace;
                grid.RowDefinitions.Add(rd);           
                grid.Children.Add(btn);
                Grid.SetRow(btn, i);
                listBtns.Add(btn);
            }
        }

        private void UploadGgidHorizontal(Grid grid, int amountRows, int startPosition)
        {
            for (int i = 0; i < amountRows; i++)
            {
                ColumnDefinition cd = new ColumnDefinition() { Width = new GridLength(35, GridUnitType.Star) };
                Button btn = new Button() { Content = Convert.ToString(i + startPosition) };
                btn.Style = stylePlace;
                grid.ColumnDefinitions.Add(cd);
                grid.Children.Add(btn);
                Grid.SetColumn(btn, i);
                listBtns.Add(btn);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
