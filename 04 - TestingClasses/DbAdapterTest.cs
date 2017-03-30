using _03___Model;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _04___TestingClasses
{
    public class DbAdapterTest : EFAdapter
    {
        
        public EFContext GetDb
        {
            get { return db; }
        }

        public void LoadTestData()
        {
            TestDB test = new TestDB(db);
            try
            {
                test.LoadData();
            }
            catch
            {
                throw;
            }
        }

        public void ClearDB()
        {
            try
            {
                db.Places.RemoveRange(db.Places);
                db.Clients.RemoveRange(db.Clients);
                db.Cars.RemoveRange(db.Cars);
                db.Tariffs.RemoveRange(db.Tariffs);
                db.RentValues.RemoveRange(db.RentValues);
                db.Users.RemoveRange(db.Users);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteDB()
        {
            db.Database.Delete();
        }

        public void ConsoleTestDisplay()
        {
            TestDB test = new TestDB(db);
            test.DisplayData();

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

        public List<TarriffExt> ListTarriff
        {
            get
            {
                return (from t in GetDb.Tariffs
                        join p in GetDb.Places on t.Id equals p.TariffId
                        where p.TariffId == t.Id
                        select new TarriffExt()
                        {
                            NumPlace = p.Number,
                            Deposit = t.Deposit,
                            Debt = t.Debt,
                            Rental = t.RentValue.Name,
                            Cost = t.RentValue.Price,
                            DatePayment = t.DatePayment
                        }).ToList();
            }
        }

        public List<Client> ListClient
        {
            get { return db.Clients.ToList(); }
        }

        public List<Car> ListCars
        {
            get { return db.Cars.ToList(); }
        }


    }
}
