using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    public class RentValueEditor : EFClientOp, IRentValueEdit
    {
        public void SetPrice(int rentId, decimal summ)
        {
            if (rentId == 1)
                this.RentTypes[0].Price = summ;
            else
                this.RentTypes[1].Price = summ;

            db.SaveChanges();
        }
    }
}
