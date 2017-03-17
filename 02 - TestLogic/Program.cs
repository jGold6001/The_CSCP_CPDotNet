using _03___Model;
using _04___TestingClasses;
using DataLayer;
using DataLayer.Constats;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _02___TD_Logic
{
    class Program
    {
        static void Main(string[] args)
        {
          
            /*тесты*/
            Test_AddDeposit_Day();
           
            




            Console.WriteLine("Press key...");
            Console.ReadKey();
        }


        static void DbLoad()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            dbtest.LoadTestData();
            Console.WriteLine("...Db load...");
        }

        static void DbDisplay()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            dbtest.ConsoleTestDisplay();
        }

        static void DbDrop()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            dbtest.DeleteDB();
            Console.WriteLine("...Db Delete...");
        }

        static void TarriffQuery()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            var query = (from t in dbtest.GetDb.Tariffs
                        join p in dbtest.GetDb.Places on t.Id equals p.TariffId
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

            foreach (var item in query)
            {
                Console.WriteLine(item.NumPlace + " "+ item.Deposit);
            }
        }

        //static void Test_AddDeposit_Month()
        //{
        //    Tariff tarriff;
        //    BusinessLogic bl = new BusinessLogic();
        //    bl.SetCostRental(10, 100);           
        //    Console.WriteLine(" date now: " + DateTime.Now + "\n");
        //    tarriff = bl.CreateTarriff(RentValues.M, 200);
        //    Console.WriteLine(tarriff);
        //    decimal summ = 300;
        //    Console.WriteLine("Пополненяю депозит: " + summ + "\n");
        //    bl.AddDeposit(summ);
        //    Console.WriteLine(tarriff);
        //}

        static void Test_AddDeposit_Day()
        {
            //загружае запись из базы
            EFContext db = new EFContext();
            TestDB testData = new TestDB(db); //клас содержащий LOad
            db = testData.db;
            Place place = db.Places.Local[0];
            Console.WriteLine(place);

            //помещаем тариф клиента в Pay
            Pay pay = new Pay(place.Tariff);
            pay.AddDeposit(10);
            




        }

        //static void Test_DebtOff()
        //{
        //    BusinessLogic bl_1 = new BusinessLogic();
        //    bl_1.SetCostRental(10, 100);
        //    Tarriff tarriff = bl_1.CreateTarriff(RentValues.D, 20);
        //    Console.WriteLine(" date now: " + DateTime.Now + "\n");
        //    Console.WriteLine(tarriff);

        //    BusinessLogic bl_2 = new BusinessLogic(tarriff);
        //    DateTime dtWillBe = new DateTime(2017, 02, 06);
        //    Console.WriteLine(" date will be: " + dtWillBe + "\n");
        //    bl_2.CalkDebt(dtWillBe);
        //    Console.WriteLine(tarriff);

        //    Console.WriteLine("Гасим кредит --> 10");
        //    bl_2.DebtOff(10);
        //    Console.WriteLine(tarriff);
        //}

        //static void Test_AddDeposit_With_DebtOff()
        //{
        //    BusinessLogic bl_1 = new BusinessLogic();
        //    bl_1.SetCostRental(10, 100);
        //    Tarriff tarriff = bl_1.CreateTarriff(RentValues.M, 300);
        //    Console.WriteLine(" date now: " + DateTime.Now + "\n");
        //    Console.WriteLine(tarriff);

        //    BusinessLogic bl_2 = new BusinessLogic(tarriff);
        //    DateTime dtWillBe = new DateTime(2017, 10, 03);
        //    Console.WriteLine(" date will be: " + dtWillBe + "\n");
        //    bl_2.CalkDebt(dtWillBe);
        //    Console.WriteLine(tarriff);

        //    decimal inputSumm = 700;
        //    Console.WriteLine("Вношу сумму: " + inputSumm + "\n");
        //    BLTest blTest = new BLTest(tarriff);
        //    blTest.AddDeposit(inputSumm, dtWillBe);
        //    Console.WriteLine(tarriff);


        //}

    }
}
