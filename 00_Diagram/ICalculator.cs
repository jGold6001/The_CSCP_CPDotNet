using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface ICalculator
    {
        void Debt();
        void DatePayment();
        void Period(DateTime date);
    }
}