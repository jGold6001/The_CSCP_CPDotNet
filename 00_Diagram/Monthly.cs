using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public class Monthly : IRent
    {
        public int Id
        {
            get { return 2; }
        }

        public string Name
        {
            get { return "Месячный"; }
        }
        public IPrice IPrice { get; set; }

    }
}