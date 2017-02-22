using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{

    public class DisplayRecords 
    {
        DataBase db;

        public DisplayRecords(DataBase db)
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
                    DatePayment = p.Tarriff.DatePayment,
                    Deposit = p.Tarriff.Deposit,
                    Debt = p.Tarriff.Debt
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
