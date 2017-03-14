using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IPay
    {
        void AddDeposit(IPrice price);
        void DebtOff(IPrice price);
    }
}