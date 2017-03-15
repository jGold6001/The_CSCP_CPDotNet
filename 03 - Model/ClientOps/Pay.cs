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
        public void AddDeposit(IPrice price)
        {
            throw new NotImplementedException();
        }

        public void DebtOff(IPrice price)
        {
            throw new NotImplementedException();
        }
    }
}
