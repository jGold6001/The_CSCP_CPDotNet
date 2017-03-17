using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    public class Pay : IPay
    {
        private Calculator calk;
        private Tariff tariff;
        EFClientOp efClient { get; set; }
        public event DbListener dbListener;


        public Pay(Tariff tariff)
        {
            this.tariff = tariff;
            calk = new Calculator(tariff);
            dbListener += this.Answer;     
        }

        public void AddDeposit(decimal summ)
        {
            if (tariff.Debt == 0)
                tariff.Deposit += summ;
            else
                this.DebtOff(summ);

            calk.DatePayment();
      
        }

        private void DebtOff(decimal summ)
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

        private void Answer(object obj)
        {
            Console.WriteLine("yes work!!!");
        }
    }
}
