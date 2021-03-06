using System.Collections.Generic;

namespace FlightPlanner_Web.Models
{
    public class PageResult
    {
        public int Page { get; set; }

        public int TotalItems { get; set; }

        public List<Flight> Items { get; set; }

        public PageResult(List<Flight> items)
        {
            TotalItems = items.Count;
            Items = items;
        }
    }
}
