using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03___Model
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
        public decimal Price { get; set; }

    }
}