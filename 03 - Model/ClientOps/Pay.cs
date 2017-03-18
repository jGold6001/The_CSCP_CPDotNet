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

        public Pay(Tariff tariff)
        {
            this.tariff = tariff;           
        }

        public object AddDeposit(decimal sum)
        {
            if (tariff.Debt == 0)
                tariff.Deposit += sum;
            else
                this.DebtOff(sum);

            calk = new Calculator(tariff, sum);
            tariff.DatePayment = calk.DatePayment(tariff.DatePayment);
            return tariff;
        }

        private void DebtOff(decimal sum)
        {         
            if (tariff.Debt >= sum)
            {
                tariff.Debt -= sum;
                tariff.DatePayment = new DateTime();
            }                
            else
            {
                tariff.Deposit = (sum - tariff.Debt);
                tariff.Debt = 0;
            }                                        
        }
    }
}
