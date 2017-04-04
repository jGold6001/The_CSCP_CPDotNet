using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace _03___Model
{
    public class EFUserOp : EFAdapter, IUserOperations
    {
        public EFUserOp()
        {
            
        }

        public IAuthorization IAuthorization { get; set; }
      
        public List<User> GetUsers
        {
            get
            {
                db.Positions.Load();
                db.Users.Load();
                return db.Users.ToList();
            }
        }

        public override void Add(object newObj)
        {
            throw new NotImplementedException();
        }

        public override void Edit(object newObj)
        {
            throw new NotImplementedException();
        }

        public override void Remove(object curObj)
        {
            throw new NotImplementedException();
        }
    }
}