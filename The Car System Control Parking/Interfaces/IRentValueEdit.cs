using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_CSCP
{
    public interface IRentValueEdit
    {
        void SetPrice(int rentId, decimal summ);
    }
}