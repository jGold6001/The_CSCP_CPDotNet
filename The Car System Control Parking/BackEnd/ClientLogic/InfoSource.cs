using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class InfoSource : INotifyPropertyChanged
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


        
        #region GridInfo_&_FullInfoWnd 

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
        #endregion

        #region DepositWnd
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

        private string minSum;
        public string MinSum
        {
            get { return minSum; }
            set
            {
                minSum = value;
                OnPropertyChanged("MinSum");
            }
        }

        private string paySum;
        public string PaySum
        {
            get { return paySum; }
            set
            {
                paySum = value;
                OnPropertyChanged("PaySum");
            }
        }
        #endregion

        #region PlaceWnd

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
                OnPropertyChanged("FreePlaces");
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
                OnPropertyChanged("BusyPlaces");
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
