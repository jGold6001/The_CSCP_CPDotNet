using _04___TestingClasses;
using DataLayer;
using DataLayer.Constats;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.MainWnd;

namespace _02___TD_Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            string home = "name=DataBaseHome";
            string auditory = "name=DataBase";

            /*тест кейсы*/

            //DbDrop(home);
            DbLoad(home);

            
           
            

            Console.WriteLine("Press key...");
            Console.ReadKey();
        }


        static void DbLoad(string strConn)
        {
            DbAdapterTest dbtest = new DbAdapterTest(strConn);
            dbtest.LoadTestData();
            dbtest.ConsoleTestDisplay();
            Console.WriteLine("...Db load...");
        }

        static void DbDisplay(string strConn)
        {
            DbAdapterTest dbtest = new DbAdapterTest(strConn);
            dbtest.ConsoleTestDisplay();
        }

        static void DbDrop(string strConn)
        {
            DbAdapterTest dbtest = new DbAdapterTest(strConn);
            dbtest.DeleteDB();
            Console.WriteLine("...Db Delete...");
        }

        static void TarriffQuery(string strConn)
        {
            DbAdapterTest dbtest = new DbAdapterTest(strConn);
            var query = (from t in dbtest.GetDb.Tarriffs
                        join p in dbtest.GetDb.Places on t.Id equals p.TarriffId
                        where p.TarriffId == t.Id
                        select new TarriffExt()
                        {
                            NumPlace = p.Number,
                            Cost = t.Cost,
                            Deposit = t.Deposit,
                            Rental = t.Rental,
                            DatePayment = t.DatePayment

                        }).ToList();

            foreach (var item in query)
            {
                Console.WriteLine(item.NumPlace + " "+ item.Deposit);
            }
        }

        static void Test_AddDeposit_Month()
        {
            Tarriff tarriff;
            BusinessLogic bl = new BusinessLogic();
            bl.SetCostRental(10, 100);           
            Console.WriteLine(" date now: " + DateTime.Now + "\n");
            tarriff = bl.CreateTarriff(RentValues.M, 200);
            Console.WriteLine(tarriff);
            decimal summ = 300;
            Console.WriteLine("Пополненяю депозит: " + summ + "\n");
            bl.AddDeposit(summ);
            Console.WriteLine(tarriff);
        }

        static void Test_AddDeposit_Day()
        {
            Tarriff tarriff;
            BusinessLogic bl = new BusinessLogic();
            bl.SetCostRental(10, 100);
            Console.WriteLine(" date now: " + DateTime.Now + "\n");         
            tarriff = bl.CreateTarriff(RentValues.D, 20);
            Console.WriteLine(tarriff);
            decimal summ = 30;
            Console.WriteLine("Пополненяю депозит: " + summ + "\n");
            bl.AddDeposit(summ);
            Console.WriteLine(tarriff);

        }

        static void Test_DebtOff()
        {
            BusinessLogic bl_1 = new BusinessLogic();
            bl_1.SetCostRental(10, 100);
            Tarriff tarriff = bl_1.CreateTarriff(RentValues.D, 20);
            Console.WriteLine(" date now: " + DateTime.Now + "\n");
            Console.WriteLine(tarriff);

            BusinessLogic bl_2 = new BusinessLogic(tarriff);
            DateTime dtWillBe = new DateTime(2017, 02, 06);
            Console.WriteLine(" date will be: " + dtWillBe + "\n");
            bl_2.CalkDebt(dtWillBe);
            Console.WriteLine(tarriff);

            Console.WriteLine("Гасим кредит --> 10");
            bl_2.DebtOff(10);
            Console.WriteLine(tarriff);
        }

        static void Test_AddDeposit_With_DebtOff()
        {
            BusinessLogic bl_1 = new BusinessLogic();
            bl_1.SetCostRental(10, 100);
            Tarriff tarriff = bl_1.CreateTarriff(RentValues.M, 300);
            Console.WriteLine(" date now: " + DateTime.Now + "\n");
            Console.WriteLine(tarriff);

            BusinessLogic bl_2 = new BusinessLogic(tarriff);
            DateTime dtWillBe = new DateTime(2017, 10, 03);
            Console.WriteLine(" date will be: " + dtWillBe + "\n");
            bl_2.CalkDebt(dtWillBe);
            Console.WriteLine(tarriff);

            decimal inputSumm = 700;
            Console.WriteLine("Вношу сумму: " + inputSumm + "\n");
            BLTest blTest = new BLTest(tarriff);
            blTest.AddDeposit(inputSumm, dtWillBe);
            Console.WriteLine(tarriff);


        }

    }
}
