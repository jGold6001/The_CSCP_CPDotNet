using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Constats
{
    public static class RentValues
    {
        public const string D = "Сутки";
        public const string M = "Месяц";
        public static decimal Mpay { get; set; }
        public static decimal Dpay { get; set; }

        public static List<string> ListRents()
        {
            return new List<string>() { D, M };
        }

    }
}
