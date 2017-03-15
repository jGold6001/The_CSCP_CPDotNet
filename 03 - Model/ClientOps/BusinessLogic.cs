using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BusinessLogic
    {
        public Tariff Tarriff { get; private set; }     

        public BusinessLogic()
        {
           //вызываем если обьект Tarriff не создан;
        }

        public BusinessLogic(Tariff tarriff)
        {
            this.Tarriff = tarriff;
        }
        

        public Tariff CreateTarriff(string rentalType, decimal startDeposit)
        {
            Tarriff = new Tariff() { Rental=rentalType, Cost=this.Cost(rentalType), Deposit= startDeposit };
            this.CalkDatePayment();
            return Tarriff;
        } 
        
        public void AddDeposit(decimal summ)
        {
            
            if(Tarriff.Debt == 0)
                Tarriff.Deposit += summ;
            else
                this.DebtOff(summ);

            this.CalkDatePayment();
        }     

        public void DebtOff(decimal summ)
        {
            if (Tarriff.Debt > 0)
            {
                if (Tarriff.Debt >= summ)
                {
                    Tarriff.Debt -= summ;
                    Tarriff.DatePayment = new DateTime();
                }                
                else
                {
                    Tarriff.Deposit = (summ - Tarriff.Debt);
                    Tarriff.Debt = 0;
                }                  
            }             
        }


        public void CalkDatePayment(DateTime date)
        {
            if (Tarriff.Rental == "Сутки")
                Tarriff.DatePayment = date.AddDays(CalkRemainder);
            else
                Tarriff.DatePayment = date.AddMonths(CalkRemainder);
        }


        private void CalkDatePayment()
        {
            if (Tarriff.Rental == "Сутки")
                Tarriff.DatePayment = DateTime.Now.AddDays(CalkRemainder);
            else
                Tarriff.DatePayment = DateTime.Now.AddMonths(CalkRemainder);
        }

        //остаток от деления депозит/стоимость
        private int CalkRemainder
        {
            get { return Convert.ToInt32(Tarriff.Deposit / Tarriff.Cost); }
        }

        private decimal CalkMonths(DateTime date)
        {
            int year = 1;
            if (date.Year > Tarriff.DatePayment.Year)
                year = (date.Year - Tarriff.DatePayment.Year) * 12;

            int month = (date.Month - Tarriff.DatePayment.Month) * year;
            return Tarriff.Cost * month;
        }

        private decimal CalkDays(DateTime date)
        {
            int days = (date - Tarriff.DatePayment).Days;
            return Tarriff.Cost * (days + 1);
        }

        public void CalkDebt(DateTime date)
        {
            if (date > Tarriff.DatePayment)
            {
                if (Tarriff.Rental == "Сутки")
                    Tarriff.Debt = this.CalkDays(date);
                else
                    Tarriff.Debt = this.CalkMonths(date);

                Tarriff.Deposit = 0;
            }
        }

        private void CalkDebt()
        {
            if (DateTime.Now > Tarriff.DatePayment)
            {
                if (Tarriff.Rental == "Сутки")
                    Tarriff.Debt = this.CalkDays(DateTime.Now);
                else
                    Tarriff.Debt = this.CalkMonths(DateTime.Now);

                Tarriff.Deposit = 0;
            }
        }

        public void SetCostRental(decimal daySumm, decimal monthSumm)
        {
            RentValues.Dpay = daySumm;
            RentValues.Mpay = monthSumm;
        }

        private decimal Cost(string rentType)
        {           
                if (rentType == "Сутки")
                    return RentValues.Dpay;
                else
                    return RentValues.Mpay;       
        }
    }

}
