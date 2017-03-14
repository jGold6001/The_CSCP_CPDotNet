using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IRent
    {
        int Id { get;}
        string Name { get;}
        IPrice IPrice { get; set; }
    }
}