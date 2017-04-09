using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_CSCP
{
    public abstract class EFAdapter : IDBAdapter
    {
        protected EFContext db;

        public EFAdapter()
        {
            db = new EFContext();
        }

        public abstract void Add(object newObj);

        public abstract void Edit(object newObj);

        public abstract void Remove(object curObj);
       
    }
}