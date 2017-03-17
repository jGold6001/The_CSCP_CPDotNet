using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    public class Calculator
    {
        private int rentVal;
        private decimal deposit;
        private decimal debt;
        private DateTime datePayment;
        private decimal rentPrice;

        public Calculator(Tariff tariff)
        {
            rentVal = tariff.RentValue.Id;
            deposit = tariff.Deposit;
            debt = tariff.Debt;
            datePayment = tariff.DatePayment;
            rentPrice = tariff.RentValue.Price;
        }
        public void DatePayment()
        {
            if (rentVal == 1)
                datePayment = DateTime.Now.AddDays(this.Remainder);
            else
                datePayment = DateTime.Now.AddMonths(this.Remainder);
        }

        public DateTime DatePayment(DateTime date)
        {
            if (rentVal == 1)
                return date.AddDays(this.Remainder);
            else
                return date.AddMonths(this.Remainder);
        }

        public void Debt()
        {
            if (DateTime.Now > datePayment)
            {
                debt = this.Period(rentVal, DateTime.Now);
                deposit = 0;
            }
        }

        public void Debt(DateTime date)
        {
            if (DateTime.Now > datePayment)
            {
                this.Period(rentVal, date);
                deposit = 0;
            }
        }

        private decimal Period(int rentId, DateTime date)
        {
           if(rentId == 1)
            {
                int days = (date - datePayment).Days;
                return rentPrice * (days + 1);
            }
           else
            {
                int year = 1;
                if (date.Year > datePayment.Year)
                    year = (date.Year - datePayment.Year) * 12;

                int month = (date.Month - datePayment.Month) * year;
                return rentPrice * month;
            }
        }

        private int Remainder
        {
            get { return Convert.ToInt32(deposit / rentPrice); }
        }
    }
}
