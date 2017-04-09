using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_CSCP
{
    public interface IDBAdapter
    {
        void Add(object newObj);
        void Remove(object curObj);
        void Edit(object newObj);
    }
}