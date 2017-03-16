using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    class Pay : IPay
    {
        private Calculator calk;
        private decimal deposit;
        private decimal debt;
        private DateTime datePayment;

        public Pay(Tariff tariff)
        {      
            deposit = tariff.Deposit;
            debt = tariff.Debt;
            datePayment = tariff.DatePayment;
            calk = new Calculator(tariff);
        }

        public void AddDeposit(decimal summ)
        {
            if (debt == 0)
                deposit += summ;
            else
                this.DebtOff(summ);

            calk.DatePayment();
        }

        private void DebtOff(decimal summ)
        {         
            if (debt >= summ)
            {
                debt -= summ;
                datePayment = new DateTime();
            }                
            else
            {
                deposit = (summ - debt);
                debt = 0;
            }                                        
        }
    }
}
