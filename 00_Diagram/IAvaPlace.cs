using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _00_Diagram
{
    public interface IAvaPlace
    {
        List<int> FreePlaces { get; set; }
        List<int> BusyPlaces { get; set; }
        int SelectedPlace { get; set; }
    }
}