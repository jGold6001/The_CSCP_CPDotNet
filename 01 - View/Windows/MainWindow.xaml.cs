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
        private ObservableCollection<Record> findRecords;
        private ObservableCollection<Record> filterRecords;

        public Record SelectRecord { get; set; }
        private DataBuff buffer;
        private InfoSource infoSource;
        private int cntFindResult =0;
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
            AdminWindow aw = new AdminWindow();
            aw.ShowDialog();
            listRecords = new ObservableCollection<Record>(efClient.RecordList);
            this.ItemSourceUpdate();
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow aw = new AuthorizationWindow();
            aw.Show();
            this.Close();
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
            string messege = String.Format("Вы действительно хотите удалить запись Места №{0}", SelectRecord.NumberPLace);
            if (MessageBox.Show(messege, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                efClient.Remove(SelectRecord);
                listRecords.Remove(SelectRecord);
                this.ItemSourceUpdate();
                gridInfo.Visibility = Visibility.Hidden;
            }    
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
            dgRecords.SelectedItem = SelectRecord;
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

        #region Filter_Events
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.FindText();
        }

        private void TbFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.FindText();
        }

        private void ChbClientsDebt_Checked(object sender, RoutedEventArgs e)
        {
            gridInfo.Visibility = Visibility.Hidden;
            filterRecords = null;
            if(chbRentDaily.IsChecked == true)
                this.GetDebtAndDaily();
            else if(chbRentMonthly.IsChecked == true)
                this.GetDebtAndMonth();
            else
                this.GetDebt();
        }

        private void ChbClientsDebt_Unchecked(object sender, RoutedEventArgs e)
        {
            filterRecords = null;
            if (chbRentDaily.IsChecked == true)
                this.GetDaily();
            else if (chbRentMonthly.IsChecked == true)
                this.GetMonth();
            else
            {
                if (dgRecords.ItemsSource != listRecords)
                    this.ItemSourceUpdate();
            }
          
        }
        
        private void ChbRentDaily_Checked(object sender, RoutedEventArgs e)
        {
            gridInfo.Visibility = Visibility.Hidden;
            if (chbRentMonthly.IsChecked == true)
                chbRentMonthly.IsChecked = false;


            filterRecords = null;
            if (chbClientsDebt.IsChecked == true)
                this.GetDebtAndDaily();
            else
                this.GetDaily();

            
        }

        private void ChbRentDaily_Unchecked(object sender, RoutedEventArgs e)
        {
            filterRecords = null;
            if (chbClientsDebt.IsChecked == true)
                this.GetDebt();
            else
            {
                if (dgRecords.ItemsSource != listRecords)
                    this.ItemSourceUpdate();
            }
        }

        private void ChbRentMonthly_Checked(object sender, RoutedEventArgs e)
        {
            gridInfo.Visibility = Visibility.Hidden;
            if (chbRentDaily.IsChecked == true)
                chbRentDaily.IsChecked = false;

            filterRecords = null;
            if (chbClientsDebt.IsChecked == true)
                this.GetDebtAndMonth();
            else
                this.GetMonth();
        }

        private void ChbRentMonthly_Unchecked(object sender, RoutedEventArgs e)
        {
            filterRecords = null;
            if (chbClientsDebt.IsChecked == true)
                this.GetDebt();
            else
            {
                if (dgRecords.ItemsSource != listRecords)
                    this.ItemSourceUpdate();
            }
        }

        #endregion

        #region Methods
        private void SetClickEvents()
        {
            btnDelete.Click += BtnDelete_Click;
            dgDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
            dgEdit.Click += BtnEdit_Click;
            btnFullInfo.Click += BtnFullInfo_Click;
            btnPay.Click += BtnPay_Click;
            AddClient.Click += AddClient_Click;
            FreePlace.Click += FreePlace_Click;
            Manager.Click += Manager_Click;
            LogOut.Click += LogOut_Click;
            btnOk.Click += BtnOk_Click;
            chbClientsDebt.Checked += ChbClientsDebt_Checked;
            chbClientsDebt.Unchecked += ChbClientsDebt_Unchecked;
            chbRentDaily.Checked += ChbRentDaily_Checked;
            chbRentDaily.Unchecked += ChbRentDaily_Unchecked;
            chbRentMonthly.Checked += ChbRentMonthly_Checked;
            chbRentMonthly.Unchecked += ChbRentMonthly_Unchecked;
            tbFind.KeyUp += TbFind_KeyUp;
        }

        private void FindText()
        {
            gridInfo.Visibility = Visibility.Hidden;
            if (chbClientsDebt.IsChecked == true || chbRentDaily.IsChecked == true || chbRentMonthly.IsChecked == true)
            {
                chbClientsDebt.IsChecked = false;
                chbRentDaily.IsChecked = false;
                chbRentMonthly.IsChecked = false;
            }
            string findWord = tbFind.Text;
            this.IfFindListNotNull();

            if (findWord != "")
            {
                if (findWord.Count() > 2)
                {
                    foreach (var item in efClient.GetPlaces)
                    {
                        string firstname = item.Client.FirstName;
                        if (firstname.ToLower().Contains(findWord) || firstname.ToUpper().Contains(findWord) || firstname.Contains(findWord))
                            this.FindRecords(item);

                        string lastname = item.Client.LastName;
                        if (lastname.ToLower().Contains(findWord) || lastname.ToUpper().Contains(findWord) || lastname.Contains(findWord))
                            this.FindRecords(item);

                        string firstLast = String.Format("{0} {1}", firstname, lastname);
                        if (firstLast.ToLower().Contains(findWord) || firstLast.ToUpper().Contains(findWord) || firstLast.Contains(findWord))
                            this.FindRecords(item);

                        string lastFirst = String.Format("{0} {1}", lastname, firstname);
                        if (lastFirst.ToLower().Contains(findWord) || lastFirst.ToUpper().Contains(findWord) || lastFirst.Contains(findWord))
                            this.FindRecords(item);

                        string telNum = item.Client.PhoneNumber.ToString();
                        if (telNum.Contains(findWord))
                        {
                            this.FindRecords(item);
                            break;
                        }

                        string passport = item.Client.PassportID;
                        if (passport.ToLower().Contains(findWord) || passport.ToUpper().Contains(findWord) || passport.Contains(findWord))
                        {
                            this.FindRecords(item);
                            break;
                        }

                        if (item.Client.AdditionalInfo != null)
                        {
                            string info = item.Client.AdditionalInfo;
                            if (info.ToLower().Contains(findWord) || info.ToUpper().Contains(findWord) || info.Contains(findWord))
                                this.FindRecords(item);
                        }

                        string carId = item.Car.VehicleID;
                        if (carId.ToLower().Contains(findWord) || carId.ToUpper().Contains(findWord) || carId.Contains(findWord))
                        {
                            this.FindRecords(item);
                            break;
                        }

                        string brand = item.Car.Brand;
                        if (brand.Contains(findWord) || brand.ToUpper().Contains(findWord) || brand.ToLower().Contains(findWord))
                            this.FindRecords(item);

                        string color = item.Car.Color;
                        if (color.ToLower().Contains(findWord) || color.ToUpper().Contains(findWord) || color.Contains(findWord))
                            this.FindRecords(item);

                        string colorBrand = String.Format("{0} {1}", color, brand);
                        if (colorBrand.ToLower().Contains(findWord) || colorBrand.ToUpper().Contains(findWord) || colorBrand.Contains(findWord))
                            this.FindRecords(item);

                        string brandColor = String.Format("{0} {1}", brand, color);
                        if (brandColor.ToLower().Contains(findWord) || brandColor.ToUpper().Contains(findWord) || brandColor.Contains(findWord))
                            this.FindRecords(item);

                        string vin = item.Car.VIN;
                        if (vin.ToLower().Contains(findWord) || vin.ToUpper().Contains(findWord) || vin.Contains(findWord))
                        {
                            this.FindRecords(item);
                            break;
                        }
                    }
                }


                if (cntFindResult == 0)
                {
                    MessageBox.Show("Поиск не дал результат");
                    this.ItemSourceUpdate();
                }
                else
                    cntFindResult = 0;


                tbFind.Text = "";
            }
            else
            {
                if (dgRecords.ItemsSource != listRecords)
                    this.ItemSourceUpdate();
            }
        }

        private void DGLoad()
        {
            listRecords = new ObservableCollection<Record>(efClient.RecordList);
            this.ItemSourceUpdate();
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
            this.ItemSourceUpdate();
            SelectRecord = newRecord;
        }

        private void FilterRecords(Place place)
        {
            Record record = new Record()
            {
                NumberPLace = place.Number,
                ClientLastName = place.Client.LastName,
                CarBrand = place.Car.Brand,
                DatePayment = place.Tariff.DatePayment,
                Rent = place.Tariff.RentValue.Price,
                Debt = place.Tariff.Debt
            };
            if(filterRecords == null)
                filterRecords = new ObservableCollection<Record>();                

            dgRecords.ItemsSource = filterRecords;
            filterRecords.Add(record);
        }

        private void FindRecords(Place place)
        {
            cntFindResult++;
            Record record = new Record()
            {
                NumberPLace = place.Number,
                ClientLastName = place.Client.LastName,
                CarBrand = place.Car.Brand,
                DatePayment = place.Tariff.DatePayment,
                Rent = place.Tariff.RentValue.Price,
                Debt = place.Tariff.Debt
            };
            if (findRecords == null)
                findRecords = new ObservableCollection<Record>();
                       
            dgRecords.ItemsSource = findRecords;            
            foreach (var item in findRecords)
            {
                if (item.NumberPLace == record.NumberPLace)
                {
                    findRecords.Remove(item);
                    break;
                }
            }
            findRecords.Add(record);
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
            this.ItemSourceUpdate();
            dgRecords.SelectedItem = newRecord; 
                     
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
                Manager.Visibility = Visibility.Hidden;

            }
        }

        private void IfFindListNotNull()
        {
            if (findRecords != null)
                findRecords.Clear();
        }

        private void ItemSourceUpdate()
        {
            dgRecords.ItemsSource = listRecords.OrderBy(p => p.NumberPLace);
        }

        private void GetDebt()
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Tariff.Debt > 0)
                    this.FilterRecords(item);
            }
        }

        private void GetMonth()
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Tariff.RentValue.Id == 2)
                    this.FilterRecords(item);
            }
        }

        private void GetDaily()
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Tariff.RentValue.Id == 1)
                    this.FilterRecords(item);
            }
        }

        private void GetDebtAndMonth()
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Tariff.Debt > 0 && item.Tariff.RentValue.Id == 2)
                    this.FilterRecords(item);
            }
        }

        private void GetDebtAndDaily()
        {
            foreach (var item in efClient.GetPlaces)
            {
                if (item.Tariff.Debt > 0 && item.Tariff.RentValue.Id == 1)
                    FilterRecords(item);
            }
        }

        #endregion

    }
}
