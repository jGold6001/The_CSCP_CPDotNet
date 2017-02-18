using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace _04___TestingClasses
{
    public class BLTest : BusinessLogic 
    {
        public BLTest(Tarriff tarriff) : base(tarriff)
        {

        }

        public void AddDeposit(decimal summ, DateTime dt)
        {

            if (Tarriff.Debt == 0)
                Tarriff.Deposit += summ;
            else
                this.DebtOff(summ);

            this.CalkDatePayment(dt);
        }
    }
}
