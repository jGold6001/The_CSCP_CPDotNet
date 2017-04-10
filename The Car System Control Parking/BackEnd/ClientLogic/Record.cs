using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{

    public class Record : INotifyPropertyChanged
    {
        private int numPlace;
        public int NumberPLace
        {
            get { return numPlace; }
            set
            {
                numPlace = value;
                OnPropertyChanged("NumberPlace");
            }
        }

        private string clientLastName;
        public string ClientLastName
        {
            get { return clientLastName; }
            set
            {
                clientLastName = value;
                OnPropertyChanged("ClientLastName");
            }
        }

        private string carBrand;
        public string CarBrand
        {
            get { return carBrand; }
            set
            {
                carBrand = value;
                OnPropertyChanged("CarBrand");
            }
        }

        private DateTime datePay;
        public DateTime DatePayment
        {
            get { return datePay; }
            set
            {
                datePay = value;
                OnPropertyChanged("DatePayment");
            }
        }

        private decimal rent;
        public decimal Rent
        {
            get { return rent; }
            set
            {
                rent = value;
                OnPropertyChanged("Rent");
            }
        }

        private decimal debt;
        public decimal Debt
        {
            get { return debt; }
            set
            {
                debt = value;
                OnPropertyChanged("Debt");
            }
        }

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
