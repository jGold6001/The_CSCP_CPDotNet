using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IDBAdapter
    {
        void Add(object newObj);
        void Remove(object curObj);
        void Edit(object curObj, object newObj);
    }
}