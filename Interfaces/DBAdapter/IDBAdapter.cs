using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDBAdapter
    {
        event DbListener dbListener;
        void Add(object newObj);
        void Remove(object curObj);
        void Edit(object curObj, object newObj);
    }
}