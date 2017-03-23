using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{

    public class Record : INotifyPropertyChanged
    {       
        private int numPlace;
        private string clientLastName;
        private string carBrand;
        private DateTime datePay;
        private decimal rent;
        private decimal debt;

        public int NumberPLace
        {
            get
            {
                return numPlace;
            }
            set
            {
                numPlace = value;
                OnPropertyChanged("NumberPLace");
            }
        }

        public string ClientLastName
        {
            get
            {
                return clientLastName;
            }
            set
            {
                clientLastName = value;
                OnPropertyChanged("ClientLastName");
            }
        }

        public string CarBrand
        {
            get
            {
                return carBrand;
            }
            set
            {
                carBrand = value;
                OnPropertyChanged("CarBrand");
            }
        }

        public DateTime DatePayment
        {
            get
            {
                return datePay;
            }
            set
            {
                datePay = value;
                OnPropertyChanged("DatePayment");
            }
        }

        public decimal Rent
        {
            get
            {
                return rent;
            }
            set
            {
                rent = value;
                OnPropertyChanged("Rent");
            }
        }

        public decimal Debt
        {
            get
            {
                return debt;
            }
            set
            {
                debt = value;
                OnPropertyChanged("Debt");
            }
        }
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
