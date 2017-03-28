using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03___Model
{
    public class Daily : IRent
    {
        public int Id
        {
            get { return 1; }
        }

        public string Name
        {
            get { return "Суточный"; }
        }
        public decimal Price { get; }

    }
}