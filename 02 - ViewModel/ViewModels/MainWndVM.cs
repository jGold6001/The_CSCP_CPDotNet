using _03___Model;
using DataLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
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
    public class MainWndVM : ViewModelBase
    {
       
        private EFClientOp efClient = new EFClientOp();
        private ObservableCollection<Record> listRecords;

        private ObservableCollection<IDialogViewModel> dialogs = new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return dialogs; } }

        private ClientBuff buffClient;

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
               RaisePropertyChanged(() => RecordSource);
            }
        }

        private RelayCommand<SelectionChangedEventArgs> selectCommand;
        public RelayCommand<SelectionChangedEventArgs> SelectCommand
        {
            get
            {
                return selectCommand ?? (selectCommand = new RelayCommand<SelectionChangedEventArgs>(obj =>
                {
                    if(SelectRecord !=null)
                    {
                        this.NumPlace = SelectRecord.NumberPLace.ToString();

                        foreach (var item in efClient.GetPlaces)
                        {
                            if (item.Number == SelectRecord.NumberPLace)
                            {
                                this.Client = String.Format("{0} {1}", item.Client.FirstName, item.Client.LastName);
                                this.Car = String.Format("{0} {1}", item.Car.Brand, item.Car.Color);
                                this.Tariff = String.Format("{0}грн.    Дата Оплаты: {1}    Задолженность: {2}грн.", item.Tariff.RentValue.Price, item.Tariff.DatePayment.ToShortDateString(), item.Tariff.Debt);

                                buffClient = new ClientBuff()
                                {
                                    FirstName = item.Client.FirstName,
                                    LastName = item.Client.LastName,
                                    PassportID = item.Client.PassportID,
                                    PhoneNumber = item.Client.PhoneNumber.ToString(),
                                    AdditionalInfo = item.Client.AdditionalInfo,
                                    VehicleID = item.Car.VehicleID,
                                    Brand = item.Car.Brand,
                                    VIN = item.Car.VIN,
                                    Color = item.Car.Color,
                                    Rent = item.Tariff.RentValue.Name,
                                    Deposit = item.Tariff.Deposit.ToString()
                                };
                               
                                break;
                            }
                        }
                        GridVisibility = Visibility.Visible;
                    }    
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
                RaisePropertyChanged(()=> SelectRecord);
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
                RaisePropertyChanged(() => GridVisibility);
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
                RaisePropertyChanged(() => NumPlace);
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
                RaisePropertyChanged(() => Client);
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
                RaisePropertyChanged(() => Car);
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
                RaisePropertyChanged(() => Tariff);
            }
        }

        private RelayCommand<SelectionChangedEventArgs> deleteCommand;
        public RelayCommand<SelectionChangedEventArgs> DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand<SelectionChangedEventArgs>(obj =>
                {               
                    efClient.Remove(SelectRecord);
                    RecordSource.Remove(SelectRecord);
                    GridVisibility = Visibility.Hidden;
                }));
            }
        }


        private RelayCommand<SelectionChangedEventArgs> editCommand;
        public RelayCommand<SelectionChangedEventArgs> EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand<SelectionChangedEventArgs>(obj =>
                {
                    DataWndVM dataWnd = new DataWndVM(DialogName.EditData)
                    {
                        //read datas
                        FirstNameVM = buffClient.FirstName,
                        LastNameVM = buffClient.LastName,
                        PassportIDVM = buffClient.PassportID,
                        TelNumVM = buffClient.PhoneNumber,
                        AdditInfoVM = buffClient.AdditionalInfo,
                        CarIDVM = buffClient.VehicleID,
                        BrandItem = buffClient.Brand,
                        VINVM = buffClient.VIN,
                        ColorItem = buffClient.Color,
                        RentItem = buffClient.Rent
                    };

                    //save data
                    dataWnd.OnOk = ((sender) =>
                    {
                        sender.Close();
                        
                        Client client = new Client();
                        client.FirstName = dataWnd.FirstNameVM;
                        client.LastName = dataWnd.LastNameVM;
                        client.PassportID = dataWnd.PassportIDVM;
                        client.PhoneNumber = Convert.ToInt32(dataWnd.TelNumVM);
                        client.AdditionalInfo = dataWnd.AdditInfoVM;

                        Car car = new Car();
                        car.VehicleID = dataWnd.CarIDVM;
                        car.Brand = dataWnd.BrandItem;
                        car.VIN = dataWnd.VINVM;
                        car.Color = dataWnd.ColorItem;

                        Tariff tariff = new Tariff();
                        tariff.RentValue = this.GetRent(dataWnd.RentItem);

                        Place newPlace = new Place();
                        newPlace.Number = SelectRecord.NumberPLace;
                        newPlace.Client = client;
                        newPlace.Car = car;
                        newPlace.Tariff = tariff;

                        efClient.Edit(newPlace);
                        this.UpdateRecord(newPlace);
                        GridVisibility = Visibility.Hidden;

                    });

                    this.Dialogs.Add(dataWnd);
                }));
            }
        }

        private RentValue GetRent(string rentItem)
        {        
            foreach (var item in efClient.RentTypes)
            {
                if (item.Name == rentItem)
                    return item;                  
            }
            return null;         
        }

        private void UpdateRecord(Place newPlace)
        {
            Record newRecord = new Record()
            {
                NumberPLace = newPlace.Number,
                ClientLastName = newPlace.Client.LastName,
                CarBrand = newPlace.Car.Brand,
                DatePayment = newPlace.Tariff.DatePayment,
                Debt = newPlace.Tariff.Debt,
                Rent = newPlace.Tariff.RentValue.Price
            };

            foreach (var item in RecordSource)
            {
                if(item.NumberPLace == newPlace.Number)
                {
                    RecordSource.Remove(item);
                    RecordSource.Add(newRecord);
                    break;
                }
            }           
        }

        #endregion;

    }
}
