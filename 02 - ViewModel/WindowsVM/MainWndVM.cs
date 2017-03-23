using _03___Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02___ViewModel
{
    public class MainWndVM : INotifyPropertyChanged
    {
       
        private EFClientOp efClient = new EFClientOp();
        private ObservableCollection<Record> listRecords;

        public MainWndVM()
        {
            RecordSource = new ObservableCollection<Record>(efClient.RecordList);
            GridVisibility = Visibility.Hidden;
        }



        #region DataGrid;
        public ObservableCollection<Record> RecordSource
        {
            get
            {
                return listRecords;
            }
            set
            {
                listRecords = value;
                OnPropertyChanged("ListRecords");
            }
        }

        private RelayCommand<SelectionChangedEventArgs> selectCommand;
        public RelayCommand<SelectionChangedEventArgs> SelectCommand
        {
            get
            {
                return selectCommand ?? (selectCommand = new RelayCommand<SelectionChangedEventArgs>(obj =>
                {
                    this.NumPlace = SelectRecord.NumberPLace.ToString();

                    foreach (var item in efClient.GetPlaces)
                    {
                        if(item.Number == SelectRecord.NumberPLace)
                        {
                            this.Client = String.Format("{0} {1}",item.Client.FirstName, item.Client.LastName);
                            this.Car = String.Format("{0} {1}", item.Car.Brand, item.Car.Color);
                            this.Tariff = String.Format("{0}грн.    Дата Оплаты: {1}    Задолженность: {2}грн.", item.Tariff.RentValue.Price, item.Tariff.DatePayment.ToShortDateString(), item.Tariff.Debt);
                            break;
                        }
                    }

                  
                    GridVisibility = Visibility.Visible;                
                }));
            }
        }

        private Record selectRecord;
        public Record SelectRecord
        {
            get
            {
                return selectRecord;
            }
            set
            {
                selectRecord = value;
                OnPropertyChanged("SelectRecord");
            }
        }

        private Visibility gridVisibility;
        public Visibility GridVisibility
        {
            get
            {
                return gridVisibility;
            }
            set
            {
                gridVisibility = value;
                OnPropertyChanged("GridVisibility");
            }
        }


        #endregion;

        #region GridInfo
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
                OnPropertyChanged("NumPlace");
            }
        }

        private string client;
        public string Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }

        private string car;
        public string Car
        {
            get
            {
                return car;
            }
            set
            {
                car = value;
                OnPropertyChanged("Car");
            }
        }

        private string tariff;
        public string Tariff
        {
            get
            {
                return tariff;
            }
            set
            {
                tariff = value;
                OnPropertyChanged("Tariff");
            }
        }


        #endregion;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion;
    }
}
