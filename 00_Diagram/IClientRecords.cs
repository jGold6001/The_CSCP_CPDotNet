using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IClientRecords : IBaseOperation
    {
        IDisplayInfo IDisplayInfo { get; set; }
        IPay IPay { get; set; }
        IFilter IFilter { get; set; }
        ITarriffEdit ITarriffEdit { get; set; }
        ISelectPlace ISelectPlace { get; set; }
    }
}