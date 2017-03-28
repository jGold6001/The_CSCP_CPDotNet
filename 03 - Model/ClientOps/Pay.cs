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
        public Tariff tariff { get; private set; }
        private decimal sumBuff;   

        public Pay(Tariff tariff)
        {
            this.tariff = tariff;           
        }

        public decimal MinSum
        {
            get
            {
                  return tariff.RentValue.Price + tariff.Debt;
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

        private void AddDeposit(decimal sum)
        {
            this.sumBuff = sum;
            if (tariff.Debt == 0)
                tariff.Deposit += sum;
            else
                this.DebtOff();

            calk = new Calculator(tariff, sumBuff);
            tariff.DatePayment = calk.DatePayment();
        }

        private void DebtOff()
        {
            if (sumBuff >= MinSum)
            {
                sumBuff -= tariff.Debt;
                tariff.Deposit = sumBuff;
                tariff.Debt = 0;
                tariff.DatePayment = DateTime.Now;
            }            
        }
    }
}
