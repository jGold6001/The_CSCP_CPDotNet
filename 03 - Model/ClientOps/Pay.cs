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
        public ICalculator calk { get; set; }
        private Tariff tariff;

        public Pay(Tariff tariff)
        {
            this.tariff = tariff;
            calk = new Calculator(tariff);
        }

        public void AddDeposit(decimal summ)
        {
            if (tariff.Debt == 0)
                tariff.Deposit += summ;
            else
                this.DebtOff(summ);

            calk.DatePayment();
        }

        public void DebtOff(decimal summ)
        {
             if (tariff.Debt > 0)
            {
                if (tariff.Debt >= summ)
                {
                    tariff.Debt -= summ;
                    tariff.DatePayment = new DateTime();
                }                
                else
                {
                    tariff.Deposit = (summ - tariff.Debt);
                    tariff.Debt = 0;
                }                  
            }             
        }
    }
}
