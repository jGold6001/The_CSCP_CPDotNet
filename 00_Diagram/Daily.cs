﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
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
        public IPrice IPrice { get; set; }
 
    }
}