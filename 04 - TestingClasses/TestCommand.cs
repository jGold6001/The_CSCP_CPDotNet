using DataLayer.Constats;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels;
using ViewModels.MainWnd;

namespace _04___TestingClasses
{
    public static class TestCommand
    {
        public static Place SimpleTestAdd
        {
            get
            {
                Client client = new Client() { FirstName = "Антон", LastName = "Городецкий", PassportID = "ВП123459", PhoneNumber = 0969876543 };
                Car car = new Car() { VehicleID = "Хз", Brand = "Toyota", VIN = "Хз", Color = "Red" };
                Tarriff tarriff = new Tarriff() { Rental = RentValues.M, Cost = 15, Debt=30, Deposit=31, DatePayment = DateTime.Now.Date };
                Place place = new Place() { Number = 8, DateRegistred = DateTime.Now.Date, Client = client, Car = car, Tarriff = tarriff };

                Place place1 = new Place();

                Place place2 = new Place() { Number = 9, Car = car,};
                return place;
            }
           
        }

        public static Place SimpleTestEdit(Record rec, DbAdapter dba)
        {           
                Place place = null;
                foreach (var item in dba.GetPlaces)
                {
                    if (rec.NumberPLace == item.Number)
                    {
                        item.Client.LastName = "Zig";
                        item.Car.Brand = "Zag";
                        item.Tarriff.Cost = 100;
                        place = item;
                        break;
                    }
                }
                return place;          
        }

        public static void LoadData(DbAdapterTest dba)
        {
            try
            {
                dba.LoadTestData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public static void ClearData(DbAdapterTest dba)
        {
            try
            {
                dba.ClearDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
