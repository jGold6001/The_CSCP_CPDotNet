using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IPay
    {
        ICalculator calk { get; set; }
        void AddDeposit(decimal summ);
        void DebtOff(decimal summ);
    }
}