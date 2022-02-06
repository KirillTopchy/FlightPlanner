using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner_Web.Models
{
    public class PageResult
    {
        public int Page { get; set; }

        public int TotalItems { get; set; }

        public List<Flight> Items { get; set; }

        public PageResult(List<Flight> items)
        {
            Page = 0;
            TotalItems = items.Count();
            Items = items;
        }
    }
}
