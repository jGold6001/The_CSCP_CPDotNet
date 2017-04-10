using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class DataBuff: INotifyPropertyChanged
    {

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

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;                
                OnPropertyChanged("FirstName");
            }
        }

        private string passportID;
        public string PassportID
        {
            get { return passportID; }
            set
            {
                passportID = value;
                OnPropertyChanged("PassportID");
            }
        }

        private string dateReg;
        public string DateRegistration
        {
            get { return dateReg; }
            set
            {
                dateReg = value;
                OnPropertyChanged("DateRegistration");
            }
        }

        private string phone;
        public string PhoneNumber
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string addInfo;
        public string AdditionalInfo
        {
            get { return addInfo; }
            set
            {
                addInfo = value;
                OnPropertyChanged("AdditionalInfo");
            }
        }

        private string vehileID;
        public string VehicleID
        {
            get { return vehileID; }
            set
            {
                vehileID = value;
                OnPropertyChanged("VehicleID");
            }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("Brand");
            }
        }

        private string vin;
        public string VIN
        {
            get { return vin; }
            set
            {
                vin = value;
                OnPropertyChanged("VIN");
            }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        private string rentType;
        public string RentType
        {
            get { return rentType; }
            set
            {
                rentType = value;
                OnPropertyChanged("RentType");
            }
        }

        private string rentPrice;
        public string RentPrice
        {
            get { return rentPrice; }
            set
            {
                rentPrice = value;
                OnPropertyChanged("RentType");
            }
        }

        private string deposit;
        public string Deposit
        {
            get { return deposit; }
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
            }
        }

        private string debt;
        public string Debt
        {
            get { return debt; }
            set
            {
                debt = value;
                OnPropertyChanged("Debt");
            }
        }

        private string datePay;
        public string DatePayment
        {
            get { return datePay; }
            set
            {
                datePay = value;
                OnPropertyChanged("DatePayment");
            }
        }


        #region DataWindow_ComboBoxes_collections
        private ObservableCollection<string> brands;
        public ObservableCollection<string> Brands
        {
            get
            {
                return brands;
            }
            set
            {
                brands = value;
                OnPropertyChanged("Brands");
            }
        }


        private ObservableCollection<string> colors;
        public ObservableCollection<string> Colors
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
                OnPropertyChanged("Colors");
            }
        }


        private ObservableCollection<string> rentTypes;
        public ObservableCollection<string> RentTypes
        {
            get
            {
                return rentTypes;
            }
            set
            {
                rentTypes = value;
                OnPropertyChanged("RentTypes");
            }
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler P = PropertyChanged;
            P?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
