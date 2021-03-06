﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        //Move this Data annotation to ApplicationDbContext. Use FluentApih.
        //[Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        //Move this Data annotation to ApplicationDbContext. Use FluentApih.
        //[Required]
        //[StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        //Move this Data annotation to ApplicationDbContext. Use FluentApih.
        //[Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, this.DateTime, this.Venue);

            this.Venue = venue;
            this.DateTime = dateTime;
            this.GenreId = genre;

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}