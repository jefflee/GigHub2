using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigDetialViewModel
    {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}