using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using The_CSCP;

namespace LoadTestData
{
    class Program
    {
        static void Main(string[] args)
        {
            DbLoad();
            Console.WriteLine("Press key...");
            Console.ReadKey();
        }

        static void DbLoad()
        {
            using (EFContext db = new EFContext())
            {
                Console.WriteLine("Start load...");
                Thread.Sleep(400);
                Console.WriteLine("Please wait...");
                Thread.Sleep(400);
                Task task = new Task(async () =>
                {
                    await Task.Run(() =>
                    {
                        TestDB test = new TestDB(db);
                        test.LoadData();
                    });
                });
                task.Start();
                for (int i = 1; i < 101; i++)
                {
                    Console.Clear();
                    Console.WriteLine(i + "%");
                    Thread.Sleep(100);
                }
                for (int i = 1; i < 20; i++)
                {
                    Console.Write("-");
                    Thread.Sleep(100);
                }               
                Console.WriteLine();
                Console.WriteLine("Db loaded...");
            }              
        }

    }
}
