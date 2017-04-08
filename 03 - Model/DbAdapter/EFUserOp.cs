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
      
        public List<User> GetUsers
        {
            get
            {
                db.Positions.Load();
                db.Users.Load();
                return db.Users.ToList();
            }
        }

        public List<Position> Positions
        {
            get
            {
                return db.Positions.ToList();
            }
        }

        public List<UserExtend> UserList
        {
            get
            {
                return db.Users.Select(u => new UserExtend()
                {
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Login = u.Login,
                   Password = u.Password,
                   Position = u.Position.Name
                }).ToList();
            }
        }

        public override void Add(object newObj)
        {
            
            User user = newObj as User;
            foreach (User userCurr in GetUsers)
            {
                if (user.Login == userCurr.Login)
                    return;
            }
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        public override void Edit(object newObj)
        {
            User newUser = newObj as User;
            foreach (User userCurrent in db.Users.Local)
            {
                if (userCurrent.Login == newUser.Login)
                {
                    //write not changed data
                    newUser.Login = userCurrent.Login;
                    newUser.LastName = userCurrent.LastName;
                    newUser.FirstName = userCurrent.FirstName;

                    //delete old data
                    db.Users.Remove(userCurrent);

                    db.Users.Add(newUser);
                    break;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                //если не все данные будут заполнены
                return;
            }
        }

        public override void Remove(object curObj)
        {
            UserExtend userEx = curObj as UserExtend;
            foreach (User user in db.Users)
            {
                if (userEx.Login == user.Login)
                {
                    db.Users.Remove(user);
                    break;
                }
            }
            db.SaveChanges();
        }
    }
}