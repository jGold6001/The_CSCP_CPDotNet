using _03___Model;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;

namespace _01___View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFClientOp efClient = new EFClientOp();
        public ObservableCollection<Record> listRecords{ get; set; }
        public Record SelectRecord { get; set; }
        private DataBuff buffer;
        private InfoSource infoSource;
        public MainWndAccess access;

        public MainWindow(MainWndAccess access)
        {          
            InitializeComponent();
            this.access = access;
            this.SetAccess();
            this.DGLoad();
            this.SetClickEvents();
        }

        private bool Flag
        {
            get
            {
                return access == MainWndAccess.Admin ? true : false;
            }
        }

        #region MenuPanel_Events
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dw = new DataWindow(DialogName.AddData);

            if (dw.ShowDialog() == true)
            {
                Client client = new Client();
                client.FirstName = dw.Buffer.FirstName;
                client.LastName = dw.Buffer.LastName;              
                client.PassportID = dw.Buffer.PassportID;
                client.PhoneNumber = Convert.ToInt64(this.IfEmty(dw.Buffer.PhoneNumber));
                client.AdditionalInfo = dw.Buffer.AdditionalInfo;
                client.DateRegistred = DateTime.Now;

                Car car = new Car();
                car.VehicleID = dw.Buffer.VehicleID;
                car.Brand = dw.Buffer.Brand;
                car.VIN = dw.Buffer.VIN;
                car.Color = dw.Buffer.Color;

                Tariff tariff = new Tariff();
                tariff.RentValue = this.GetRent(dw.Buffer.RentType);
                tariff.Deposit = Convert.ToDecimal(dw.Buffer.Deposit);
                tariff.Debt = 0;
                tariff.DatePayment = Calculator.DatePayment(tariff);


                Place newPlace = new Place();
                newPlace.Number = Convert.ToInt32(dw.Buffer.NumPlace);
                newPlace.Client = client;
                newPlace.Car = car;
                newPlace.Tariff = tariff;

                efClient.Add(newPlace);
                this.CreateRecord(newPlace);
            }

        }

        private void FreePlace_Click(object sender, RoutedEventArgs e)
        {
            PlaceWindow pw = new PlaceWindow(PlaceWndName.FreePlace);
            pw.Show();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region DataGrid_Events
        private void SelectionRecord(object sender, SelectionChangedEventArgs e)
        {
            SelectRecord = dgRecords.SelectedItem as Record;
            if (SelectRecord != null)
            {
                this.LoadBuffer();
                this.UpdateInfo();
                gridInfo.Visibility = Visibility.Visible;
            }

        }
        #endregion

        #region InfoGrid_Events

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            efClient.Remove(SelectRecord);
            listRecords.Remove(SelectRecord);
            gridInfo.Visibility = Visibility.Hidden;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataWindow dw = new DataWindow(DialogName.EditData);
            dw.Buffer.NumPlace = buffer.NumPlace;
            dw.Buffer.FirstName = buffer.FirstName;
            dw.Buffer.LastName = buffer.LastName;
            dw.Buffer.PassportID = buffer.PassportID;
            dw.Buffer.PhoneNumber = buffer.PhoneNumber;
            dw.Buffer.AdditionalInfo = buffer.AdditionalInfo;
            dw.Buffer.Brand = buffer.Brand;
            dw.Buffer.Color = buffer.Color;
            dw.Buffer.VehicleID = buffer.VehicleID;
            dw.Buffer.VIN = buffer.VIN;
            dw.Buffer.RentType = buffer.RentType;
            dw.Buffer.Deposit = buffer.Deposit;

            if(dw.ShowDialog() == true)
            {
                Client client = new Client();
                client.FirstName = dw.Buffer.FirstName;
                client.LastName = dw.Buffer.LastName;
                client.PassportID = dw.Buffer.PassportID;              
                client.PhoneNumber = Convert.ToInt64(this.IfEmty(dw.Buffer.PhoneNumber));
                client.AdditionalInfo = dw.Buffer.AdditionalInfo;

                Car car = new Car();
                car.VehicleID = dw.Buffer.VehicleID;
                car.Brand = dw.Buffer.Brand;
                car.VIN = dw.Buffer.VIN;
                car.Color = dw.Buffer.Color;

                Tariff tariff = new Tariff();
                tariff.RentValue = this.GetRent(dw.Buffer.RentType);

                Place newPlace = new Place();
                newPlace.Number = Convert.ToInt32(dw.Buffer.NumPlace);
                newPlace.Client = client;
                newPlace.Car = car;
                newPlace.Tariff = tariff;

                efClient.Edit(newPlace);
                this.UpdateBuffer(newPlace);
                this.UpdateRecord();
                this.UpdateInfo();
            }
           
        }

        private void BtnFullInfo_Click(object sender, RoutedEventArgs e)
        {
            FullInfoWindow fullInfo = new FullInfoWindow();
            fullInfo.InfoSource.NumPlace = buffer.NumPlace;
            fullInfo.InfoSource.Client = String.Format("{0} {1}\nПаспорт: {2}\nТел: {3}\nДата регистрации: {4}\nДоп.Инфо: {5}", buffer.LastName, buffer.FirstName, buffer.PassportID, this.IfNull(buffer.PhoneNumber), buffer.DateRegistration, this.IfNull(buffer.AdditionalInfo));
            fullInfo.InfoSource.Car = String.Format("{0} {1}\nНомер: {2}\nНомер кузова: {3}", buffer.Brand, buffer.Color, buffer.VehicleID, buffer.VIN);
            fullInfo.InfoSource.Tariff = String.Format("{0} {1}грн.\nДепозит: {2}грн.\nЗадолженность: {3}грн.\nДата оплаты: {4}", buffer.RentType, buffer.RentPrice, buffer.Deposit, buffer.Debt, buffer.DatePayment);
            fullInfo.Show();
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            Tariff tariff = new Tariff()
            {
                RentValue = this.GetRent(buffer.RentType),
                Deposit = Convert.ToDecimal(buffer.Deposit),
                Debt = Convert.ToDecimal(buffer.Debt),
                DatePayment = Convert.ToDateTime(buffer.DatePayment)
            };
            Pay pay = new Pay(tariff);

            DepositWindow dw = new DepositWindow();
            dw.InfoSource.Client = String.Format("{0} {1}", buffer.LastName, buffer.FirstName);
            dw.InfoSource.Deposit = buffer.Deposit;
            dw.InfoSource.Debt = buffer.Debt;
            dw.InfoSource.MinSum = pay.MinSum.ToString();

            if (dw.ShowDialog() == true)
            {
                decimal sum = Convert.ToDecimal(dw.InfoSource.PaySum);
                pay.Payment(sum);
                int num_index = Convert.ToInt32(buffer.NumPlace);
                foreach (var item in efClient.GetPlaces)
                {
                    if (item.Number == num_index)
                    {
                        item.Tariff = pay.tariff;
                        this.UpdateBuffer(item);
                        this.UpdateRecord();
                        this.UpdateInfo();
                        break;
                    }
                }
                dw.InfoSource.PaySum = null;
            }
        }
        #endregion

        #region Methods

        private void SetClickEvents()
        {
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            btnFullInfo.Click += BtnFullInfo_Click;
            btnPay.Click += BtnPay_Click;
            AddClient.Click += AddClient_Click;
            FreePlace.Click += FreePlace_Click;
            Manager.Click += Manager_Click;
            LogOut.Click += LogOut_Click;
        }

        private void DGLoad()
        {
            listRecords = new ObservableCollection<Record>(efClient.RecordList);
            dgRecords.ItemsSource = listRecords;
            dgRecords.SelectionChanged += SelectionRecord;
            gridInfo.Visibility = Visibility.Hidden;            
        }       

        private void LoadBuffer()
        {         
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Number == SelectRecord.NumberPLace)
                {
                    buffer = new DataBuff()
                    {
                        NumPlace = item.Number.ToString(),
                        FirstName = item.Client.FirstName,
                        LastName = item.Client.LastName,
                        PassportID = item.Client.PassportID,
                        PhoneNumber = item.Client.PhoneNumber.ToString(),
                        DateRegistration = item.Client.DateRegistred.ToShortDateString(),
                        AdditionalInfo = item.Client.AdditionalInfo,
                        VehicleID = item.Car.VehicleID,
                        Brand = item.Car.Brand,
                        VIN = item.Car.VIN,
                        Color = item.Car.Color,
                        RentType = item.Tariff.RentValue.Name,
                        RentPrice = item.Tariff.RentValue.Price.ToString(),
                        Deposit = item.Tariff.Deposit.ToString(),
                        Debt = item.Tariff.Debt.ToString(),
                        DatePayment = item.Tariff.DatePayment.ToShortDateString()
                    };
                    break;
                }
            }
        }

        private void UpdateBuffer(Place newPlace)
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Number == newPlace.Number)
                {
                    buffer = new DataBuff()
                    {
                        NumPlace = item.Number.ToString(),
                        FirstName = item.Client.FirstName,
                        LastName = item.Client.LastName,
                        PassportID = item.Client.PassportID,
                        PhoneNumber = item.Client.PhoneNumber.ToString(),
                        DateRegistration = item.Client.DateRegistred.ToShortDateString(),
                        AdditionalInfo = item.Client.AdditionalInfo,
                        VehicleID = item.Car.VehicleID,
                        Brand = item.Car.Brand,
                        VIN = item.Car.VIN,
                        Color = item.Car.Color,
                        RentType = item.Tariff.RentValue.Name,
                        RentPrice = item.Tariff.RentValue.Price.ToString(),
                        Deposit = item.Tariff.Deposit.ToString(),
                        Debt = item.Tariff.Debt.ToString(),
                        DatePayment = item.Tariff.DatePayment.ToShortDateString()
                    };
                    break;
                }
            }
        }

        private void UpdateInfo()
        {
            infoSource = (InfoSource)App.Current.Resources["infoSource"];
            infoSource.NumPlace = buffer.NumPlace;
            infoSource.Client = String.Format("{0} {1}", buffer.LastName, buffer.FirstName);
            infoSource.Car = String.Format("{0} {1}", buffer.Brand, buffer.Color);
            infoSource.Tariff = String.Format("{0}грн.    Дата Оплаты: {1}    Задолженность: {2}грн.", buffer.RentPrice, buffer.DatePayment, buffer.Debt);
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

        private void UpdateRecord()
        {
            Record newRecord = new Record()
            {
                NumberPLace = Convert.ToInt32(buffer.NumPlace),
                ClientLastName = buffer.LastName,
                CarBrand = buffer.Brand,
                DatePayment = Convert.ToDateTime(buffer.DatePayment),
                Debt = Convert.ToDecimal(buffer.Debt),
                Rent = Convert.ToDecimal(buffer.RentPrice)
            };

            foreach (var item in listRecords)
            {
                if (item.NumberPLace == newRecord.NumberPLace)
                {
                    listRecords.Remove(item);
                    listRecords.Add(newRecord);
                    break;
                }
            }
        }

        private void CreateRecord(Place newPlace)
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
            listRecords.Add(newRecord);            
        }

        private string IfNull(string value)
        {
            
            if (value == "0" || value == "" || value ==null)
                return "(отсутствует)";
            else
                return value;
        }

        private string IfEmty(string value)
        {
            return (value == "" ? "0" : value); 
        }

        private void SetAccess()
        {
            if(!Flag)
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
                Manager.IsEnabled = false;

            }
        }
        #endregion

    }
}
