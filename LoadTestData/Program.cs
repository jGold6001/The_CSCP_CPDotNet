using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestData
{
    class Program
    {
        static void Main(string[] args)
        {

            /*тесты*/
            //DbLoad();

            Console.WriteLine("Press key...");
            Console.ReadKey();
        }


        static void DbLoad()
        {
            DbAdapterTest dbtest = new DbAdapterTest();
            dbtest.LoadTestData();
            Console.WriteLine("...Db load...");
        }
    }
}
