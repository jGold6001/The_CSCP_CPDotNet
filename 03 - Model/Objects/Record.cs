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

    public class Record 
    {                 
        public int NumberPLace { get; set; }
        public string ClientLastName { get; set; }
        public string CarBrand { get; set; }
        public DateTime DateRegistred { get; set; }
        public DateTime DatePayment { get; set; }
        public decimal Deposit { get; set; }
        public decimal Debt { get; set; }

    }


}
