using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public abstract class EFAdapter
    {
        protected EFContext db;
        public EFAdapter(string conString)
        {
            db = new EFContext(conString);
        }
    }
}