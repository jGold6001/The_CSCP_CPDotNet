using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_CSCP
{
    public class Calculator
    {
        private int rentVal;
        private decimal deposit;
        private decimal debt;
        private DateTime datePayment;
        private decimal rentPrice;

        private decimal sum;

        public Calculator(Tariff tariff, decimal sum)
        {
            rentVal = tariff.RentValue.Id;
            deposit = tariff.Deposit;
            debt = tariff.Debt;
            datePayment = tariff.DatePayment;
            rentPrice = tariff.RentValue.Price;
            this.sum = sum;
        }
             
        public DateTime DatePayment()
        {
            if (rentVal == 1)
                return datePayment.AddDays(this.Remainder);
            else
                return datePayment.AddMonths(this.Remainder);
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
            get { return Convert.ToInt32(sum / rentPrice); }
        }

        #region static_methods
        public static DateTime DatePayment(Tariff tariff)
        {
            if (tariff.RentValue.Id == 1)
                return DateTime.Now.AddDays(Convert.ToInt32(tariff.Deposit / tariff.RentValue.Price));
            else
                return DateTime.Now.AddMonths(Convert.ToInt32(tariff.Deposit / tariff.RentValue.Price));
        }

        public static decimal Debt(Tariff tariff)
        {
            if (DateTime.Now > tariff.DatePayment)
            {
                tariff.Deposit = 0;
                int period;
                if (tariff.RentValue.Id == 1)
                {
                    period = (DateTime.Now - tariff.DatePayment).Days;
                    return tariff.RentValue.Price * (period + 1);
                }
                else
                {
                    int year = 1;
                    if (DateTime.Now.Year > tariff.DatePayment.Year)
                        year = (DateTime.Now.Year - tariff.DatePayment.Year) * 12;

                    int month = (DateTime.Now.Month - tariff.DatePayment.Month) * year;
                    return tariff.RentValue.Price * month;
                }
            }
            else
                return 0;
        }

        #endregion
    }
}
