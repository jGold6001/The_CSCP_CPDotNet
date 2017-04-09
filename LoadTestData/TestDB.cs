using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_CSCP;

namespace LoadTestData
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
            this.db.Positions.Load();          
            this.db.Users.Load();            
        }

        public void LoadData()
        {
            try
            {
                db.RentValues.AddRange(this.RentValues());
                db.Places.AddRange(this.Places());
                db.Positions.AddRange(this.Positions());
                db.Users.AddRange(this.Users());       
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
                new Place() { Number = 5, Client = Clients()[4], Car = Cars()[4], Tariff= Tariffs()[4]},

                new Place() { Number = 20, Client = Clients()[5], Car = Cars()[5], Tariff= Tariffs()[5]},
                new Place() { Number = 31, Client = Clients()[6], Car = Cars()[6], Tariff= Tariffs()[6]},
                new Place() { Number = 62, Client = Clients()[7], Car = Cars()[7], Tariff= Tariffs()[7]},
                new Place() { Number = 63, Client = Clients()[8], Car = Cars()[8], Tariff= Tariffs()[8]},
                new Place() { Number = 21, Client = Clients()[9], Car = Cars()[9], Tariff= Tariffs()[9]},

                new Place() { Number = 55, Client = Clients()[10], Car = Cars()[10], Tariff= Tariffs()[10]},
                new Place() { Number = 56, Client = Clients()[11], Car = Cars()[11], Tariff= Tariffs()[11]},
                new Place() { Number = 57, Client = Clients()[12], Car = Cars()[12], Tariff= Tariffs()[12]},
                new Place() { Number = 38, Client = Clients()[13], Car = Cars()[13], Tariff= Tariffs()[13]},
                new Place() { Number = 39, Client = Clients()[14], Car = Cars()[14], Tariff= Tariffs()[14]}
            };
        }

        public List<Client> Clients()
        {
            return new List<Client>()
            {
                new Client() { FirstName = "Джек", LastName = "Лондон", PassportID = "СМ123456", PhoneNumber = 0631234567, AdditionalInfo = "Американский Писака", DateRegistred= DateTime.Now.AddDays(-3) },
                new Client() { FirstName = "Джонни", LastName ="Уокер", PassportID = "ПР123456", PhoneNumber = 0731234567, AdditionalInfo = "Вискарик", DateRegistred= DateTime.Now.AddDays(-10) },
                new Client() { FirstName = "Богдан", LastName ="Хмельницкий", PassportID = "НО123456", PhoneNumber = 0501234567, AdditionalInfo = "Гетьман", DateRegistred= DateTime.Now.AddMonths(-1) },
                new Client() { FirstName = "Алеша", LastName = "Попович", PassportID = "ДЛ123456", PhoneNumber = 0971234567, DateRegistred= DateTime.Now.AddMonths(-2)},
                new Client() { FirstName = "Василий", LastName = "Пупкин", PassportID = "ТР123456",  AdditionalInfo = "Студент", DateRegistred= DateTime.Now.AddDays(-20) },


                 new Client() { FirstName = "Аркадий", LastName = "Лондон", PassportID = "СМ123986", PhoneNumber = 0631234657, AdditionalInfo = "Брат Джека, живе у Полтаве", DateRegistred= DateTime.Now },
                new Client() { FirstName = "Аркадий", LastName ="Булкин", PassportID = "ПР123654", PhoneNumber = 0731234657, DateRegistred= DateTime.Now.AddDays(-5) },
                new Client() { FirstName = "Богдан", LastName ="Ручка", PassportID = "НО123654",  AdditionalInfo = "Приходит всегда 3-го числа", DateRegistred= DateTime.Now.AddDays(-2) },
                new Client() { FirstName = "Алексей", LastName = "Горбушкин", PassportID = "ДЛ123645", PhoneNumber = 0971234657, AdditionalInfo = "Непонятный чувак", DateRegistred= DateTime.Now.AddMonths(-3).AddDays(-3)},
                new Client() { FirstName = "Василий", LastName = "Пупкин", PassportID = "ТР123654",  PhoneNumber = 0501234657, AdditionalInfo = "Клон Васи", DateRegistred= DateTime.Now },


                 new Client() { FirstName = "Маргарита", LastName = "Гамлетова", PassportID = "СМ321456", PhoneNumber = 0633214567, AdditionalInfo = "Мать Гамлета Датского", DateRegistred= DateTime.Now.AddMonths(-1).AddDays(-6) },
                new Client() { FirstName = "Емеля", LastName ="Кудряшкин", PassportID = "ПР321456", PhoneNumber = 0733214567, AdditionalInfo = "Наш человек", DateRegistred= DateTime.Now.AddDays(-3).AddDays(-10) },
                new Client() { FirstName = "Богдан", LastName ="Румынчин", PassportID = "НО321456", PhoneNumber = 0503214567, AdditionalInfo = "Еще один Бодя", DateRegistred= DateTime.Now.AddDays(-23) },
                new Client() { FirstName = "Николай", LastName = "Попович", PassportID = "ДЛ321456", PhoneNumber = 0973214567, DateRegistred= DateTime.Now.AddMonths(-1)},
                new Client() { FirstName = "Алиса", LastName = "Чудотворец", PassportID = "ТР321456",  AdditionalInfo = "Просто уматовая чувиха)", DateRegistred= DateTime.Now.AddDays(-28) }


            };
        }


        public List<Car> Cars()
        {
            return new List<Car>()
            {
                new Car() {VehicleID="АК1234АВ", Brand="UAZ (УАЗ)", VIN="ZFA22330005556771", Color="Белый" },
                new Car() {VehicleID="АА1234РВ", Brand="Ford", VIN="IFA22350015556772", Color="Красный" },
                new Car() {VehicleID="АС1234ЛД", Brand="Audi", VIN="ZOA22370025556773", Color="Жёлтый" },
                new Car() {VehicleID="ВН1234ЛО", Brand="Mitsubishi", VIN="LFA22380035556774", Color="Синий" },
                new Car() {VehicleID="СА1234ЩШ", Brand="BMW", VIN="KFA22390065556775", Color="Черный" },

                new Car() {VehicleID="АК3214АВ", Brand="UAZ (УАЗ)", VIN="ZFA22331115556771", Color="Белый" },
                new Car() {VehicleID="АА3214РВ", Brand="Chery", VIN="IFA22351115556772", Color="Красный" },
                new Car() {VehicleID="АС3214ЛД", Brand="Chevrolet", VIN="ZOA22371125556773", Color="Баклажан" },
                new Car() {VehicleID="ВН3214ЛО", Brand="Chevrolet", VIN="LFA22381135556774", Color="Синий" },
                new Car() {VehicleID="СА3214ЩШ", Brand="Great Wall", VIN="KFA22391165556775", Color="Черный" },

                new Car() {VehicleID="АК2134АВ", Brand="Jaguar", VIN="ZFA22333335556771", Color="Серо-бежевый" },
                new Car() {VehicleID="АА2134РВ", Brand="Hyundai", VIN="IFA22353315556772", Color="Красный" },
                new Car() {VehicleID="АС2134ЛД", Brand="Audi", VIN="ZOA22373325556773", Color="Жёлтый" },
                new Car() {VehicleID="ВН2134ЛО", Brand="Honda", VIN="LFA22383335556774", Color="Виктория" },
                new Car() {VehicleID="СА2134ЩШ", Brand="Lexus", VIN="KFA22393365556775", Color="Аметист" }
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
                new Tariff() { RentValueId=2, Debt = 0, Deposit = 300, DatePayment=DateTime.Now.AddMonths(3) },

                new Tariff() { RentValueId=1, Debt = 0, Deposit =30, DatePayment=DateTime.Now.AddDays(3)  },
                new Tariff() { RentValueId=2, Debt = 300, Deposit =0, DatePayment=DateTime.Now.AddMonths(-3) },
                new Tariff() { RentValueId=1, Debt = 30, Deposit =0,DatePayment=DateTime.Now.AddDays(-3) },
                new Tariff() { RentValueId=2, Debt = 100, Deposit = 0,  DatePayment=DateTime.Now.AddMonths(-1)},
                new Tariff() { RentValueId=1, Debt = 0, Deposit = 300, DatePayment=DateTime.Now.AddMonths(3) },

                new Tariff() { RentValueId=1, Debt = 0, Deposit =30, DatePayment=DateTime.Now.AddDays(3)  },
                new Tariff() { RentValueId=2, Debt = 0, Deposit =200, DatePayment=DateTime.Now.AddMonths(2) },
                new Tariff() { RentValueId=1, Debt = 50, Deposit =0,DatePayment=DateTime.Now.AddDays(-5) },
                new Tariff() { RentValueId=2, Debt = 200, Deposit = 0,  DatePayment=DateTime.Now.AddMonths(-2)},
                new Tariff() { RentValueId=2, Debt = 0, Deposit = 300, DatePayment=DateTime.Now.AddMonths(3) }

            };
        }

        public List<RentValue> RentValues()
        {
            return new List<RentValue>()
            {
                new RentValue { Name="Суточный", Price=10 },
                new RentValue {Name="Месячный", Price=100, }
            };
        }

        public List<User> Users()
        {
            return new List<User>()
            {
                new User() {FirstName = "Админ", LastName = "Админ", Login = "admin", Password = "admin", PositionId = 1 },
                new User() {Login="Cheff", Password="killAmAll", FirstName="Арсений", LastName="Бердяник", PositionId=1 },
                new User() {Login="Goga", Password="goga", FirstName="Борис", LastName="Гогишвили", PositionId=2 },
                new User() {Login="Kuzya", Password="Passwd01", FirstName="Семен", LastName="Кузьменко", PositionId=2 },
            };

        }

        public List<Position> Positions()
        {
            return new List<Position>()
            {
                new Position() {Name="Администратор" },
                new Position() {Name="Пользователь" }
            };
        }

    }

}
