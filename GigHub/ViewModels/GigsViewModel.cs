using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public bool ShowActtions { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}
