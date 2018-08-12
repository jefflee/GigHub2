using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using AutoMapper;
using GigHub.Dtos;
using GigHub.Util;

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
                //we don't want to display messages which is read, but remove this condition is easy to run the demo.
                //.Where(un => un.UserId == userId && un.IsRead)  
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            IMapper mapper = MyMapper.Instance.Mapper;
            return notifications.Select(k => mapper.Map<Notification, NotificationDto>(k));
        }
    }
}
