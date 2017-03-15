using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public class EFClientOp : EFAdapter, IClientOperations
    {
        public EFClientOp(string conString) : base(conString)
        {
        }
        public IDisplayInfo IDisplayInfo { get; set; }
        public IFilter IFilter { get; set; }
        public IPay IPay { get; set; }
        public IRecord IRecord { get; set; }     
        public IAvaPlace ISelectPlace { get; set; }
        public ITarriffEdit ITarriffEdit { get; set; }

        public override void Add(object newObj)
        {
            throw new NotImplementedException();
        }

        public override void Edit(object curObj, object newObj)
        {
            throw new NotImplementedException();
        }

        public override void Remove(object curObj)
        {
            throw new NotImplementedException();
        }
    }
}