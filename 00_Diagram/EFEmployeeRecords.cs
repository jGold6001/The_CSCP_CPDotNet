using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public class EFEmployeeRecords : EFAdapter, IEmployeeRecords
    {
        public EFEmployeeRecords(string conString) : base(conString)
        {
        }

        public void Add(object newObj)
        {
            throw new NotImplementedException();
        }

        public void Edit(object curObj, object newObj)
        {
            throw new NotImplementedException();
        }

        public void Remove(object curObj)
        {
            throw new NotImplementedException();
        }
    }
}