using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Place
    {
        [Key]
        public int Id { get; set; }        
        public int Number { get; set; }      
        public int? ClientId { get; set; }       
        public int? CarId { get; set; }       
        public int? TariffId { get; set; }
        public Client Client { get; set; }
        public Car Car { get; set; }
        public Tariff Tariff{get;set;}

        public override string ToString()
        {
            if(Client != null && Car !=null && Tariff != null)
            {
                return String.Format("Place #{0}\nClient: {1} {5}\ncar: {2} {6}\ndate register: {3}\ntarriff: {4}\nDeposit: {7}\nDebt: {8}\nDayPay: {9}\n\n", Number, Client.LastName, Car.Brand, Client.DateRegistred.ToShortDateString(), Tariff.RentValue.Price, Client.FirstName, Car.Color, Tariff.Deposit, Tariff.Debt, Tariff.DatePayment.ToShortDateString());
            }
            return String.Format("Place #{0} - Empty", Number);
        }
    }
}
