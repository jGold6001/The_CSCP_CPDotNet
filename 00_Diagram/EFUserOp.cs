using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public class EFUserOp : EFAdapter, IUserOperations
    {
        public EFUserOp(string conString) : base(conString)
        {
        }

        public IAuthorization IAuthorization { get; set; }
      

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