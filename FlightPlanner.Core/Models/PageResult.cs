using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult()
        {
            Page = 0;
            TotalItems = 0;
            Items = new List<Flight>();
        }
    }
}
