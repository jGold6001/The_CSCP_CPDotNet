using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    public class Pay : EFClientOp, IPay
    {
        private Calculator calk;
        private Tariff tariff;
        private decimal sumBuff;   

        public Pay(Tariff tariff)
        {
            this.tariff = tariff;           
        }

        public decimal NeedSum
        {
            get { return tariff.RentValue.Price + tariff.Debt; }
        }

        public object AddDeposit(decimal sum)
        {
            this.sumBuff = sum;
            if (tariff.Debt == 0)
                tariff.Deposit += sum;
            else
                this.DebtOff();

            calk = new Calculator(tariff, sumBuff);
            tariff.DatePayment = calk.DatePayment(tariff.DatePayment);
            return tariff;
        }

        private void DebtOff()
        {
            if (sumBuff >= NeedSum)
            {
                sumBuff -= tariff.Debt;
                tariff.Deposit = sumBuff;
                tariff.Debt = 0;
                tariff.DatePayment = DateTime.Now;
            }            
        }

        public void Payment(decimal sum)
        {
            this.AddDeposit(sum);
            foreach (var item in db.Tariffs)
            {
                if (item.Id == tariff.Id)
                {
                    item.Deposit = tariff.Deposit;
                    item.Debt = tariff.Debt;
                    item.DatePayment = tariff.DatePayment;
                    break;
                }
            }
            db.SaveChanges();
        }


    }
}
