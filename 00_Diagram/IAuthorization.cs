using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IAuthorization 
    {
        string Login { get; set; }
        string Password { get; set; }

        bool Result(bool flag);
    }
}