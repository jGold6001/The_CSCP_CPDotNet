using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface ITarriffEdit
    {
        void SetPrice(IRent id, IPrice price);
    }
}