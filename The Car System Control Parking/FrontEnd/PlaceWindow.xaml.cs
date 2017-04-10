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

namespace The_CSCP
{
    /// <summary>
    /// Interaction logic for PlaceWindow.xaml
    /// </summary>
    public partial class PlaceWindow : Window
    {
        private EFClientOp efClient = new EFClientOp();
        private List<Button> listBtns;
        private Style stylePlace;
        private PlaceWndName name;
        private int countBusyPlaces = 0;
        private int countFreePlaces = 0;
        public InfoSource InfoSource{get;set;} 

        public PlaceWindow(PlaceWndName name)
        {
            InitializeComponent();
            this.name = name;
            this.Title = this.Caption;
            InfoSource = (InfoSource)App.Current.Resources["infoSource"];
            this.SetBtnPlaces();
            this.SetBusyPlaces();          
            this.SetSelectedPlace();
            this.GetCountPlaces();
            this.IfSelectPlaceWnd();
            InfoSource.NumPlace = null;
            btnCancel.Click += Cancel;
            btnOk.Click += Ok;
        }

        #region Properties
        private bool Flag
        {
            get
            {
                return name == PlaceWndName.SelectPlace ? true : false;
            }
        }
        public string Caption
        {
            get
            {
                if (name == PlaceWndName.SelectPlace)
                    return "Выберете место";
                else
                    return "Свободные места";
            }
        }
        #endregion

        #region Methods
        private void SetBtnPlaces()
        {
            listBtns = new List<Button>();
            stylePlace = this.FindResource("btnPlace") as Style;
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
            for (int i = 0; i < amountRows; i++)
            {
                RowDefinition rd = new RowDefinition() { Height = new GridLength(35, GridUnitType.Star) };
                Button btn = new Button() { Content = Convert.ToString(i + startPosition) };
                btn.Style = stylePlace;
                btn.Click += SelectionPlace;
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
                btn.Click += SelectionPlace;
                grid.ColumnDefinitions.Add(cd);
                grid.Children.Add(btn);
                Grid.SetColumn(btn, i);
                listBtns.Add(btn);
            }
        }
        private void SetBusyPlaces()
        {
            foreach (Button button in listBtns)
            {
                int btnNum = Convert.ToInt32(button.Content);
                foreach (var place in efClient.GetPlaces)
                {                   
                    if (btnNum == place.Number)
                    {
                        button.IsEnabled = false;
                        countBusyPlaces++;
                    }
                }
            }
        }
        private void GetCountPlaces()
        {
            countFreePlaces = listBtns.Count - countBusyPlaces;
            InfoSource.BusyPlaces = Convert.ToString(countBusyPlaces);
            InfoSource.FreePlaces = Convert.ToString(countFreePlaces);
        }
        private void SetSelectedPlace()
        {
            if(InfoSource.NumPlace !=null)
            {
                foreach (Button item in listBtns)
                {
                    if (item.Content.ToString() == InfoSource.NumPlace)
                    {
                            item.Focus();
                            break;
                    }
                                    
                }
            }
        }
        private void IfSelectPlaceWnd()
        {
            if(!Flag)
            {
                btnCancel.Visibility = Visibility.Hidden;
                lSelect.Visibility = Visibility.Hidden;               
                foreach (Button item in listBtns)
                    item.IsHitTestVisible = false;
            }
        }
        #endregion

        #region Events

        private void SelectionPlace(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            InfoSource.NumPlace = btn.Content.ToString();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            InfoSource.NumPlace = null;
            this.Close();
        }
        private void Ok(object sender, RoutedEventArgs e)
        {
            if (Flag)
            {
                if (InfoSource.NumPlace != null)
                    this.DialogResult = true;
                else
                    MessageBox.Show("Выберете номер места");
            }              
            else
                this.Close();
        }
        #endregion
    }

    public enum PlaceWndName
    {
        FreePlace,
        SelectPlace
    }
}
