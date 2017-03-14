using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IEmployeeRecords : IBaseOperation
    {
        IAuthorization IAuthorization { get; set; }
    }
}