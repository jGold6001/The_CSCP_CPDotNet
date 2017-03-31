using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _02___ViewModel
{
    public class PlaceWndVM : ModalWndVM
    {
        List<Button> listBtns;
        Style stylePlace;

        public PlaceWndVM()
        {
                
        }

        public PlaceWndVM(Grid grid1, Grid grid2, Grid grid3, Grid grid4, Grid grid5, Grid grid6, Grid grid7)
        {
            listBtns = new List<Button>();
            stylePlace = Application.Current.FindResource("btnPlace") as Style;

            //UploadGgidVertical(grid1, 14, 1);
            //UploadGgidVertical(grid2, 10, 16);
            //UploadGgidVertical(grid3, 10, 26);
            //UploadGgidVertical(grid4, 10, 36);
            //UploadGgidVertical(grid5, 10, 46);
            //UploadGgidVertical(grid6, 10, 56);
            //UploadGgidHorizontal(grid7, 14, 66);
        }
        #region Methods

        private void UploadGgidVertical(Grid grid, int amountRows, int startPosition)
        {
            for (int i = 0; i < amountRows; i++)
            {
                RowDefinition rd = new RowDefinition() { Height = new GridLength(35, GridUnitType.Star) };
                Button btn = new Button() { Content = Convert.ToString(i + startPosition) };
                btn.Style = stylePlace;
                btn.Command = SelectCommand;
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
                btn.Command = SelectCommand;
                grid.ColumnDefinitions.Add(cd);
                grid.Children.Add(btn);
                Grid.SetColumn(btn, i);
                listBtns.Add(btn);
            }
        }
        #endregion

        #region Properties
        private string numPlace;
        public string NumPlace
        {
            get
            {
                return numPlace;
            }
            set
            {
                numPlace = value;
                RaisePropertyChanged(() => NumPlace);
            }
        }

        public static string SelectNum { get; set; }

        private string freePlaces;
        public string FreePlaces
        {
            get
            {
                return freePlaces;
            }
            set
            {
                freePlaces = value;
                RaisePropertyChanged(() => FreePlaces);
            }
        }

        private string busyPlaces;
        public string BusyPlaces
        {
            get
            {
                return busyPlaces;
            }
            set
            {
                busyPlaces = value;
                RaisePropertyChanged(() => BusyPlaces);
            }
        }



        #endregion

        #region Commands
        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                return selectCommand ?? (selectCommand = new RelayCommand( e =>
                {
                   
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }));
            }
        }

        #endregion

    }
}
