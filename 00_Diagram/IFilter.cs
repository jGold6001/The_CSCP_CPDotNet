using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IFilter 
    {
        IRecord ResultFind(string expression);
        List<IRecord> ClientsDebt { get; set; }
        List<IRecord> MonthlyRents { get; set; }
        List<IRecord> DailyRents { get; set; }
    }
}