using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }
        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            //Arrange
            var gig = new Gig() { ArtistId = _userId + "-" };
           
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            
            //action
            var result = _controller.Cancel(1);

            //Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            //Arrange
            var gig = new Gig() { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            //action
            var result = _controller.Cancel(1);

            //Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
