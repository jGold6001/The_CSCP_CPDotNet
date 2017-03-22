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

    public class DisplayRecords 
    {
        EFContext db;

        public DisplayRecords(EFContext db)
        {
            this.db = db;
        }

        public List<Record> GetList
        {
            get
            {
                return db.Places.Select(p => new Record()
                {
                    NumberPLace = p.Number,
                    ClientLastName = p.Client.LastName,
                    CarBrand = p.Car.Brand,
                    DateRegistred = p.Client.DateRegistred,
                    DatePayment = p.Tariff.DatePayment,
                    Deposit = p.Tariff.Deposit,
                    Debt = p.Tariff.Debt
                }).ToList();
            }
        }
    }

    public class Record : EFPropertyChanged
    {       
        private int numPlace;
        private string clientLastName;
        private string carBrand;
        private DateTime dateReg;
        private DateTime datePay;
        private decimal deposit;
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

        public DateTime DateRegistred
        {
            get
            {
                return dateReg;
            }
            set
            {
                dateReg = value;
                OnPropertyChanged("DateRegistred");
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

        public decimal Deposit
        {
            get
            {
                return deposit;
            }
            set
            {
                deposit = value;
                OnPropertyChanged("Deposit");
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
    }


}
