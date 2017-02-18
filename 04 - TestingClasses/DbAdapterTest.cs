using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MainWnd;

namespace _04___TestingClasses
{
    public class DbAdapterTest : DbAdapter
    {

        public DbAdapterTest(string conString) : base(conString)
        {

        }


        public DataBase GetDb
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
                db.Tarriffs.RemoveRange(db.Tarriffs);
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

        public List<TarriffExt> ListTarriff
        {
            get
            {
                return (from t in GetDb.Tarriffs
                        join p in GetDb.Places on t.Id equals p.TarriffId
                        where p.TarriffId == t.Id
                        select new TarriffExt()
                        {
                            NumPlace = p.Number,
                            Cost = t.Cost,
                            Deposit = t.Deposit,
                            Rental = t.Rental,
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
