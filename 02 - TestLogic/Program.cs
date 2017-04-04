using _01___View;
using _03___Model;
using _04___TestingClasses;

using DataLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _02___TD_Logic
{
    class Program
    {
        static void Main(string[] args)
        {

            /*тесты*/
            DbLoad();

            Console.WriteLine("Press key...");
            Console.ReadKey();
        }


        static void DbLoad()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            dbtest.LoadTestData();
            Console.WriteLine("...Db load...");
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
            Pay pay = new Pay(place.Tariff);
            pay.Payment(summ);
            Console.WriteLine(place);
        }


        static void Test_AddDeposit_Month()
        {
            //-------------------input
            EFContext db = new EFContext();
            TestDB testData = new TestDB(db);
            db = testData.db;

            Place place = db.Places.Local[3]; //select record             
            decimal summ = 300;//enter summ
            Console.WriteLine(place + summ.ToString());


            //--------------------output
            Pay pay = new Pay(place.Tariff);
            pay.Payment(summ);
            Console.WriteLine(place);
        }

        static void JSonData()
        {
            //string path = @"H:\ITstep\Subjects\CP_dotNet\The_CSCP\DataLayer\Jsons\Brands.json";
            List<string> list = JSONReader.Colors();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

       
    }
}
