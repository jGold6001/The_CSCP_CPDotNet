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
            Test_DebtOff_Equal();
           
            




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

        static void Test_AddDeposit_Day()
        {
            //-------------------input
            EFContext db = new EFContext();
            TestDB testData = new TestDB(db);
            db = testData.db;

            Place place = db.Places.Local[0]; //select record             
            decimal summ = 20;//enter summ
            Console.WriteLine(place + summ.ToString());


            //--------------------output
            EFClientOp client = new EFClientOp();
            client.Payment(place.Tariff, summ);
        }

        static  void Test_DebtOff_Equal()
        {

            //-------------------input
            EFContext db = new EFContext();
            TestDB testData = new TestDB(db);
            db = testData.db;

            Place place = db.Places.Local[2]; //select record             
            decimal summ = 20;//enter summ
            Console.WriteLine(place + summ.ToString());


            //--------------------output
            EFClientOp client = new EFClientOp();
            client.Payment(place.Tariff, summ);
        }


    }
}
