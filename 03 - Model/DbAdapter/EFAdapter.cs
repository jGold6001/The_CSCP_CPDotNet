using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03___Model
{
    public abstract class EFAdapter : IDBAdapter
    {
        protected EFContext db;
        public EFAdapter(string conString)
        {
            db = new EFContext(conString);
        }

        public abstract void Add(object newObj);

        public abstract void Edit(object curObj, object newObj);

        public abstract void Remove(object curObj);
       
    }
}