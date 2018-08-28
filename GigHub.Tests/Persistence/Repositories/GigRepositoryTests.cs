using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<IApplicationDbContext> _mockContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();

            _mockContext = new Mock<IApplicationDbContext>();

            //Note the introduction of () => in the line of code marked with (*).
            //This effectively means that the return value gets lazily evaluated,
            //the lines marked with (1) and (2) will end up running first.
            //We need lazily evaluate in new version moq. ( after 4.7)
            _mockContext.SetupGet(c => c.Gigs).Returns(() => _mockGigs.Object);

            _repository = new GigRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInthePast_ShouldNotBeReturned()
        {
            
            //arrange
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });

            //action
            var gigs = _repository.GetUpcomingGigsByArtist("1");

            //assert
            gigs.Should().BeEmpty();

        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            //arrange
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            //action
            var gigs = _repository.GetUpcomingGigsByArtist("1");

            //assert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);

        }

    }
}
