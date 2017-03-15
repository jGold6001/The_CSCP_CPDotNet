using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ICalculator
    {
        void Debt();
        void DatePayment();
        IPrice Period(DateTime date);
    }
}