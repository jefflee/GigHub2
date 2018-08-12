using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(k => new NotificationDto()
            {
                DateTime = k.DateTime,
                Gig = new GigDto()
                {
                    Artist = new UserDto()
                    {
                        Id = k.Gig.Artist.Id,
                        Name = k.Gig.Artist.Name
                    },
                    DateTime = k.Gig.DateTime,
                    Id = k.Gig.Id,
                    IsCanceled = k.Gig.IsCanceled,
                    Venue = k.Gig.Venue
                },
                OriginalDateTime = k.OriginalDateTime,
                OriginalVenue = k.OriginalVenue,
                Type = k.Type
            });
        }
    }
}
