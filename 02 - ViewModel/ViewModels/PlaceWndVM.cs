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
            this.BusyPlaces = "Занято: ";
            this.FreePlaces = "Свободно: ";
            this.NumPlace = "Выбрано:";

            listBtns = new List<Button>();
            stylePlace = Application.Current.FindResource("btnPlace") as Style;



            UploadGgidVertical(, 14, 1);
            //UploadGgidVertical(gridSection2, 10, 16);
            //UploadGgidVertical(gridSection3, 10, 26);
            //UploadGgidVertical(gridSection4, 10, 36);
            //UploadGgidVertical(gridSection5, 10, 46);
            //UploadGgidVertical(gridSection6, 10, 56);
            //UploadGgidHorizontal(gridSection7, 14, 66);
        }



        private static Grid grid = null;

        public static readonly DependencyProperty GridProperty = DependencyProperty.RegisterAttached("Grid",
                                                                     typeof(Grid), typeof(PlaceWndVM),
                                                                     new FrameworkPropertyMetadata(OnGridChanged));
        public static void SetGrid(DependencyObject element, Grid value)
        {
            element.SetValue(GridProperty, value);
        }

        public static Grid GetGrid(DependencyObject element)
        {
            return (Grid)element.GetValue(GridProperty);
        }

        public static void OnGridChanged
            (DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            grid = obj as Grid;
        }

        #region Methods

        private void UploadGgidVertical(Grid grid, int amountRows, int startPosition)
        {
            for (int i = 0; i < amountRows; i++)
            {
                RowDefinition rd = new RowDefinition() { Height = new GridLength(35, GridUnitType.Star) };
                Button btn = new Button() { Content = Convert.ToString(i + startPosition) };
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
                return selectCommand ?? (selectCommand = new RelayCommand( () =>
                {
                   
                    
                }));
            }
        }

        #endregion

    }
}
