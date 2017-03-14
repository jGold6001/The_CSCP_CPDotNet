using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public class EFClientRecord : EFAdapter, IClientRecords
    {
        public EFClientRecord(string conString) : base(conString)
        {
        }
        public IDisplayInfo IDisplayInfo { get; set; }
        public IFilter IFilter { get; set; }
        public IPay IPay { get; set; }
        public ISelectPlace ISelectPlace { get; set; }
        public ITarriffEdit ITarriffEdit { get; set; }

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