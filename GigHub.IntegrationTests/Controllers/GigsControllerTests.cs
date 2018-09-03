using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Mine();

            // Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

    }
}
