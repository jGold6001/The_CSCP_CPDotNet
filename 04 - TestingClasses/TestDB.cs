using _03___Model;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _04___TestingClasses
{
    public class TestDB
    {
        public EFContext db;
        public TestDB(EFContext db)
        {
            this.db = db;
            this.db.Places.Load();
            this.db.Clients.Load();
            this.db.Cars.Load();
            this.db.RentValues.Load();
            this.db.Tariffs.Load();           
            this.db.Users.Load();            
        }

        public void LoadData()
        {
            try
            {
                db.RentValues.AddRange(this.RentValues());
                db.Places.AddRange(this.Places());          
                db.SaveChanges();
            }
            catch
            {
                throw;
            }       
          
        }

        public void DisplayData()
        {              
            foreach (var item in db.Places.Local)
            {
                Console.WriteLine(item);
            }
        }

        public List<Place> Places()
        {
            return new List<Place>()
            {
                new Place() { Number = 1, Client = Clients()[0], Car = Cars()[0], Tariff= Tariffs()[0]},
                new Place() { Number = 2, Client = Clients()[1], Car = Cars()[1], Tariff= Tariffs()[1]},
                new Place() { Number = 3, Client = Clients()[2], Car = Cars()[2], Tariff= Tariffs()[2]},
                new Place() { Number = 4, Client = Clients()[3], Car = Cars()[3], Tariff= Tariffs()[3]},
                new Place() { Number = 5, Client = Clients()[4], Car = Cars()[4], Tariff= Tariffs()[4]}
            };
        }

        public List<Client> Clients()
        {
            return new List<Client>()
            {
                new Client() { FirstName = "Джек", LastName = "Лондон", PassportID = "СМ123456", PhoneNumber = 0631234567, AdditionalInfo = "Американский Писака", DateRegistred= DateTime.Now },
                new Client() { FirstName = "Джонни", LastName ="Уокер", PassportID = "ПР123456", PhoneNumber = 0731234567, AdditionalInfo = "Вискарик", DateRegistred= DateTime.Now },
                new Client() { FirstName = "Богдан", LastName ="Хмельницкий", PassportID = "НО123456", PhoneNumber = 0501234567, AdditionalInfo = "Гетьман", DateRegistred= DateTime.Now.AddDays(-2) },
                new Client() { FirstName = "Алеша", LastName = "Попович", PassportID = "ДЛ123456", PhoneNumber = 0971234567, DateRegistred= DateTime.Now.AddMonths(-1)},
                new Client() { FirstName = "Василий", LastName = "Пупкин", PassportID = "ТР123456",  AdditionalInfo = "Студент", DateRegistred= DateTime.Now }
            };
        }


        public List<Car> Cars()
        {
            return new List<Car>()
            {
                new Car() {VehicleID="АК1234АВ", Brand="ВАЗ", VIN="ZFA22330005556771", Color="Белый" },
                new Car() {VehicleID="АА1234РВ", Brand="Ford", VIN="IFA22350015556772", Color="Красный" },
                new Car() {VehicleID="АС1234ЛД", Brand="Audi", VIN="ZOA22370025556773", Color="Желтый" },
                new Car() {VehicleID="ВН1234ЛО", Brand="Mitchubisi", VIN="LFA22380035556774", Color="Синий" },
                new Car() {VehicleID="СА1234ЩШ", Brand="BMW", VIN="KFA22390065556775", Color="Черный" }
            };
        }

        public List<Tariff>Tariffs()
        {
            
            return new List<Tariff>()
            {
                new Tariff() { RentValueId=1, Debt = 0, Deposit =30, DatePayment=DateTime.Now.AddDays(3)  },
                new Tariff() { RentValueId=2, Debt = 0, Deposit =200, DatePayment=DateTime.Now.AddMonths(2) },
                new Tariff() { RentValueId=1, Debt = 20, Deposit =0,DatePayment=DateTime.Now.AddDays(-2) },
                new Tariff() { RentValueId=2, Debt = 100, Deposit = 0,  DatePayment=DateTime.Now.AddMonths(-1)},
                new Tariff() { RentValueId=2, Debt = 0, Deposit = 300, DatePayment=DateTime.Now.AddMonths(3) }
            };
        }

        public List<RentValue> RentValues()
        {
            return new List<RentValue>()
            {
                new RentValue { Name="Daily", Price=10 },
                new RentValue {Name="Monthly", Price=100 }
            };
        }

    }

}
